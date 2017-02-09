using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILogging {
    public interface ILoggingFactory {

        ILog GetCurrentClassLogger();
        ILog GetLogger(string loggerName);

        void ShutdownLogging();

    }
}
