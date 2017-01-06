using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashAccountingEntities {
    public class Bankkontobewegung : EntityBase {

        private int _BewegungsID = default(int);
        public int BewegungsID {
            get { return _BewegungsID; }
            set { ChangeProperty(value); }
        }

        private string _Bewegungstext = default(string);
        public string Bewegungstext {
            get { return _Bewegungstext; }
            set { ChangeProperty(value); }
        }

        private DateTime _Bewegungsdatum = default(DateTime);
        public DateTime Bewegungsdatum {
            get { return _Bewegungsdatum; }
            set { ChangeProperty(value); }
        }

        private DateTime _Valutadatum = default(DateTime);
        public DateTime Valutadatum {
            get { return _Valutadatum; }
            set { ChangeProperty(value); }
        }

        private decimal _Betrag = default(decimal);
        public decimal Betrag {
            get { return _Betrag; }
            set { ChangeProperty(value); }
        }

        private int _KontoID = default(int);
        public int KontoID {
            get { return _KontoID; }
            set { ChangeProperty(value); }
        }

        private int? _Verrechnungsjahr = default(int?);
        public int? Verrechnungsjahr {
            get { return _Verrechnungsjahr; }
            set { ChangeProperty(value); }
        }

        private int? _BankbewegungstypId = default(int?);
        public int? BankbewegungstypId {
            get { return _BankbewegungstypId; }
            set { ChangeProperty(value); }
        }

        private bool? _FiBuVerbucht = default(bool?);
        public bool? FiBuVerbucht {
            get { return _FiBuVerbucht; }
            set { ChangeProperty(value); }
        }


        private Bankkonto _bankkonto = default(Bankkonto);
        public Bankkonto bankkonto {
            get { return _bankkonto; }
            set { ChangeProperty(value); }
        }


        public override string ToString() {
            return string.Format("BewegungsID {0} - Betrag {1} - Buchungstext '{2}'", BewegungsID, Betrag, Bewegungstext);
        }
    
    }
}
