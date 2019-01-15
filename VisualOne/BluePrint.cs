using System;

namespace VisualOne
{
    public class BluePrint
    {
        public Guid Guid { get; set; }

        public string Source { get; set; }

        public string Layout { get; set; }

        public string Type { get; set; }

        public string CropNonCrop { get; set; }

        public string AspectRaio;

        public string variant;

        public double KeptRate { get; set; }

        public UInt32 Kept { get; set; }

        public UInt32 Seen { get; set; }

        public string PngPath;

        public string FlattendPptxPath;

        public string OriginalPath;

        public string[] OtherProperties;
    }

    public struct TempSeenKeptRecord
    {
        public UInt32 seen;
        public UInt32 kept;
    }
}
