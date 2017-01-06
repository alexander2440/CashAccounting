
namespace CashAccountingEntities {
    public class FAKategorie : EntityBase {

        private int _FAKategorieId = default(int);
        public int FAKategorieId {
            get { return _FAKategorieId; }
            set { ChangeProperty(value); }
        }

        private string _FAKennzeichenEA = default(string);
        public string FAKennzeichenEA {
            get { return _FAKennzeichenEA; }
            set { ChangeProperty(value); }
        }

        private int _FAKategoriekennzahl = default(int);
        public int FAKategoriekennzahl {
            get { return _FAKategoriekennzahl; }
            set { ChangeProperty(value); }
        }

        private int _FAJahrGueltigAb = default(int);
        public int FAJahrGueltigAb {
            get { return _FAJahrGueltigAb; }
            set { ChangeProperty(value); }
        }

        private string _FAKategorieKurztext = default(string);
        public string FAKategorieKurztext {
            get { return _FAKategorieKurztext; }
            set { ChangeProperty(value); }
        }

        private string _FAKategorieLangtext = default(string);
        public string FAKategorieLangtext {
            get { return _FAKategorieLangtext; }
            set { ChangeProperty(value); }
        }

    }
}
