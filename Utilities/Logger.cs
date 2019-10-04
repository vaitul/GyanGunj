using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Logger
    {
        private static string LogFolder = "App_Log";
        private static string LogFile
        {
            get
            {
                return DateTime.Now.Date.ToString("dd-MM-yy") + ".log";
            }
        }
        public static string FullPath
        {
            get
            {
                return LogFolder + "/" + LogFile;
            }
        }
        public static void LogError(string text)
        {
            if (!Directory.Exists(LogFolder))
                Directory.CreateDirectory(LogFolder);
            if (!File.Exists(FullPath))
                File.Create(FullPath).Close();
            File.WriteAllText(FullPath, "\n"+DateTime.Now.ToString() + "\n-----------------------\n" + text + "\n" + File.ReadAllText(FullPath));
        }
        public static void LogText(string text)
        {
            if (!Directory.Exists(LogFolder))
                Directory.CreateDirectory(LogFolder);
            if (!File.Exists(FullPath))
                File.Create(FullPath).Close();
            File.WriteAllText(FullPath, "\n"+DateTime.Now.ToString() + "   " + text + "\n-----------------------\n" + File.ReadAllText(FullPath));

        }
    }
}
