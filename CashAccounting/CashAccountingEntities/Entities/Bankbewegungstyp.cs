using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashAccountingEntities {
    public class Bankbewegungstyp : EntityBase {


        private int? _BankbewegungstypId = default(int?);
        public int? BankbewegungstypId {
            get { return _BankbewegungstypId; }
            set { ChangeProperty(value); }
        }

        private string _BankbewegungstypBez = default(string);
        public string BankbewegungstypBez {
            get { return _BankbewegungstypBez; }
            set { ChangeProperty(value); }
        }

        public override string ToString() {
            return BankbewegungstypId + " - " + BankbewegungstypBez;
        }


    }
}
