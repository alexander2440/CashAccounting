using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ILogging;
using Microsoft.Owin.Hosting;
using System.Net;
using System.Net.NetworkInformation;

namespace TrayHost.ViewModel {
    public class MainViewModel : BaseViewModel {


        private ILog ClassLogger = (App.ioc.Resolve(typeof(ILoggingFactory), typeof(ILoggingFactory).Name, null) as ILoggingFactory)?.GetCurrentClassLogger();

        private bool started = false;
        private bool inProgress = false;

        private Dictionary<Type, object> ChildViewModels = new Dictionary<Type, object>();
        public void AddChildVm(object dataContext) {
            ChildViewModels.Add(dataContext.GetType(), dataContext);
        }

        private T ViewModel<T>() where T : BaseViewModel {
            T retVal = null;
            if(ChildViewModels.ContainsKey(typeof(T))) {
                retVal = (T)ChildViewModels[typeof(T)];
            }
            return retVal;
        }




        private List<Level> _allLevels = new List<Level>(Enum.GetValues(typeof(Level)).Cast<Level>());
        public List<Level> AllLevels {
            get { return _allLevels; }
            set { ChangeProperty(value); }
        }

        private Level _selectedLevel = Level.Debug;
        public Level SelectedLevel {
            get { return _selectedLevel; }
            set {
                if(ChangeProperty(value)) {
                    ViewModel<LiveLogViewModel>().Level = value;
                }
            }
        }

        #region Commands
        private RelayCommand _ServiceStart = null;
        public RelayCommand ServiceStart {
            get {
                if(ServiceStart == null) {
                    _ServiceStart = new RelayCommand(ServiceStartExecute, ServiceStartCanExecute);
                }
                return _ServiceStart;
            }
        }

        public void ServiceStartExecute() {
            inProgress = true;
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += new DoWorkEventHandler((obj, args) => {
                StartService();
                inProgress = false;
                OnPropertyChanged(() => ServiceStop);
            });
            bg.RunWorkerAsync();
        }
        public bool ServiceStartCanExecute() {
            return !started && !inProgress;
        }

        public RelayCommand ServiceStop { get { return new RelayCommand(ServiceStopExecute, ServiceStopCanExecute); } }
        public void ServiceStopExecute() {
            inProgress = true;
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += new DoWorkEventHandler((obj, args) => {
                StopService();
                inProgress = false;
                OnPropertyChanged(() => ServiceStart);
            });
            bg.RunWorkerAsync();
        }
        public bool ServiceStopCanExecute() {
            return started && !inProgress;
        }

        public RelayCommand LogClear { get { return new RelayCommand(LogClearExecute); } }
        public void LogClearExecute() {
            // Eigentlich sollte hier das command weiter delegiert werden.....
            ViewModel<LiveLogViewModel>().LogLines.Clear();
        }


        #endregion

        public void StartService() {

            try {
                ClassLogger.Info($" ---  Lokale Machine: {Environment.MachineName}");
                string domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;
                ClassLogger.Info($" ---  akutelle Domäne: {domainName}");
                string hostname = Dns.GetHostName();
                ClassLogger.Info($" ---  akuteller Host: {hostname}");
                string fqdn = hostname.EndsWith(domainName) ? hostname : $"{hostname}.{domainName}";
                ClassLogger.Info($" ---  akuteller FQDN: {fqdn}");
                var addresses = Dns.GetHostAddresses(fqdn);
                List<string> ipAdrToListen = new List<string>();
                addresses.ToList().ForEach(adr => {
                    if(adr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) {
                        ipAdrToListen.Add(adr.ToString());
                    }
                });

                // Es müssen alle gültigen URLs als StartOptions der WebApp.Start-Methode mitgegeben werden
                // da Connectionversuche mit nicht definierten Adressen zurückgewiesen werden.
                // Wenn also der FQDN (hier 'MobileWS.ad.aptasys') nicht definiert ist und das Service nur
                // an localhost lauscht, werden selbst von der selben Maschine keine Verbindungen akzeptiert,
                // die an MobileWS.ad.aptasys gerichtet sind bzw. das selbe gilt auch umgekehrt.
                // Selbst Verbindungen, die direkt an die IP-Adresse gerichtet sind, werden nicht akzeptiert, 
                // es sei denn, sie ist entsprechend konfiguriert.
                StartOptions options = new StartOptions();
                ////options.Urls.Add("http://test.developer.rkos.at:7990");
                options.Urls.Add("http://localhost:7990");
                ////options.Urls.Add("https://localhost:44400");
                ////options.Urls.Add($"http://{fqdn}:7990");
                options.Urls.Add($"https://asit.developer.rkos.at:44400");
                options.Urls.Add($"https://{fqdn}:44400");
                ipAdrToListen.ForEach(ipToListen => {
                    options.Urls.Add($"http://{ipToListen}:7990");
                    ////options.Urls.Add($"https://{ipToListen}:44400");
                });


                using(var wa = WebApp.Start<CashAccountingSvr.Startup>(options)) {
                    string urlList = options.Urls.Aggregate((s1, s2) => s1 + "\n                         " + s2);
                    ClassLogger.Info($"Server startet;\n   Started listening on: {urlList}");
                    Console.WriteLine("Press Enter to quit.");
                    Console.ReadLine();
                    ClassLogger.Info($"Server stopped;\n   Stopped listening on: {urlList}");
                }

            } catch(Exception ex) {
                ClassLogger.Error($"Unerwarteter Fehler aufgetreten.'{ex.Message}'", ex);
            }


        }

        public void StopService() {

        }


    }
}
