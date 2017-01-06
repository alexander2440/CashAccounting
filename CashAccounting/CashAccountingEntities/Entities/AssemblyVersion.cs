using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashAccountingEntities {
    public class AssemblyVersion : EntityBase {


        private string _NameOfAssembly = default(string);
        public string NameOfAssembly {
            get { return _NameOfAssembly; }
            set { ChangeProperty(value); }
        }

        private string _VersionOfAssembly = default(string);
        public string VersionOfAssembly {
            get { return _VersionOfAssembly; }
            set { ChangeProperty(value); }
        }



    }
}
