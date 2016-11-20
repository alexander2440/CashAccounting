using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Entities = CashAccountingEntities;

namespace CashAccountingSvr.Controller {
    public class ServiceInfoController : ApiController {

        public HttpResponseMessage ServiceInfo() {

            Assembly ass = this.GetType().Assembly;
            AssemblyProductAttribute assPrd = ass.GetCustomAttributes(typeof(AssemblyProductAttribute), false).FirstOrDefault() as AssemblyProductAttribute;
            AssemblyInformationalVersionAttribute assInfVer = ass.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false).FirstOrDefault() as AssemblyInformationalVersionAttribute;
            AssemblyVersionAttribute assVer = ass.GetCustomAttributes(typeof(AssemblyVersionAttribute), false).FirstOrDefault() as AssemblyVersionAttribute;

            Entities.ServiceInfo svcInfo = new Entities.ServiceInfo();
            svcInfo.ServiceName = assPrd == null ? "<n/a>" : assPrd.Product;
            svcInfo.ProductVersion = assInfVer == null ? "<n/a>" : assInfVer.InformationalVersion;
            svcInfo.BuildVersion = assVer == null ? "<n/a>" : assVer.Version;

            AppDomain.CurrentDomain.GetAssemblies().ToList()
                .Where(w => w.CodeBase.StartsWith(ass.CodeBase))
                .ToList().ForEach(a => {
                    svcInfo.AssemblyVersions.Add(new Entities.AssemblyVersion() {
                        NameOfAssembly = a.GetName().FullName,
                        VersionOfAssembly = a.ImageRuntimeVersion
                    });
                });

            HttpResponseMessage msg = Request.CreateResponse<Entities.ServiceInfo>(System.Net.HttpStatusCode.OK, svcInfo);
            return msg;

        }

    }
}
