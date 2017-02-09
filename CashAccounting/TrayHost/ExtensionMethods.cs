using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrayHost {
    public static class ExtensionMethods {
        public static NLog.LogLevel ToNLogLevel(this ILogging.Level lev) {
            return NLog.LogLevel.FromString(Enum.GetName(typeof(ILogging.Level), lev));
        }

    }
}
