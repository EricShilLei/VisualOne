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
using System.Threading.Tasks;

namespace Splitter
{
    internal class SlideFromPresentation
    {
        private Package m_originalPackage;
        private ArrayList m_pptParts = new ArrayList();
        private string m_handoutMasterTarget = null;

        private Package m_outputPackage;
        private ArrayList m_skipParts = new ArrayList();
        private ArrayList m_partsReferencedBySlide = new ArrayList();
        private Dictionary<string, string> m_slideTargets = new Dictionary<string, string>();
        private int m_slideId = -1;
        private Guid m_selectedBPGuid = Guid.Empty;
        private string m_slideTargetRelId = null;
        private string m_layoutTargetRelId = null;
        private string m_selectedSlideLayoutTarget = null;
        private string m_selectedSlideMasterTarget = null;
        private string m_selectedSlideNotesTarget = null;

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
        #endregion

        public Dictionary<int, string> SlideRelIds { get; } = new Dictionary<int, string>();

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

        private Guid LayoutGuid(string notesSlidePath)
        {
            XmlDocument notesDoc = ReadXmlPart(notesSlidePath, false);
            string notesText = notesDoc.InnerText;
            try
            {
                if (notesText.StartsWith("ID="))
                {
                    //                if (notesText.Contains(@"0bc70563-41f8-4777-8988-6e9382451e8b"))
                    //                    Console.WriteLine("found");
                    notesText = notesText.Replace('\r', '\n');      // Clean up
                    string[] properties = notesText.Split('\n');
                    //                 return Guid.Parse(properties[0].Substring(3, Guid.Empty.ToString().Length));
                    return Guid.Parse(properties[0].Substring(3));
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
                    m_pptParts.Add(uriPath);
            }
            m_skipParts.Add(@"/docProps/thumbnail.jpeg");

            XmlDocument doc = ReadXmlPart(s_pathPrentationXmlRels, false);
            XmlNode relationships = doc.ChildNodes[1];
            foreach (XmlNode relationship in relationships.ChildNodes)
            {
                if (relationship.Attributes["Type"].Value == s_relTypePptSlide)
                {
                    m_slideTargets[relationship.Attributes["Id"].Value] = @"/ppt/" + relationship.Attributes["Target"].Value;
                }
                else if(relationship.Attributes["Type"].Value == s_relTypePptHandoutMaster)
                {
                    m_handoutMasterTarget = @"/ppt/" + relationship.Attributes["Target"].Value;
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
            if (!SlideRelIds.ContainsKey(m_slideId) || SlideRelIds[m_slideId] == null)
            {
                throw new IndexOutOfRangeException(@"Slide with ID " + m_slideId + " is not found");
            }
            m_partsReferencedBySlide.Clear();
            m_layoutTargetRelId = null;
            m_selectedSlideLayoutTarget = null;
            m_selectedSlideMasterTarget = null;
            m_selectedSlideNotesTarget = null;
            m_slideTargetRelId = SlideRelIds[m_slideId];

            string slideTarget = m_slideTargets[m_slideTargetRelId];
            string slideRels = RelsPathFromTarget(slideTarget);
            m_partsReferencedBySlide.Add(s_pathPrentationXmlRels);
            if(m_handoutMasterTarget != null)
            {
                m_partsReferencedBySlide.Add(m_handoutMasterTarget);
                m_partsReferencedBySlide.Add(RelsPathFromTarget(m_handoutMasterTarget));
            }
            m_partsReferencedBySlide.Add(slideTarget);
            m_partsReferencedBySlide.Add(slideRels);
            m_selectedSlideLayoutTarget = TargetOfRel(slideRels, s_relTypePptLayout);
            m_selectedSlideNotesTarget = TargetOfRel(slideRels, s_relTypePptNotes);
            m_selectedBPGuid = LayoutGuid(m_selectedSlideNotesTarget);
            string layoutRels = RelsPathFromTarget(m_selectedSlideLayoutTarget);
            m_selectedSlideMasterTarget = TargetOfRel(layoutRels, s_relTypePptMaster);
            m_partsReferencedBySlide.Add(m_selectedSlideLayoutTarget);
            m_partsReferencedBySlide.Add(layoutRels);
            string masterRels = RelsPathFromTarget(m_selectedSlideMasterTarget);
            XmlDocument docMasterRels = ReadXmlPart(masterRels, false);
            XmlNode relationships = docMasterRels.ChildNodes[1];
            foreach (XmlNode relationship in relationships.ChildNodes)
            {
                if (relationship.Attributes["Type"].Value == s_relTypePptLayout)
                {
                    string target = MakePptTargetPathAbsolute(relationship.Attributes["Target"].Value);
                    if (target == m_selectedSlideLayoutTarget)
                    {
                        m_layoutTargetRelId = relationship.Attributes["Id"].Value;
                        break;
                    }
                }
            }
            AllPartsReferenced(slideRels);
        }

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
            if(thumbnail != null)
                relationships.RemoveChild(thumbnail);
            return doc;
        }

        private void AllPartsReferenced(string relsPath )
        {
            ArrayList relsList = new ArrayList();
            relsList.Add(relsPath);
            if (m_handoutMasterTarget != null)
                relsList.Add(RelsPathFromTarget(m_handoutMasterTarget));
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
                    if (relationship.Attributes["Id"].Value != m_slideTargetRelId)
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
                    if (target != m_selectedSlideLayoutTarget)
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
            foreach (XmlNode node in presentation.ChildNodes)
            {
                if (node.Name == "p:sldIdLst")
                {
                    sldIdLst = node;
                    break;
                }
            }
            ArrayList sldIdNodesToBeRemoved = new ArrayList();
            foreach (XmlNode sldId in sldIdLst.ChildNodes)
            {
                if (int.Parse(sldId.Attributes["id"].Value) != m_slideId )
                    sldIdNodesToBeRemoved.Add(sldId);
            }
            foreach (XmlNode sldId in sldIdNodesToBeRemoved)
            {
                sldIdLst.RemoveChild(sldId);
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
                if (layoutId.Attributes["r:id"].Value != m_layoutTargetRelId)
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
            if (part.Uri.ToString() == RelsPathFromTarget(m_selectedSlideMasterTarget))
                return GetModifiedXmlDocForMasterRels(part);
            if (part.Uri.ToString() == m_selectedSlideMasterTarget)
                return GetModifiedXmlDocForMasterXml(part);
            return null;
        }

        private bool FSkipPart(string partPath)
        {
            if (m_skipParts.Contains(partPath))
                return true;
            if (m_pptParts.Contains(partPath) && !m_partsReferencedBySlide.Contains(partPath))
                return true;
            return false;
        }

        public void OutputSlide(int slideId, string outputPath)
        {
            m_slideId = slideId;
            FillSkipList();
            if (m_selectedBPGuid == Guid.Empty)
                return;
            m_outputPackage = Package.Open(outputPath + m_selectedBPGuid + ".pptx", FileMode.Create);
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
                 if( fOkay )
                 {
                     Console.WriteLine(currentFile);

                     SlideFromPresentation single = new SlideFromPresentation(original);
                     foreach (int slideId in single.SlideRelIds.Keys)
                         single.OutputSlide(slideId, outputDir);
                 }
             }
            );

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
            if (Directory.Exists(args[0]))
                RunOnDirectory(args[0], args[1]);
            else
                RunOneFile(args[0], args[1]);
            Console.ReadKey();
        }
    }
}
