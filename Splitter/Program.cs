using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitter
{
    class Program
    {
        //  --------------------------- CopyStream ---------------------------
        /// <summary>
        ///   Copies data from a source stream to a target stream.</summary>
        /// <param name="source">
        ///   The source stream to copy from.</param>
        /// <param name="target">
        ///   The destination stream to copy to.</param>
        private static void CopyStream(Stream source, Stream target)
        {
            const int bufSize = 0x1000;
            byte[] buf = new byte[bufSize];
            int bytesRead = 0;
            while ((bytesRead = source.Read(buf, 0, bufSize)) > 0)
                target.Write(buf, 0, bytesRead);
        }// end:CopyStream()
        static void Main(string[] args)
        {
            Package package = Package.Open(args[0], System.IO.FileMode.Open);
            Console.WriteLine(package.PackageProperties.Creator);

            Package clone = Package.Open(args[1], System.IO.FileMode.Create);
            PackagePartCollection parts = package.GetParts();
            foreach( PackagePart part in parts)
            {
                Console.WriteLine(part.Uri);

                PackagePart clonedPart = clone.CreatePart(part.Uri, part.ContentType, part.CompressionOption);
                CopyStream(part.GetStream(), clonedPart.GetStream());
            }
            clone.Flush();
            clone.Close();
            package.Close();
        }
    }
}
