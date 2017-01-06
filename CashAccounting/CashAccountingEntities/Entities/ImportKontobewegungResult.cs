using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashAccountingEntities {
    public class ImportKontobewegungResult : EntityBase {

        private bool _Importiert = default(bool);
        public bool Importiert {
            get { return _Importiert; }
            set { ChangeProperty(value); }
        }

        private bool _Doppelt = default(bool);
        public bool Doppelt {
            get { return _Doppelt; }
            set { ChangeProperty(value); }
        }

        private bool _Fehlerhaft = default(bool);
        public bool Fehlerhaft {
            get { return _Fehlerhaft; }
            set { ChangeProperty(value); }
        }


    }

    public class ImportKontobewegungenResult : EntityBase {

        private long _Gelesen = default(long);
        public long Gelesen {
            get { return _Gelesen; }
            set { ChangeProperty(value); }
        }

        private long _Importiert = default(long);
        public long Importiert {
            get { return _Importiert; }
            set { ChangeProperty(value); }
        }

        private long _Doppelt = default(long);
        public long Doppelt {
            get { return _Doppelt; }
            set { ChangeProperty(value); }
        }

        private long _Fehlerhaft = default(long);
        public long Fehlerhaft {
            get { return _Fehlerhaft; }
            set { ChangeProperty(value); }
        }

    }


}
