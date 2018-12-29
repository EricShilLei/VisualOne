using System;

namespace VisualOne
{
    public class BluePrint
    {
        public Guid guid { get; set; }

        public string source { get; set; }

        public string layout { get; set; }

        public string type { get; set; }

        public string cropNonCrop { get; set; }

        public string aspectRaio;

        public string variant;

        public double keptRate { get; set; }

        public UInt32 kept { get; set; }

        public UInt32 seen { get; set; }

        public string pngPath;

        public string flatPath;

        public string originalPath;

        public string[] otherProperties;
    }

    public struct TempSeenKeptRecord
    {
        public UInt32 seen;
        public UInt32 kept;
    }
}
