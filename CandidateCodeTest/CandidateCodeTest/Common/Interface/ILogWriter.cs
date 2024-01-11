using System;
using System.Collections.Generic;
using System.Text;

namespace CandidateCodeTest.Common.Interfaces
{
    public interface ILogWriter
    {

        /// <summary>
        /// This file is used to maintain log in the current assembly
        /// </summary>
        /// <param name="logMessage"></param>
        void LogWrite(string logMessage);
    }
}
