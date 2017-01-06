using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashAccountingEntities {
    public class BelegZahlungPosition : EntityBase {

        private int _ZahlungPositionId = default(int);
        public int ZahlungPositionId {
            get { return _ZahlungPositionId; }
            set { ChangeProperty(value); }
        }

        private int _ZahlungKopfId = default(int);
        public int ZahlungKopfId {
            get { return _ZahlungKopfId; }
            set { ChangeProperty(value); }
        }

        private decimal _ZahlungsbetragBruttoBeleg = default(decimal);
        public decimal ZahlungsbetragBruttoBeleg {
            get { return _ZahlungsbetragBruttoBeleg; }
            set { ChangeProperty(value); }
        }

        private decimal _ZahlungsbetragBruttoBetrieb = default(decimal);
        public decimal ZahlungsbetragBruttoBetrieb {
            get { return _ZahlungsbetragBruttoBetrieb; }
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

        private bool _BruttoVerrechnung = default(bool);
        public bool BruttoVerrechnung {
            get { return _BruttoVerrechnung; }
            set { ChangeProperty(value); }
        }

        private string _Bemerkung = default(string);
        public string Bemerkung {
            get { return _Bemerkung; }
            set { ChangeProperty(value); }
        }

        private int? _BelegpositionId = default(int?);
        public int? BelegpositionId {
            get { return _BelegpositionId; }
            set { ChangeProperty(value); }
        }

        private decimal _SkonotBetragPosition = default(decimal);
        public decimal SkonotBetragPosition {
            get { return _SkonotBetragPosition; }
            set { ChangeProperty(value); }
        }

        private int _FAKategorieId = default(int);
        public int FAKategorieId {
            get { return _FAKategorieId; }
            set { ChangeProperty(value); }
        }

    }
}
