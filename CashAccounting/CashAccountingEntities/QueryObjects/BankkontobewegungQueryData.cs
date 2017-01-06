using System;

namespace CashAccountingEntities {
    public class BankkontobewegungQueryData {
        public DateTime? VonValutaDatum { get; set; }
        public DateTime? BisValutaDatum { get; set; }
        public int? KontoId { get; set; }
        public bool VerbuchteInkludieren { get; set; }

        public BankkontobewegungQueryData() {
            VonValutaDatum = null;
            BisValutaDatum = null;
            KontoId = null;
            VerbuchteInkludieren = false;
        }

    }
}
