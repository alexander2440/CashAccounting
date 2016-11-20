using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashAccountingEntities {
    public class ServiceInfo : EntityBase {

        private string _ServiceName = default(string);
        public string ServiceName {
            get { return _ServiceName; }
            set { ChangeProperty<string>(value); }
        }

        private string _BuildVersion = default(string);
        public string BuildVersion {
            get { return _BuildVersion; }
            set { ChangeProperty<string>(value); }
        }

        private string _ProductVersion = default(string);
        public string ProductVersion {
            get { return _ProductVersion; }
            set { ChangeProperty<string>(value); }
        }

        private ObservableCollection<AssemblyVersion> _AssemblyVersions = new ObservableCollection<AssemblyVersion>();
        public ObservableCollection<AssemblyVersion> AssemblyVersions {
            get { return _AssemblyVersions; }
            protected set { ChangeProperty<ObservableCollection<AssemblyVersion>>(value); }
        }
    }
}
