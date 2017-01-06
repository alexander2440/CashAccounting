
namespace CashAccountingEntities {
    public class Land : EntityBase {

        private int _LandId = default(int);
        public int LandId {
            get { return _LandId; }
            set { ChangeProperty(value); }
        }

        private string _ISO2Code = default(string);
        public string ISO2Code {
            get { return _ISO2Code; }
            set { ChangeProperty(value); }
        }

        private string _ISO3Code = default(string);
        public string ISO3Code {
            get { return _ISO3Code; }
            set { ChangeProperty(value); }
        }

        private string _Landesname = default(string);
        public string Landesname {
            get { return _Landesname; }
            set { ChangeProperty(value); }
        }

    }
}
