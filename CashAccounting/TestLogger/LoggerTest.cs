using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TestLogger {
    [TestFixture]
    public class LoggerTest {

        [Test]
        public void InstantiateLogger() {
            // Die Referenz auf NLogLogger.LoggingFactory wird später via DependencyInjection
            // an die anderen Klassen weitergegeben --> für Test direkt instantiiert.
            ILogging.ILoggingFactory factory = new NLogLogger.LoggingFactory();
            Assert.NotNull(factory);
            Assert.IsInstanceOf(typeof(NLogLogger.LoggingFactory), factory);

            // Logger nur noch über ILogging.ILog - Interface verwenden
            ILogging.ILog logger = factory.GetCurrentClassLogger();
            Assert.NotNull(logger);
            Assert.IsInstanceOf(typeof(NLogLogger.Logger), logger);

            // Typisierten Logger holen, um tiefergreifende Tests zu ermöglichen; für normale 
            // Logging-Benutzer nicht zu verwenden
            NLogLogger.Logger typedLogger = logger as NLogLogger.Logger;
            Assert.NotNull(typedLogger);
            Assert.AreEqual(1, typedLogger.GetAllTargets().Count);
            Assert.AreEqual(1, typedLogger.GetTargets<NLog.Targets.MemoryTarget>().Count);
            NLog.Targets.MemoryTarget memTarget = typedLogger.GetTargets<NLog.Targets.MemoryTarget>()[0];
            Assert.AreEqual(this.GetType().Name, typedLogger.Name);

            logger.Trace("First message to log.");
            Assert.AreEqual(1, memTarget.Logs.Count);
            logger.Error("Second message to log.");
            Assert.AreEqual(2, memTarget.Logs.Count);
            memTarget.Logs.Clear();
            Assert.AreEqual(0, memTarget.Logs.Count);

            // Logger mit dem selben Namen aber über anderen Weg muss auf das
            // selbe Logging-Objekt zeigen
            ILogging.ILog sameLogger = factory.GetLogger(this.GetType().Name);
            Assert.AreSame(logger, sameLogger);
        }

    }
}
