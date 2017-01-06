using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashAccountingEntities {
    public class Bankkonto : EntityBase {

        private int _KontoID = default(int);
        public int KontoID {
            get { return _KontoID; }
            set { ChangeProperty(value); }
        }


        //public string KontoNr { get; set; }
        private string _KontoNr = default(string);
        public string KontoNr {
            get { return _KontoNr; }
            set { ChangeProperty(value); }
        }


        private string _Waehrung = default(string);
        public string Waehrung {
            get { return _Waehrung; }
            set { ChangeProperty(value); }
        }



        private string _KontoInhaber = default(string);
        public string KontoInhaber {
            get { return _KontoInhaber; }
            set { ChangeProperty(value); }
        }


        private int _BankID = default(int);
        public int BankID {
            get { return _BankID; }
            set { ChangeProperty(value); }
        }


        private string _Kontobezeichnung = default(string);
        public string Kontobezeichnung {
            get { return _Kontobezeichnung; }
            set { ChangeProperty(value); }
        }


        private string _IBAN = default(string);
        public string IBAN {
            get { return _IBAN; }
            set { ChangeProperty(value); }
        }


        public override string ToString() {
            return string.Format("KontoID {0} - '{1}'", KontoID, Kontobezeichnung);
        }

    }
}
