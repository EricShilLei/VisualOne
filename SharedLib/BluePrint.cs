using System;
using System.Collections.Generic;
using System.IO;

namespace SharedLib
{
    public class BluePrint : IComparable<BluePrint>
    {
        public Guid Guid { get; set; }

        public string Source { get; set; }

        public string Theme { get; set; }

        public string Variant { get; set; }

        public string Layout { get; set; }

        public string Type { get; set; }

        public string CropNonCrop { get; set; }

        public string AspectRaio;

        public string variant;

        public double KeptRate { get; set; }

        public UInt32 Kept { get; set; }

        public UInt32 Seen { get; set; }

        public uint SlideIndex;

        public string PngPath;

        public string FlattendPptxPath;

        public string CloneBPID;

        public string OriginalPath;

        public string[] OtherProperties;



        public int CompareTo(BluePrint compareBP)
        {
            if (compareBP == null)
                return 1;
            return this.Guid.CompareTo(compareBP.Guid);
        }
    }

    public struct TempSeenKeptRecord
    {
        public UInt32 seen;
        public UInt32 kept;
    }
}
