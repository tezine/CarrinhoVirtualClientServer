using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Codes {
    public class ClientLogger {
        static public void LogError(string msg) {
            Console.WriteLine(msg);
            Debug.WriteLine(msg);
        }

        static public void LogDebug(string msg) {
            Console.WriteLine(msg);
            Debug.WriteLine(msg);
        }

        static public void LogError(Exception ex) {
            Console.WriteLine(ex);
            Debug.WriteLine(ex);
        }
    }
}
