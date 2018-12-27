using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Splitter
{
    internal class BluePrint
    {
        public int SlideId { get; set; } = -1;
        public Guid Guid { get; set; } = Guid.Empty;
        public string SlideTarget { get; set; } = null;
        public string SlideRelId { get; set; } = null;
        public string LayoutTarget { get; set; } = null;
        public string LayoutRelId { get; set; } = null;
        public string MasterTarget { get; set; } = null;
        public string MasterRelId { get; set; } = null;
        public string NotesTarget { get; set; } = null;
    }

    internal class SlideFromPresentation
    {
        private Package m_originalPackage;
        private Package m_outputPackage;
        private ArrayList m_skipParts = new ArrayList();
        private ArrayList m_partsReferencedBySlide = new ArrayList();
        public  BluePrint CurrentBP { get; set; }  = null;

        #region static strings
        static private string s_pathRels_Rels = RelsPathFromTarget("");
        static private string s_pathPrentationXml = @"/ppt/presentation.xml";
        static private string s_pathPrentationXmlRels = RelsPathFromTarget(s_pathPrentationXml);
        static private string s_relTypePptSlide = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide";
        static private string s_relTypePptNotes = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/notesSlide";
        static private string s_relTypePptLayout = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideLayout";
        static private string s_relTypePptMaster = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideMaster";
        static private string s_relTypeThumbnail = @"http://schemas.openxmlformats.org/package/2006/relationships/metadata/thumbnail";
        static private string s_relTypePptHandoutMaster = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/handoutMaster";
        static private string s_relTypePptNotesMaster = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/notesMaster";
        #endregion

        public Dictionary<int, string> SlideRelIds { get; } = new Dictionary<int, string>();
        public Dictionary<string, string> SlideTargets { get; } = new Dictionary<string, string>();
        public Dictionary<string, string> MasterRelIds { get; } = new Dictionary<string, string>();
        public string NotesMasterTarget { get => m_notesMasterTarget; }
        public string HandoutMasterTarget { get => m_handoutMasterTarget; }
        public ArrayList PptParts { get; } = new ArrayList();

        private string m_handoutMasterTarget = null;
        private string m_notesMasterTarget = null;

        #region helper APIs
        private void FlushAndClosePackage()
        {
            if (m_outputPackage != null)
            {
                m_outputPackage.Flush();
                m_outputPackage.Close();
                m_outputPackage = null;
            }
        }

        static private string MakePptTargetPathAbsolute(string target)
        {
            string path = target;
            if (target.StartsWith(@"../"))
            {
                path = target.Substring(3);
                path = @"/ppt/" + path;
            }
            else if(!target.StartsWith(@"/ppt/"))
            {
                path = @"/ppt/" + path;
            }
            return path;
        }

        static private Stream XmlDocToStream(XmlDocument doc)
        {
            Stream modified = new MemoryStream();
            doc.Save(modified);
            modified.Flush();
            modified.Seek(0, SeekOrigin.Begin);
            return modified;
        }

        static string RelsPathFromTarget(string slideTarget)
        {
            int lastSlashPos = slideTarget.LastIndexOf('/');
            string localPath = "";
            string folderPath = "";
            if (lastSlashPos < 0)
            {
                localPath = slideTarget;
            }
            else
            {
                localPath = slideTarget.Substring(lastSlashPos + 1);
                folderPath = slideTarget.Substring(0, lastSlashPos);
            }

            return folderPath + @"/_rels/" + localPath + @".rels";
        }

        private XmlDocument ReadXmlPart(string partPath, bool fModified)
        {
            Uri uri = new Uri(partPath, UriKind.Relative);
            PackagePart part = m_originalPackage.GetPart(uri);
            XmlDocument doc = fModified ? GetModifiedXmlDoc(part) : null;
            if (doc == null)
            {
                doc = new XmlDocument();
                doc.Load(part.GetStream());
            }
            return doc;
        }

        private string TargetOfRel(string partPath, string relationshipType)
        {
            XmlDocument docSlideRels = ReadXmlPart(partPath, false);
            XmlNode relationships = docSlideRels.ChildNodes[1];
            foreach (XmlNode relationship in relationships.ChildNodes)
            {
                if (relationship.Attributes["Type"].Value == relationshipType)
                {
                    return MakePptTargetPathAbsolute(relationship.Attributes["Target"].Value);
                }
            }
            return null;
        }

        private Guid BluePrintGuid(string notesSlidePath)
        {
            Uri uri = new Uri(notesSlidePath, UriKind.Relative);
            PackagePart part = m_originalPackage.GetPart(uri);
            XmlDocument notesDoc = new XmlDocument();
            notesDoc.PreserveWhitespace = true;
            notesDoc.Load(part.GetStream());
            XmlNodeList paragraphs = notesDoc.GetElementsByTagName("p", @"http://schemas.openxmlformats.org/drawingml/2006/main");
            string notesText = "";
            foreach (XmlNode para in paragraphs)
            {
                notesText += para.InnerText + "\n";
            }
            try
            {
                notesText = notesText.Replace('\r', '\n');      // Clean up
                string[] lines = notesText.Split('\n');
                foreach(var line in lines )
                {
                    if (line.StartsWith("ID="))
                    {
                        //                if (notesText.Contains(@"0bc70563-41f8-4777-8988-6e9382451e8b"))
                        //                    Console.WriteLine("found");
                        //                 return Guid.Parse(properties[0].Substring(3, Guid.Empty.ToString().Length));
                        return Guid.Parse(line.Substring(3));
                    }
                }
                throw new InvalidDataException();
            }
            catch
            {
                Console.WriteLine("Invalid Notes: " + notesText);
                return Guid.Empty;
            }
        }
        #endregion

        public SlideFromPresentation(Package original)
        {
            m_originalPackage = original;
            foreach (PackagePart part in m_originalPackage.GetParts())
            {
                string uriPath = part.Uri.ToString();
                if (uriPath.StartsWith(@"/ppt/") && uriPath.IndexOf('/',5)>0)
                    PptParts.Add(uriPath);
            }
            m_skipParts.Add(@"/docProps/thumbnail.jpeg");

            XmlDocument doc = ReadXmlPart(s_pathPrentationXmlRels, false);
            XmlNode relationships = doc.ChildNodes[1];
            foreach (XmlNode relationship in relationships.ChildNodes)
            {
                if (relationship.Attributes["Type"].Value == s_relTypePptSlide)
                {
                    SlideTargets[relationship.Attributes["Id"].Value] = @"/ppt/" + relationship.Attributes["Target"].Value;
                }
                else if (relationship.Attributes["Type"].Value == s_relTypePptMaster)
                {
                    MasterRelIds[@"/ppt/" + relationship.Attributes["Target"].Value] = relationship.Attributes["Id"].Value;
                }
                else if(relationship.Attributes["Type"].Value == s_relTypePptHandoutMaster)
                {
                    m_handoutMasterTarget = @"/ppt/" + relationship.Attributes["Target"].Value;
                }
                else if (relationship.Attributes["Type"].Value == s_relTypePptNotesMaster)
                {
                    m_notesMasterTarget = @"/ppt/" + relationship.Attributes["Target"].Value;
                }
            }

            doc = ReadXmlPart(s_pathPrentationXml, false);
            XmlNode presentation = doc.ChildNodes[1];
            foreach (XmlNode node in presentation.ChildNodes)
            {
                if (node.Name == "p:sldIdLst")
                {
                    foreach (XmlNode sldId in node.ChildNodes)
                    {
                        SlideRelIds[int.Parse(sldId.Attributes["id"].Value)] = sldId.Attributes["r:id"].Value;
                    }
                    break;
                }
            }
        }

        private void FillSkipList()
        {
            int slideId = CurrentBP.SlideId;
            if (!SlideRelIds.ContainsKey(slideId) || SlideRelIds[slideId] == null)
            {
                throw new IndexOutOfRangeException(@"Slide with ID " + slideId + " is not found");
            }
            m_partsReferencedBySlide.Clear();
            CurrentBP.SlideRelId = SlideRelIds[slideId];

            CurrentBP.SlideTarget = SlideTargets[CurrentBP.SlideRelId];
            string slideRels = RelsPathFromTarget(CurrentBP.SlideTarget);
            m_partsReferencedBySlide.Add(s_pathPrentationXmlRels);
            if(m_notesMasterTarget != null)
            {
                m_partsReferencedBySlide.Add(m_notesMasterTarget);
                m_partsReferencedBySlide.Add(RelsPathFromTarget(m_notesMasterTarget));
            }
            if (m_handoutMasterTarget != null)
            {
                m_partsReferencedBySlide.Add(m_handoutMasterTarget);
                m_partsReferencedBySlide.Add(RelsPathFromTarget(m_handoutMasterTarget));
            }
            m_partsReferencedBySlide.Add(CurrentBP.SlideTarget);
            m_partsReferencedBySlide.Add(slideRels);
            CurrentBP.LayoutTarget = TargetOfRel(slideRels, s_relTypePptLayout);
            CurrentBP.NotesTarget = TargetOfRel(slideRels, s_relTypePptNotes);
            CurrentBP.Guid = BluePrintGuid(CurrentBP.NotesTarget);
            string layoutRels = RelsPathFromTarget(CurrentBP.LayoutTarget);
            CurrentBP.MasterTarget = TargetOfRel(layoutRels, s_relTypePptMaster);
            CurrentBP.MasterRelId = MasterRelIds[CurrentBP.MasterTarget];
            m_partsReferencedBySlide.Add(CurrentBP.LayoutTarget);
            m_partsReferencedBySlide.Add(layoutRels);
            string masterRels = RelsPathFromTarget(CurrentBP.MasterTarget);
            XmlDocument docMasterRels = ReadXmlPart(masterRels, false);
            XmlNode relationships = docMasterRels.ChildNodes[1];
            foreach (XmlNode relationship in relationships.ChildNodes)
            {
                if (relationship.Attributes["Type"].Value == s_relTypePptLayout)
                {
                    string target = MakePptTargetPathAbsolute(relationship.Attributes["Target"].Value);
                    if (target == CurrentBP.LayoutTarget)
                    {
                        CurrentBP.LayoutRelId = relationship.Attributes["Id"].Value;
                        break;
                    }
                }
            }
            AllPartsReferenced(slideRels);
        }

        private void AllPartsReferenced(string relsPath )
        {
            ArrayList relsList = new ArrayList();
            relsList.Add(relsPath);
            if (m_handoutMasterTarget != null)
                relsList.Add(RelsPathFromTarget(m_handoutMasterTarget));
            if (m_notesMasterTarget != null)
                relsList.Add(RelsPathFromTarget(m_notesMasterTarget));
            int index = 0;
            while(index < relsList.Count)
            {
                string path = (string)relsList[index];
                index++;
                XmlDocument doc = ReadXmlPart(path, true);
                XmlNode relationships = doc.ChildNodes[1];
                foreach (XmlNode relationship in relationships.ChildNodes)
                {
                    string target = MakePptTargetPathAbsolute(relationship.Attributes["Target"].Value);
                    string relsTarget = RelsPathFromTarget(target);
                    bool fRelExists = m_originalPackage.PartExists(new Uri(relsTarget, UriKind.Relative));
                    if (!m_partsReferencedBySlide.Contains(target))
                    {
                        m_partsReferencedBySlide.Add(target);
                        if( fRelExists)
                            m_partsReferencedBySlide.Add(relsTarget);
                    }
                    if(fRelExists && !relsList.Contains(relsTarget))
                        relsList.Add(relsTarget);
                }
            }
        }

        #region GetModifiedXmlDoc
        private XmlDocument GetModifiedXmlDocForRels_Rels(PackagePart part)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(part.GetStream());
            XmlNode relationships = doc.ChildNodes[1];
            XmlNode thumbnail = null;
            foreach (XmlNode relationship in relationships.ChildNodes)
            {
                if (relationship.Attributes["Type"].Value == s_relTypeThumbnail)
                    thumbnail = relationship;
            }
            if (thumbnail != null)
                relationships.RemoveChild(thumbnail);
            return doc;
        }

        private XmlDocument GetModifiedXmlDocForPresentationXmlRels(PackagePart part)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(part.GetStream());
            XmlNode relationships = doc.ChildNodes[1];
            ArrayList nodesToBeRemoved = new ArrayList();
            foreach (XmlNode relationship in relationships.ChildNodes)
            {
                if (relationship.Attributes["Type"].Value == s_relTypePptSlide)
                {
                    if (relationship.Attributes["Id"].Value != CurrentBP.SlideRelId)
                        nodesToBeRemoved.Add(relationship);
                }
                else if(relationship.Attributes["Type"].Value == s_relTypePptMaster)
                {
                    if (relationship.Attributes["Id"].Value != CurrentBP.MasterRelId)
                        nodesToBeRemoved.Add(relationship);
                }
            }
            foreach (XmlNode node in nodesToBeRemoved)
            {
                relationships.RemoveChild(node);
            }
            return doc;
        }

        private XmlDocument GetModifiedXmlDocForMasterRels(PackagePart part)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(part.GetStream());
            XmlNode relationships = doc.ChildNodes[1];
            ArrayList nodesToBeRemoved = new ArrayList();
            foreach (XmlNode relationship in relationships.ChildNodes)
            {
                if (relationship.Attributes["Type"].Value == s_relTypePptLayout)
                {
                    string target = MakePptTargetPathAbsolute(relationship.Attributes["Target"].Value);
                    if (target != CurrentBP.LayoutTarget)
                        nodesToBeRemoved.Add(relationship);
                }
            }
            foreach (XmlNode node in nodesToBeRemoved)
            {
                relationships.RemoveChild(node);
            }
            return doc;
        }

        private XmlDocument GetModifiedXmlDocForPresentationXml(PackagePart part)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(part.GetStream());
            XmlNode presentation = doc.ChildNodes[1];
            XmlNode sldIdLst = null;
            XmlNode masterIdLst = null;
            foreach (XmlNode node in presentation.ChildNodes)
            {
                if (node.Name == "p:sldIdLst")
                {
                    sldIdLst = node;
                }
                else if (node.Name == "p:sldMasterIdLst")
                {
                    masterIdLst = node;
                }
            }
            ArrayList toBeRemoved = new ArrayList();
            foreach (XmlNode sldId in sldIdLst.ChildNodes)
            {
                if (int.Parse(sldId.Attributes["id"].Value) != CurrentBP.SlideId )
                    toBeRemoved.Add(sldId);
            }
            foreach (XmlNode sldId in toBeRemoved)
            {
                sldIdLst.RemoveChild(sldId);
            }
            toBeRemoved.Clear();
            foreach (XmlNode masterId in masterIdLst.ChildNodes)
            {
                if (masterId.Attributes["r:id"].Value != CurrentBP.MasterRelId)
                    toBeRemoved.Add(masterId);
            }
            foreach (XmlNode masterId in toBeRemoved)
            {
                masterIdLst.RemoveChild(masterId);
            }

            return doc;
        }

        private XmlDocument GetModifiedXmlDocForMasterXml(PackagePart part)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(part.GetStream());
            XmlNode master = doc.ChildNodes[1];
            XmlNode layoutIdLst = null;
            foreach (XmlNode node in master.ChildNodes)
            {
                if (node.Name == "p:sldLayoutIdLst")
                {
                    layoutIdLst = node;
                    break;
                }
            }
            ArrayList layoutIdNodesToBeRemoved = new ArrayList();
            foreach (XmlNode layoutId in layoutIdLst.ChildNodes)
            {
                if (layoutId.Attributes["r:id"].Value != CurrentBP.LayoutRelId)
                    layoutIdNodesToBeRemoved.Add(layoutId);
            }
            foreach (XmlNode sldId in layoutIdNodesToBeRemoved)
            {
                layoutIdLst.RemoveChild(sldId);
            }
            return doc;
        }

        public XmlDocument GetModifiedXmlDoc(PackagePart part)
        {
            if (part.Uri.OriginalString == s_pathRels_Rels)
                return GetModifiedXmlDocForRels_Rels(part);
            if (part.Uri.OriginalString == s_pathPrentationXml)
                return GetModifiedXmlDocForPresentationXml(part);
            if (part.Uri.OriginalString == s_pathPrentationXmlRels)
                return GetModifiedXmlDocForPresentationXmlRels(part);
            if (part.Uri.ToString() == RelsPathFromTarget(CurrentBP.MasterTarget))
                return GetModifiedXmlDocForMasterRels(part);
            if (part.Uri.ToString() == CurrentBP.MasterTarget)
                return GetModifiedXmlDocForMasterXml(part);
            return null;
        }
        #endregion

        private bool FSkipPart(string partPath)
        {
            if (m_skipParts.Contains(partPath))
                return true;
            if (PptParts.Contains(partPath) && !m_partsReferencedBySlide.Contains(partPath))
                return true;
            return false;
        }

        public void OutputSlide(int slideId, string outputPath)
        {
            CurrentBP = new BluePrint();
            CurrentBP.SlideId = slideId;
            FillSkipList();
            if (CurrentBP.Guid == Guid.Empty)
                return;
            m_outputPackage = Package.Open(outputPath + CurrentBP.Guid + ".pptx", FileMode.Create);
            foreach (PackagePart part in m_originalPackage.GetParts())
            {
                if (FSkipPart(part.Uri.OriginalString) )
                    continue;
                PackagePart clonedPart = m_outputPackage.CreatePart(part.Uri, part.ContentType, part.CompressionOption);
                XmlDocument modifiedDocument = GetModifiedXmlDoc(part);
                if (modifiedDocument != null)
                    modifiedDocument.Save(clonedPart.GetStream());
                else
                    part.GetStream().CopyTo(clonedPart.GetStream());
            }
            FlushAndClosePackage();
        }
    }

    class Program
    {
        static void RunOnDirectory(string sourceDir, string outputDir)
        {
            string directory = sourceDir;
            var pptxFiles = Directory.EnumerateFiles(directory, "*.pptx", SearchOption.AllDirectories);
            StreamWriter catalogWriter = File.CreateText(outputDir + "Catalog.txt");
            Parallel.ForEach(pptxFiles, (currentFile) =>
             {
                 Package original = null;
                 bool fOkay = false;
                 try
                 {
                     original = Package.Open(currentFile, FileMode.Open);
                     fOkay = true;
                 }
                 catch (System.IO.FileFormatException)
                 {
                     Console.WriteLine("Cannot open " + currentFile);
                 }
                 catch (System.UnauthorizedAccessException)
                 {
                     Console.WriteLine("Cannot access " + currentFile);
                 }
                 if ( fOkay )
                 {
                     Console.WriteLine(currentFile);
                     string fileName = currentFile.Substring(directory.Length + 1);
                     string[] segments = fileName.Split('\\');
                     string layout = segments[0];
                     string type = segments[1];
                     string crop = segments[2];
                     string aspect = segments[3];
                     string path = segments[4];
                     SlideFromPresentation single = new SlideFromPresentation(original);
                     string catalogLines = string.Empty;
                     foreach (int slideId in single.SlideRelIds.Keys)
                     {
                         single.OutputSlide(slideId, outputDir);
                         catalogLines += single.CurrentBP.Guid + " " + fileName + catalogWriter.NewLine;
                     }
                     catalogWriter.WriteLine(catalogLines);
                 }
             }
            );
            catalogWriter.Flush();
        }

        static void RunOneFile(string sourceFile, string outputDir)
        {
            Package original = Package.Open(sourceFile, FileMode.Open);

            SlideFromPresentation single = new SlideFromPresentation(original);
            foreach (int slideId in single.SlideRelIds.Keys)
                single.OutputSlide(slideId, outputDir);
        }

        // Settings:
        // C:\Users\dzhang\git\designer.blueprints\Blueprints\Active C:\Users\dzhang\Desktop\Clone\
        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            if (Directory.Exists(args[0]))
                RunOnDirectory(args[0], args[1]);
            else
                RunOneFile(args[0], args[1]);
            watch.Stop();
            Console.WriteLine("Total Execution Time in ms: " + watch.ElapsedMilliseconds);
            Console.WriteLine("Press any key ...");
            Console.ReadKey();
        }
    }
}
