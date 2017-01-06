
namespace CashAccountingEntities {
    public class Belegposition : EntityBase {

        private int _BelegpositionId = default(int);
        public int BelegpositionId {
            get { return _BelegpositionId; }
            set { ChangeProperty(value); }
        }

        private int _BelegId = default(int);
        public int BelegId {
            get { return _BelegId; }
            set { ChangeProperty(value); }
        }

        private decimal _BttoPosBetragOriginal = default(decimal);
        public decimal BttoPosBetragOriginal {
            get { return _BttoPosBetragOriginal; }
            set { ChangeProperty(value); }
        }

        private decimal _NttoPosBetragOriginal = default(decimal);
        public decimal NttoPosBetragOriginal {
            get { return _NttoPosBetragOriginal; }
            set { ChangeProperty(value); }
        }

        private decimal _MWSTSatz = default(decimal);
        public decimal MWSTSatz {
            get { return _MWSTSatz; }
            set { ChangeProperty(value); }
        }

        private decimal _MWSTSatzAequivalentUID = default(decimal);
        public decimal MWSTSatzAequivalentUID {
            get { return _MWSTSatzAequivalentUID; }
            set { ChangeProperty(value); }
        }

        private decimal _Betriebsanteil = default(decimal);
        public decimal Betriebsanteil {
            get { return _Betriebsanteil; }
            set { ChangeProperty(value); }
        }

        private decimal _BttoPosBetragBetrieb = default(decimal);
        public decimal BttoPosBetragBetrieb {
            get { return _BttoPosBetragBetrieb; }
            set { ChangeProperty(value); }
        }

        private decimal _NttoPosBetragBetrieb = default(decimal);
        public decimal NttoPosBetragBetrieb {
            get { return _NttoPosBetragBetrieb; }
            set { ChangeProperty(value); }
        }

        private string _Bemerkung = default(string);
        public string Bemerkung {
            get { return _Bemerkung; }
            set { ChangeProperty(value); }
        }

        private int _FAKategorieId = default(int);
        public int FAKategorieId {
            get { return _FAKategorieId; }
            set { ChangeProperty(value); }
        }

    }
}
