using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILogging;

namespace NLogLogger {
    public class Logger : NLog.Logger, ILog {

        #region ILog-Interface; Manche Methoden werden von NLog.Logger direkt implementiert, manche werden bewusst verdeckt
        ////public bool IsTraceEnabled {
        ////    get {
        ////        throw new NotImplementedException();
        ////    }
        ////}
        ////public bool IsDebugEnabled {
        ////    get {
        ////        throw new NotImplementedException();
        ////    }
        ////}
        ////public bool IsInfoEnabled {
        ////    get {
        ////        throw new NotImplementedException();
        ////    }
        ////}
        ////public bool IsWarnEnabled {
        ////    get {
        ////        throw new NotImplementedException();
        ////    }
        ////}
        ////public bool IsErrorEnabled {
        ////    get {
        ////        throw new NotImplementedException();
        ////    }
        ////}
        ////public bool IsFatalEnabled {
        ////    get {
        ////        throw new NotImplementedException();
        ////    }
        ////}



        ////public void Trace(string message) {
        ////    throw new NotImplementedException();
        ////}

        public new void Trace(string message, Exception exception) {
            base.Trace(exception, message);
        }

        public void TraceFormat(string format, params object[] args) {
            base.Trace(format, args);
        }

        public void TraceFormat(string format, Exception exception, params object[] args) {
            base.Trace(exception, format, args);
        }

        ////public void Info(object message) {
        ////    throw new NotImplementedException();
        ////}

        public new void Info(string message, Exception exception) {
            base.Info(exception, message);
        }

        public void InfoFormat(string format, params object[] args) {
            base.Info(format, args);
        }

        public void InfoFormat(string format, Exception exception, params object[] args) {
            base.Info(exception, format, args);
        }

        ////public void Debug(object message) {
        ////    throw new NotImplementedException();
        ////}

        public new void Debug(string message, Exception exception) {
            base.Debug(exception, message);
        }

        public void DebugFormat(string format, params object[] args) {
            base.Debug(format, args);
        }

        public void DebugFormat(string format, Exception exception, params object[] args) {
            base.Debug(exception, format, args);
        }

        ////public void Warn(object message) {
        ////    throw new NotImplementedException();
        ////}

        public new void Warn(string message, Exception exception) {
            base.Warn(exception, message);
        }

        public void WarnFormat(string format, params object[] args) {
            base.Warn(format, args);
        }

        public void WarnFormat(string format, Exception exception, params object[] args) {
            base.Warn(exception, format, args);
        }

        ////public void Error(object message) {
        ////    throw new NotImplementedException();
        ////}

        public new void Error(string message, Exception exception) {
            base.Error(exception, message);
        }

        public void ErrorFormat(string format, params object[] args) {
            base.Error(format, args);
        }

        public void ErrorFormat(string format, Exception exception, params object[] args) {
            base.Error(exception, format, args);
        }

        ////public void Fatal(object message) {
        ////    throw new NotImplementedException();
        ////}

        public new void Fatal(string message, Exception exception) {
            base.Fatal(exception, message);
        }

        public void FatalFormat(string format, params object[] args) {
            base.Fatal(format, args);
        }

        public void FatalFormat(string format, Exception exception, params object[] args) {
            base.Fatal(exception, format, args);
        }

        #endregion

        public List<NLog.Targets.Target> GetAllTargets() {
            return NLog.LogManager.Configuration.AllTargets.ToList();
        }

        public List<TTarget> GetTargets<TTarget>() where TTarget : NLog.Targets.Target {
            return NLog.LogManager.Configuration.AllTargets.Where(w => w.GetType() == typeof(TTarget))
                .Select(s=> s as TTarget).ToList();
        }
    }
}
