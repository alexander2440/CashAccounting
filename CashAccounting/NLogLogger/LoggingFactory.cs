using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILogging;
using NLog;

namespace NLogLogger {
    public class LoggingFactory : ILoggingFactory {
        public ILog GetCurrentClassLogger() {
            StackFrame caller = new StackFrame(1);
            string classname = caller.GetMethod().DeclaringType.Name;
            return GetLogger(classname);
        }

        public ILog GetLogger(string loggerName) {
            return (ILog)NLog.LogManager.GetLogger(loggerName, typeof(NLogLogger.Logger));
        }


    }
}
