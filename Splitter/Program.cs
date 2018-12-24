using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace Splitter
{
    internal class SlideFromPresentation
    {
        private Package m_originalPackage;
        private ArrayList m_pptParts = new ArrayList();

        private Package m_outputPackage;
        private ArrayList m_skipParts = new ArrayList();
        private ArrayList m_partsReferencedBySlide = new ArrayList();
        private Dictionary<string, string> m_slideTargets = new Dictionary<string, string>();
        private int m_slideId = -1;
        private string m_slideTargetRelId = null;
        private string m_layoutTargetRelId = null;
        private string m_selectedSlideLayoutTarget = null;
        private string m_selectedSlideMasterTarget = null;

        private void FlushAndClosePackage()
        {
            if (m_outputPackage != null)
            {
                m_outputPackage.Flush();
                m_outputPackage.Close();
                m_outputPackage = null;
            }
        }

        private string MakePptTargetPathAbsolute(string target)
        {
            string path = target;
            if (target.StartsWith(@"../"))
                path = target.Substring(3);
            path = @"/ppt/" + path;
            return path;
        }

        public SlideFromPresentation(Package original)
        {
            m_originalPackage = original;
            foreach (PackagePart part in m_originalPackage.GetParts())
            {
                Console.WriteLine(part.Uri);
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

        private string TargetOfRel( string partPath, string relationshipType)
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
            m_slideTargetRelId = SlideRelIds[m_slideId];

            string slideTarget = m_slideTargets[m_slideTargetRelId];
            string slideRels = RelsPathFromTarget(slideTarget);
            m_partsReferencedBySlide.Add(s_pathPrentationXmlRels);
            m_partsReferencedBySlide.Add(slideTarget);
            m_partsReferencedBySlide.Add(slideRels);
            m_selectedSlideLayoutTarget = TargetOfRel(slideRels, s_relTypePptLayout);
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

        static private string s_pathRels_Rels = RelsPathFromTarget("");
        static private string s_pathPrentationXml = @"/ppt/presentation.xml";
        static private string s_pathPrentationXmlRels = RelsPathFromTarget(s_pathPrentationXml);
        static private string s_relTypePptSlide = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide";
        static private string s_relTypePptLayout = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideLayout";
        static private string s_relTypePptMaster = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideMaster";
        static private string s_relTypeThumbnail = @"http://schemas.openxmlformats.org/package/2006/relationships/metadata/thumbnail";

        public Dictionary<int, string> SlideRelIds { get; } = new Dictionary<int, string>();

        static private Stream XmlDocToStream(XmlDocument doc)
        {
            Stream modified = new MemoryStream();
            doc.Save(modified);
            modified.Flush();
            modified.Seek(0, SeekOrigin.Begin);
            return modified;
        }

        private Stream GetModifiedStreamForRels_Rels(PackagePart part)
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
            relationships.RemoveChild(thumbnail);
            return XmlDocToStream(doc);
        }

        static string RelsPathFromTarget(string slideTarget)
        {
            int lastSlashPos = slideTarget.LastIndexOf('/');
            string localPath = "";
            string folderPath = "";
            if ( lastSlashPos < 0)
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

        XmlDocument ReadXmlPart(string partPath, bool fModified)
        {
            Uri uri = new Uri(partPath, UriKind.Relative);
            PackagePart part = m_originalPackage.GetPart(uri);
            XmlDocument doc = new XmlDocument();
            doc.Load(fModified ? GetModifiedStream(part) : part.GetStream());
            return doc;
        }

        private void AllPartsReferenced(string relsPath )
        {
            ArrayList relsList = new ArrayList();
            relsList.Add(relsPath);
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

        private Stream GetModifiedStreamForPresentationXmlRels(PackagePart part)
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
            return XmlDocToStream(doc);
        }

        private Stream GetModifiedStreamForMasterRels(PackagePart part)
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
            return XmlDocToStream(doc);
        }

        private Stream GetModifiedStreamForPresentationXml(PackagePart part)
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
            return XmlDocToStream(doc);
        }

        private Stream GetModifiedStreamForMasterXml(PackagePart part)
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
            return XmlDocToStream(doc);
        }

        public Stream GetModifiedStream(PackagePart part)
        {
            if (part.Uri.OriginalString == s_pathRels_Rels)
                return GetModifiedStreamForRels_Rels(part);
            if (part.Uri.OriginalString == s_pathPrentationXml)
                return GetModifiedStreamForPresentationXml(part);
            if (part.Uri.OriginalString == s_pathPrentationXmlRels)
                return GetModifiedStreamForPresentationXmlRels(part);
            if (part.Uri.ToString() == RelsPathFromTarget(m_selectedSlideMasterTarget))
                return GetModifiedStreamForMasterRels(part);
            if (part.Uri.ToString() == m_selectedSlideMasterTarget)
                return GetModifiedStreamForMasterXml(part);
            return part.GetStream();
        }

        private bool FSkipPart(string partPath)
        {
            if (m_skipParts.Contains(partPath))
                return true;
            if (m_pptParts.Contains(partPath) && !m_partsReferencedBySlide.Contains(partPath))
                return true;
            return false;
        }

        public void SelectSlide(int slideId, string outputPath)
        {
            m_slideId = slideId;
            m_outputPackage = Package.Open(outputPath + slideId + ".pptx", FileMode.Create);
            FillSkipList();
            foreach (PackagePart part in m_originalPackage.GetParts())
            {
                if (FSkipPart(part.Uri.OriginalString) )
                    continue;
                PackagePart clonedPart = m_outputPackage.CreatePart(part.Uri, part.ContentType, part.CompressionOption);
                GetModifiedStream(part).CopyTo(clonedPart.GetStream());
            }
            FlushAndClosePackage();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Package original = Package.Open(args[0], FileMode.Open);
            Console.WriteLine(original.PackageProperties.Creator);

            SlideFromPresentation single = new SlideFromPresentation(original);
            foreach(int slideId in single.SlideRelIds.Keys)
                single.SelectSlide(slideId, args[1]);
        }
    }
}
