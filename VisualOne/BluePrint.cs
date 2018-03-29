using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualOne
{
    public class BluePrint
    {
        public string guid { get; set; }

        public string source { get; set; }

        public string layout { get; set; }

        public string type { get; set; }

        public string cropNonCrop { get; set; }

        public string aspectRaio;

        public string variant;

        public double keptRate { get; set; }

        public UInt32 kept { get; set; }

        public UInt32 seen { get; set; }

        public string path;

        public string[] otherProperties;
    }

    public struct TempSeenKeptRecord
    {
        public UInt32 seen;
        public UInt32 kept;
    }
}
