using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TazorLib.Codes {
    public class Logger {

        static public void LogError(object o, string msg) {
            Console.WriteLine(msg);
            Debug.WriteLine(msg);
        }

        static public void LogWarning(object o, string msg) {
            Console.WriteLine(msg);
            Debug.WriteLine(msg);
        }

        static public void LogDebug(object o, string msg) {
            Console.WriteLine(msg);
            Debug.WriteLine(msg);
        }
    }
}
