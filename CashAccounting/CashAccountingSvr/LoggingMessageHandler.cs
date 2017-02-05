using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ILogging;
using Microsoft.Practices.Unity;

namespace CashAccountingSvr {
    public class LoggingMessageHandler : DelegatingHandler {
        private IUnityContainer IoC;
        ILog Log = null;

        public LoggingMessageHandler(IUnityContainer ioC) {
            this.IoC = ioC;
            Log = (IoC.Resolve(typeof(ILoggingFactory), typeof(ILoggingFactory).Name, null) as ILoggingFactory)?.GetCurrentClassLogger();
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {

            try {
                string headers = request.Headers
                    .Select(s => s.Key + "=" + s.Value.Aggregate((s1, s2) => s1 + "; " + s2) + "\n")
                    .Aggregate((s1, s2) => s1 + s2);
                string strContent = await request.Content.ReadAsStringAsync();
                Log.Info($"\n   Method: {request.Method}\n   Uri: {request.RequestUri}\n   Version: {request.Version}\n   Headers:\n {headers} \n   Properties:\n   Content:\n{strContent}");
            } catch (Exception ex) {
                Log.Error("Fehler beim Message-Logging aufgetreten.", ex);
            }

            var response = await base.SendAsync(request, cancellationToken);

            try {
                if (response.Content != null) {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Log.Info($"\n   Content: {responseContent}");
                }
            } catch (Exception ex) {
                Log.Error("Fehler beim Message-Logging aufgetreten.", ex);
            }
            return response;
        }

    }
}
