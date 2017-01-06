
namespace CashAccountingEntities {
    public class BelegDokument : EntityBase {

        private int _BelegDokumentId = default(int);
        public int BelegDokumentId {
            get { return _BelegDokumentId; }
            set { ChangeProperty(value); }
        }

        private int _BelegId = default(int);
        public int BelegId {
            get { return _BelegId; }
            set { ChangeProperty(value); }
        }

        private string _DokumentTyp = default(string);  // z.B. ".PDF" oder ".JPG" usw.
        public string DokumentTyp {
            get { return _DokumentTyp; }
            set { ChangeProperty(value); }
        }

        private byte[] _Dokuemnt = default(byte[]);
        public byte[] Dokuemnt {
            get { return _Dokuemnt; }
            set { ChangeProperty(value); }
        }

        private string _Bemerkung = default(string);
        public string Bemerkung {
            get { return _Bemerkung; }
            set { ChangeProperty(value); }
        }

    }
}
