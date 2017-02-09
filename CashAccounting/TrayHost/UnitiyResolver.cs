using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using Unity = Microsoft.Practices.Unity;

namespace TrayHost {
    public class UnityResolver : IDependencyResolver {

        protected Unity.IUnityContainer Container { get; private set; } = null;

        #region Constructor

        public UnityResolver(Unity.IUnityContainer container) {
            if(container == null) {
                throw new ArgumentNullException("container");
            }
            this.Container = container;
        }

        #endregion

        public IDependencyScope BeginScope() {
            var child = Container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose() {
            Container.Dispose();
        }

        public object GetService(Type serviceType) {
            try {
                return Container.Resolve(serviceType, serviceType.Name, null);
            } catch(Unity.ResolutionFailedException) {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            try {
                return Container.ResolveAll(serviceType);
            } catch(Unity.ResolutionFailedException) {
                return new List<object>();
            }
        }
    }
}
