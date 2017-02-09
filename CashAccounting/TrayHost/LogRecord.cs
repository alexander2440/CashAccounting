using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrayHost {
    public class LogRecord {
        public ILogging.Level Level { get; set; }
        public String Message { get; set; }

        public override string ToString() {
            return Message;
        }
    }
}
