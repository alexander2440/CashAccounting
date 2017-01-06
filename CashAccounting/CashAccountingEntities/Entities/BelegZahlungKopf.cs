using System;
using System.Collections.Generic;

namespace CashAccountingEntities {
    public class BelegZahlungKopf : EntityBase {

        private int _ZahlungKopfId = default(int);
        public int ZahlungKopfId {
            get { return _ZahlungKopfId; }
            set { ChangeProperty(value); }
        }

        private DateTime _Zahlungsdatum = default(DateTime);
        public DateTime Zahlungsdatum {
            get { return _Zahlungsdatum; }
            set { ChangeProperty(value); }
        }

        private int _BelegId = default(int);
        public int BelegId {
            get { return _BelegId; }
            set { ChangeProperty(value); }
        }

        private int _USTJahr = default(int);
        public int USTJahr {
            get { return _USTJahr; }
            set { ChangeProperty(value); }
        }

        private int _USTMonat = default(int);
        public int USTMonat {
            get { return _USTMonat; }
            set { ChangeProperty(value); }
        }

        private string _Bemerkung = default(string);
        public string Bemerkung {
            get { return _Bemerkung; }
            set { ChangeProperty(value); }
        }

        private decimal _SkonotBetrag = default(decimal);
        public decimal SkonotBetrag {
            get { return _SkonotBetrag; }
            set { ChangeProperty(value); }
        }


        private List<BelegZahlungPosition> _ZahlungsPositionen = new List<BelegZahlungPosition>();
        public List<BelegZahlungPosition> ZahlungsPositionen {
            get { return _ZahlungsPositionen; }
            set { _ZahlungsPositionen = value; }
        }

    }
}
