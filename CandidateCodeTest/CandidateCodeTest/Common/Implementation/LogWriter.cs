using CandidateCodeTest.Common.Interfaces;
using System;
using System.IO;

namespace CandidateCodeTest.Common
{
    public class LogWriter : ILogWriter
    {
        public LogWriter(string logMessage)
        {
            LogWrite(logMessage);
        }
        public void LogWrite(string logMessage)
        {
            var path = Directory.CreateDirectory
                                            (Path.Combine
                                            (Environment.GetFolderPath
                                            (Environment.SpecialFolder.Desktop), "Logs"));
            try
            {
                using StreamWriter streamWriter = File.AppendText(path.FullName + "\\" + "log.txt"); //storing all logs like details, exceptions here in one file only for now

                AddLogMessage(logMessage, streamWriter);
            }
            catch (Exception)
            {
                throw;
            }

        }
        private static void AddLogMessage(string logMessage, TextWriter msgWriter)
        {
            try
            {
                msgWriter.Write("\r\nLog Entry : ");
                msgWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                msgWriter.WriteLine("  :");
                msgWriter.WriteLine("  :{0}", logMessage);
                msgWriter.WriteLine("-------------------------------");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
