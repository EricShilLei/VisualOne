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
 
        public string aspectRaio { get; set; }

        public string path;

        public string[] otherProperties;
    }
}
