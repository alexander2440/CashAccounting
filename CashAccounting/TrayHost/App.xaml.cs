using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity = Microsoft.Practices.Unity;
using IRepo = CashAccountingIRepo;
using Reop = CashAccountingRepoMSSQL;

namespace TrayHost {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        private static ILogging.ILoggingFactory logFactory = new NLogLogger.LoggingFactory();
        private static ILogging.ILog logger = logFactory.GetCurrentClassLogger();
        internal static Unity.UnityContainer ioc = new Unity.UnityContainer();


        private void Application_Startup(object sender, StartupEventArgs e) {
            ioc.RegisterInstance(typeof(ILogging.ILoggingFactory), "ILoggingFacotry", logFactory, new Unity.ContainerControlledLifetimeManager());
            ioc.RegisterType(typeof(IRepo.IRepositoryFactory), typeof(Reop.RepositoryFactory), typeof(IRepo.IBankRepo).Name, new Unity.TransientLifetimeManager(), null);
            ioc.RegisterType(typeof(IRepo.IBankRepo), typeof(Reop.BankRepo), typeof(IRepo.IBankRepo).Name, new Unity.TransientLifetimeManager(), null);
            ioc.RegisterType(typeof(IRepo.IBelegRepo), typeof(Reop.BelegRepo), typeof(IRepo.IBankRepo).Name, new Unity.TransientLifetimeManager(), null);
            ioc.RegisterType(typeof(IRepo.IFAKategorieRepo), typeof(Reop.FAKategorieRepo), typeof(IRepo.IBankRepo).Name, new Unity.TransientLifetimeManager(), null);
        }

        private void Application_Exit(object sender, ExitEventArgs e) {
            logFactory?.ShutdownLogging();
        }

    }
}
