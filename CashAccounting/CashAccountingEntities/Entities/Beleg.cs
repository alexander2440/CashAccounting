using System;
using System.Collections.Generic;

namespace CashAccountingEntities {
    public class Beleg : EntityBase {

        private int _BelegId = default(int);
        public int BelegId {
            get { return _BelegId; }
            set { ChangeProperty(value); }
        }

        private string _BelegnummerIntern = default(string);
        public string BelegnummerIntern {
            get { return _BelegnummerIntern; }
            set { ChangeProperty(value); }
        }

        private DateTime? _BelegDatum = default(DateTime?);
        public DateTime? BelegDatum {
            get { return _BelegDatum; }
            set { ChangeProperty(value); }
        }

        private string _BelegnummerExtern = default(string);
        public string BelegnummerExtern {
            get { return _BelegnummerExtern; }
            set { ChangeProperty(value); }
        }

        private DateTime? _FaelligDatum = default(DateTime?);
        public DateTime? FaelligDatum {
            get { return _FaelligDatum; }
            set { ChangeProperty(value); }
        }

        private DateTime? _SkontoDatum = default(DateTime?);
        public DateTime? SkontoDatum {
            get { return _SkontoDatum; }
            set { ChangeProperty(value); }
        }

        private decimal? _Skontoprozent = default(decimal?);
        public decimal? Skontoprozent {
            get { return _Skontoprozent; }
            set { ChangeProperty(value); }
        }

        private decimal _BruttoBetragBeleg = default(decimal);
        public decimal BruttoBetragBeleg {
            get { return _BruttoBetragBeleg; }
            set { ChangeProperty(value); }
        }

        private decimal _NettoBetragBeleg = default(decimal);
        public decimal NettoBetragBeleg {
            get { return _NettoBetragBeleg; }
            set { ChangeProperty(value); }
        }

        private int _LandId = default(int);
        public int LandId {
            get { return _LandId; }
            set { ChangeProperty(value); }
        }

        private string _PartnerUID = default(string);
        public string PartnerUID {
            get { return _PartnerUID; }
            set { ChangeProperty(value); }
        }

        private string _Bemerkung = default(string);
        public string Bemerkung {
            get { return _Bemerkung; }
            set { ChangeProperty(value); }
        }

        private bool _Ausgangsbeleg = default(bool);
        public bool Ausgangsbeleg {
            get { return _Ausgangsbeleg; }
            set { ChangeProperty(value); }
        }

        private bool _BelegStorniert = default(bool);
        public bool BelegStorniert {
            get { return _BelegStorniert; }
            set { ChangeProperty(value); }
        }



        private List<Belegposition> _Positionen = new List<Belegposition>();
        public List<Belegposition> Positionen {
            get { return _Positionen; }
            set { _Positionen = value; } 
        }

        private List<BelegZahlungKopf> _Zahlungen = new List<BelegZahlungKopf>();
        public List<BelegZahlungKopf> Zahlungen {
            get { return _Zahlungen; }
            set { _Zahlungen = value; } 
        }
    }
}
