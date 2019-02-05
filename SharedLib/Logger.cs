using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharedLib
{
    public class Log
    {
        public enum Level
        {
            Info,
            Warning,
            Error
        }

        static public void TraceTag(Log.Level level, string formatString, params object[] data)
        {
            string message = string.Format(formatString, data);
            if (level == Level.Error)
            {
                MessageBox.Show(message, "Error");
            }
            Console.WriteLine(message);
        }
    }
}