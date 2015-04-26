// DebuggingHelper.cs
// Introduced: 2015-04-26
// Last edited: 2015-04-26
// Edited by:
// G�ran F

using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.IO;

namespace Synced.Static_Classes
{
    static class DebuggingHelper
    {
        static Dictionary<string, object> mInformation = new Dictionary<string, object>();
        static List<string> mLog = new List<string>();
        const int C_MAX_ENTRIES_IN_LOG = 100000;
        const int C_REMOVAL_CHUNK_OF_ENTRIES_IN_LOG_WHEN_FULL = 1000;
        public static void Add(string _text, object _value)
        {
            try { mInformation.Add(_text, _value); }
            catch { Trace.WriteLine("Error in Debugger. Can't add information."); }
        }
        public static void Update()
        {
            Console.Clear();
            Console.WriteLine("Infos: " + mInformation.Count.ToString() + ", Logs: " + mLog.Count.ToString());
            foreach (var item in mInformation)
            {
                Console.WriteLine(item.Key.ToString() + item.Value.ToString());
            }
            mInformation.Clear();
        }

        public static void AddLog(string _text, object _value)
        {
            try
            {
                mLog.Add( DateTime.Now.ToString("yyyy-MM-dd/HH:mm:ss.fff") + ": " + _text + " (" + _value.ToString() +")");
                if (mLog.Count > C_MAX_ENTRIES_IN_LOG)
                {
                    mLog.RemoveRange(0, C_REMOVAL_CHUNK_OF_ENTRIES_IN_LOG_WHEN_FULL);
                }
            }
            catch
            {
                Trace.WriteLine("Error in Debugger. Can't add to log.");
            }
        }

        public static void ClearLog()
        {
            mLog.Clear();
            AddLog("Log Restarted!", "n/a");
        }

        public static void SaveLog(string _FileName)
        {
            string fileName;
            fileName = _FileName;
            if ("" == fileName)
            {
                fileName = "DebuggingHelper_log.txt";
            }
            
            FileStream logFileStream = new FileStream(fileName, FileMode.Create);
            StreamWriter logFileStreamWriter = new StreamWriter(logFileStream);

            foreach (var s in mLog)
            {
                logFileStreamWriter.WriteLine(s); 
            }
            logFileStreamWriter.Close();
        }
    }
}
