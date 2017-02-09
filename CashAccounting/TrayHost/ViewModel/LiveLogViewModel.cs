using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;
using ILogging;


namespace TrayHost.ViewModel {
    public class LiveLogViewModel : BaseViewModel {

        private const int C_MAXLINESINGUI = 2000;

        private static LiveLogViewModel SingeltonInstance = null;
        //private List<LoggingRule> ViewModelRules;


        private ObservableCollection<LogRecord> _LogLines = new ObservableCollection<LogRecord>();
        public ObservableCollection<LogRecord> LogLines {
            get { return _LogLines; }
            set { ChangeProperty(value); }
        }


        private string _NlogRulePattern = "*";
        public string NlogRulePattern {
            get { return _NlogRulePattern; }
            set {
                if(ChangeProperty(value)) {
                    ReconficureRulePattern(value);
                }
            }
        }

        private Level _Level = Level.Debug;
        public Level Level {
            get { return _Level; }
            internal set {
                ////_Level = value;
                if(ChangeProperty(value)) {
                    ReconficureLogLevel(Level.ToNLogLevel());
                }
            }
        }


        public LiveLogViewModel() {
            try {
                SingeltonInstance = this;
                //var target = NLog.LogManager.Configuration.FindTargetByName("ViewModel");
                //ViewModelRules = NLog.LogManager.Configuration.LoggingRules.Where(r => r.Targets.Contains(target)).ToList();
                ReconficureLogLevel(Level.ToNLogLevel());
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        // Method-Callback target konfiguriert in NLog.config
        public static void LogMethod(string level, string message) {
            SingeltonInstance?.LogViewModel(level, message);
        }

        // Jeder Aufruf des ViewModel targets wird ins ViewModel eingetragen. (Log Level Checks passieren ja schon im NLog Framework). 
        private void LogViewModel(string level, string message) {
            NLog.LogLevel nl = NLog.LogLevel.FromString(level);
            Level myLev = FromNlogLevel(nl);

            Application.Current.Dispatcher.BeginInvoke(
                new Action(() => {
                    if(LogLines.Count > C_MAXLINESINGUI) {
                        LogLines.Clear();
                        LogLines.Add(new LogRecord() { Level = Level.Error, Message = $"******* GUI deleted Log lines after {C_MAXLINESINGUI} lines! *******" });
                    }
                    LogLines.Add(new LogRecord() { Level = myLev, Message = message });

                }
                ));
        }

        private void ReconficureLogLevel(NLog.LogLevel l) {
            var target = NLog.LogManager.Configuration.FindTargetByName("ViewModel");
            var ViewModelRules = NLog.LogManager.Configuration.LoggingRules.Where(r => r.Targets.Contains(target)).ToList();
            foreach(var r in ViewModelRules) {
                r.DisableLoggingForLevel(NLog.LogLevel.Fatal);
                r.DisableLoggingForLevel(NLog.LogLevel.Error);
                r.DisableLoggingForLevel(NLog.LogLevel.Warn);
                r.DisableLoggingForLevel(NLog.LogLevel.Info);
                r.DisableLoggingForLevel(NLog.LogLevel.Debug);
                r.DisableLoggingForLevel(NLog.LogLevel.Trace);
                for(int i = NLog.LogLevel.Fatal.Ordinal; i >= l.Ordinal; i--) {
                    r.EnableLoggingForLevel(NLog.LogLevel.FromOrdinal(i));
                }
            }
            NLog.LogManager.ReconfigExistingLoggers();
        }

        private void ReconficureRulePattern(string pattern) {
            var target = NLog.LogManager.Configuration.FindTargetByName("ViewModel");
            var ViewModelRules = NLog.LogManager.Configuration.LoggingRules.Where(r => r.Targets.Contains(target)).ToList();
            foreach(var r in ViewModelRules) {
                r.LoggerNamePattern = pattern;
            }
            NLog.LogManager.ReconfigExistingLoggers();
        }


        private Level FromNlogLevel(NLog.LogLevel nl) {
            if(nl.Ordinal == NLog.LogLevel.Fatal.Ordinal) {
                return Level.Fatal;
            } else if(nl.Ordinal == NLog.LogLevel.Error.Ordinal) {
                return Level.Error;
            } else if(nl.Ordinal == NLog.LogLevel.Debug.Ordinal) {
                return Level.Debug;
            } else if(nl.Ordinal == NLog.LogLevel.Info.Ordinal) {
                return Level.Info;
            } else if(nl.Ordinal == NLog.LogLevel.Warn.Ordinal) {
                return Level.Warn;
            } else if(nl.Ordinal == NLog.LogLevel.Trace.Ordinal) {
                return Level.Trace;
            } else if(nl.Ordinal == NLog.LogLevel.Off.Ordinal) {
                return Level.Off;
            } else {
                throw new Exception("Kann den Log level nicht übersetzten!?");
            }
        }
    }
}
