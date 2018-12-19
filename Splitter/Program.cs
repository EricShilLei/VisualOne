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
    class Presentation
    {
        private Package m_package;
        private bool m_readOnly;

        public Presentation(string path, FileMode packageMode)
        {
            m_package = Package.Open(path, packageMode);
            m_readOnly = (packageMode == FileMode.Open);
        }

        public void FlushAndClose()
        {
            if(!m_readOnly)
            {
                m_package.Flush();
                m_package.Close();
            }
        }

        public Package package { get { return m_package; } }
        public PackagePartCollection parts { get { return package.GetParts(); } }
        public PackagePart appXml { get { return package.GetPart(new Uri(@"/docProps/app.xml", UriKind.Relative) ); } }

        public bool FShouldClone(PackagePart part)
        {
            return part.Uri.OriginalString != @"/docProps/thumbnail.jpeg";
        }

        private Stream GetModifiedStreamFor_rels_rels(PackagePart part)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(part.GetStream());
            XmlNode relationships = doc.ChildNodes[1];
            XmlNode thumbnail = null;
            foreach (XmlNode relationship in relationships.ChildNodes)
            {
                if (relationship.Attributes["Type"].Value == @"http://schemas.openxmlformats.org/package/2006/relationships/metadata/thumbnail")
                    thumbnail = relationship;
            }
            relationships.RemoveChild(thumbnail);
            Stream modified = new MemoryStream();
            doc.Save(modified);
            modified.Flush();
            modified.Seek(0, SeekOrigin.Begin);
            return modified;
        }

        private Stream GetModifiedStreamFor_ppt_presentationxml(PackagePart part)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(part.GetStream());
            XmlNode presentation = doc.ChildNodes[1];
            XmlNode sldIdLst = null;
            foreach( XmlNode node in presentation.ChildNodes)
            {
                if (node.Name == "p:sldIdLst")
                {
                    sldIdLst = node;
                    break;
                }
            }
            bool fPreserve = true;
            ArrayList sldIdNodesToBeRemoved = new ArrayList();
            foreach (XmlNode sldId in sldIdLst.ChildNodes)
            {
                if (!fPreserve)
                    sldIdNodesToBeRemoved.Add(sldId);
                if (fPreserve)
                    fPreserve = false;
            }
            foreach (XmlNode sldId in sldIdNodesToBeRemoved)
            {
                sldIdLst.RemoveChild(sldId);
            }
            Stream modified = new MemoryStream();
            doc.Save(modified);
            modified.Flush();
            modified.Seek(0, SeekOrigin.Begin);
            return modified;
        }

        public Stream GetModifiedStream(PackagePart part)
        {
            if (part.Uri.OriginalString == @"/_rels/.rels")
                return GetModifiedStreamFor_rels_rels(part);
            if (part.Uri.OriginalString == @"/ppt/presentation.xml")
                return GetModifiedStreamFor_ppt_presentationxml(part);
            return part.GetStream();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Presentation original = new Presentation(args[0], FileMode.Open);
            Console.WriteLine(original.package.PackageProperties.Creator);

            Presentation clone = new Presentation(args[1], FileMode.Create);

            XmlDocument appDoc = new XmlDocument();
            foreach ( PackagePart part in original.parts)
            {
                Console.WriteLine(part.Uri);
                if (!original.FShouldClone(part))
                    continue;
                PackagePart clonedPart = clone.package.CreatePart(part.Uri, part.ContentType, part.CompressionOption);
                original.GetModifiedStream(part).CopyTo(clonedPart.GetStream());
            }
            clone.FlushAndClose();
            Uri appXml = new Uri(@"/docProps/app.xml", UriKind.Relative);
            PackagePart appXmlPart = original.appXml;
            appDoc.Load(appXmlPart.GetStream());
        }
    }
}
