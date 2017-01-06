using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashAccountingEntities {
    public class Bank : EntityBase {

        private int _BankID = default(int);
        public int BankID {
            get { return _BankID; }
            set { ChangeProperty(value); }
        }

        private string _Bankname = default(string);
        public string Bankname {
            get { return _Bankname; }
            set { ChangeProperty(value); }
        }

        private int _BLZ = default(int);
        public int BLZ {
            get { return _BLZ; }
            set { ChangeProperty(value); }
        }

        private string _BIC = default(string);
        public string BIC {
            get { return _BIC; }
            set { ChangeProperty(value); }
        }

        private string _CsvFormatID = default(string);
        public string CsvFormatID {
            get { return _CsvFormatID; }
            set { ChangeProperty(value); }
        }


        public override string ToString() {
            return BankID + " - " + Bankname + " - " + BLZ;
        }


    }
}
