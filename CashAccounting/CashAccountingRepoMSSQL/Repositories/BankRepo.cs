using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using CashAccountingEntities;
using CashAccountingIRepo;

namespace CashAccountingRepoMSSQL {
    public class BankRepo : RepoBase, IBankRepo {

        private List<Bank> bankenCache = new List<Bank>();
        private List<Bankkonto> kontoCache = new List<Bankkonto>();
        private RepositoryFactory myRepoFactory = null;

        private ILogging.ILog Logger = null;

        #region Prepared Commands

        private DbCommand _GetAllBankenCmd = null;
        public DbCommand GetAllBankenCmd {
            get {
                if(_GetAllBankenCmd == null) {
                    _GetAllBankenCmd = myRepoFactory.CreateCommand();
                    _GetAllBankenCmd.CommandText = "select * from Bank ";
                    _GetAllBankenCmd.Prepare();
                }
                return _GetAllBankenCmd;
            }
        }

        private DbCommand _InsertBankCmd = null;
        public DbCommand InsertBankCmd {
            get {
                if(_InsertBankCmd == null) {
                    _InsertBankCmd = myRepoFactory.CreateCommand();
                    string fieldList = string.Empty;
                    string valueList = string.Empty;
                    List<PropertyInfo> props = typeof(Bank).GetProperties().ToList();
                    myRepoFactory.GetColumsForTable("Bank", false).ForEach(c => {
                        PropertyInfo prop = props.Where(p => p.Name == c.ColumnName).FirstOrDefault();
                        if(prop != null) {
                            if(fieldList.Length > 0) {
                                fieldList += ", ";
                                valueList += ", ";
                            }
                            fieldList += string.Format("[{0}]", c.ColumnName);
                            valueList += string.Format("@{0}", c.ColumnName);
                            _InsertBankCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, string.Format("@{0}", c.ColumnName), null, size: c.maxLenght, precision: c.precision, scale: c.scale);
                        }
                    });
                    _InsertBankCmd.CommandText = string.Format("Insert into Bank ({0}) OUTPUT INSERTED.BankID values ({1})", fieldList, valueList);
                    _InsertBankCmd.Prepare();
                }
                return _InsertBankCmd;
            }
        }

        private DbCommand _UpdateBankCmd = null;
        public DbCommand UpdateBankCmd {
            get {
                if(_UpdateBankCmd == null) {
                    _UpdateBankCmd = myRepoFactory.CreateCommand();
                    string setList = string.Empty;
                    string identityList = string.Empty;
                    List<PropertyInfo> props = typeof(Bank).GetProperties().ToList();
                    myRepoFactory.GetColumsForTable("Bank", true).ForEach(c => {
                        PropertyInfo prop = props.Where(p => p.Name == c.ColumnName).FirstOrDefault();
                        if(prop != null) {
                            if(c.IsIdentity) {
                                if(identityList.Length == 0) {
                                    identityList = "where ";
                                } else {
                                    identityList += " and ";
                                }
                                identityList += string.Format("[{0}] = @{0}", c.ColumnName);
                            } else {
                                if(setList.Length > 0) {
                                    setList += ", ";
                                }
                                setList += string.Format("[{0}] = @{0}", c.ColumnName);
                            }
                            _UpdateBankCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, string.Format("@{0}", c.ColumnName), null, size: c.maxLenght, precision: c.precision, scale: c.scale);
                        }
                    });
                    _UpdateBankCmd.CommandText = string.Format("Update Bank set {0} {1} ", setList, identityList);
                    _UpdateBankCmd.Prepare();
                }
                return _UpdateBankCmd;
            }
        }

        private DbCommand _GetAllBankkontenCmd = null;
        public DbCommand GetAllBankkontenCmd {
            get {
                if(_GetAllBankkontenCmd == null) {
                    _GetAllBankkontenCmd = myRepoFactory.CreateCommand();
                    _GetAllBankkontenCmd.CommandText = "select * from Bankkonto ";
                    _GetAllBankkontenCmd.Prepare();
                }
                return _GetAllBankkontenCmd;
            }
        }

        private DbCommand _FindDuplicateBankkontobewegungCmd = null;
        public DbCommand FindDuplicateBankkontobewegungCmd {
            get {
                if(_FindDuplicateBankkontobewegungCmd == null) {
                    _FindDuplicateBankkontobewegungCmd = myRepoFactory.CreateCommand();
                    _FindDuplicateBankkontobewegungCmd.CommandText =
                        "select top 1 * from Bankkontobewegung " +
                        "where KontoID = @KontoID and Bewegungstext = @Bewegungstext " +
                        "and Bewegungsdatum = @Bewegungsdatum and Valutadatum = @Valutadatum " +
                        "and Betrag = @Betrag";
                    _FindDuplicateBankkontobewegungCmd.AddCmdParameter(typeof(int), ParameterDirection.Input, "@KontoID", null);
                    _FindDuplicateBankkontobewegungCmd.AddCmdParameter(typeof(string), ParameterDirection.Input, "@Bewegungstext", null, size: -1);
                    _FindDuplicateBankkontobewegungCmd.AddCmdParameter(typeof(DateTime), ParameterDirection.Input, "@Bewegungsdatum", null);
                    _FindDuplicateBankkontobewegungCmd.AddCmdParameter(typeof(DateTime), ParameterDirection.Input, "@Valutadatum", null);
                    _FindDuplicateBankkontobewegungCmd.AddCmdParameter(typeof(decimal), ParameterDirection.Input, "@Betrag", null, precision: 13, scale: 2);

                    _FindDuplicateBankkontobewegungCmd.Prepare();
                }
                return _FindDuplicateBankkontobewegungCmd;
            }
        }

        private DbCommand _InsertBankbuchungCmd = null;
        public DbCommand InsertBankbuchungCmd {
            get {
                if(_InsertBankbuchungCmd == null) {
                    _InsertBankbuchungCmd = myRepoFactory.CreateCommand();
                    string fieldList = string.Empty;
                    string valueList = string.Empty;
                    List<PropertyInfo> props = typeof(Bankkontobewegung).GetProperties().ToList();
                    myRepoFactory.GetColumsForTable("Bankkontobewegung", false).ForEach(c => {
                        PropertyInfo prop = props.Where(p => p.Name == c.ColumnName).FirstOrDefault();
                        if(prop != null) {
                            if(fieldList.Length > 0) {
                                fieldList += ", ";
                                valueList += ", ";
                            }
                            fieldList += string.Format("[{0}]", c.ColumnName);
                            valueList += string.Format("@{0}", c.ColumnName);
                            _InsertBankbuchungCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, string.Format("@{0}", c.ColumnName), null, size: c.maxLenght, precision: c.precision, scale: c.scale);
                        }
                    });
                    _InsertBankbuchungCmd.CommandText = string.Format("Insert into Bankkontobewegung ({0}) OUTPUT INSERTED.BewegungsID values ({1})", fieldList, valueList);
                    _InsertBankbuchungCmd.Prepare();
                }
                return _InsertBankbuchungCmd;
            }
        }

        private DbCommand _UpdateBankbuchungCmd = null;
        public DbCommand UpdateBankbuchungCmd {
            get {
                if(_UpdateBankbuchungCmd == null) {
                    _UpdateBankbuchungCmd = myRepoFactory.CreateCommand();
                    string setList = string.Empty;
                    string identityList = string.Empty;
                    List<PropertyInfo> props = typeof(Bankkontobewegung).GetProperties().ToList();
                    myRepoFactory.GetColumsForTable("Bankkontobewegung", true).ForEach(c => {
                        PropertyInfo prop = props.Where(p => p.Name == c.ColumnName).FirstOrDefault();
                        if(prop != null) {
                            if(c.IsIdentity) {
                                if(identityList.Length == 0) {
                                    identityList = "where ";
                                } else {
                                    identityList += " and ";
                                }
                                identityList += string.Format("[{0}] = @{0}", c.ColumnName);
                            } else {
                                if(setList.Length > 0) {
                                    setList += ", ";
                                }
                                setList += string.Format("[{0}] = @{0}", c.ColumnName);
                            }
                            _UpdateBankbuchungCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, string.Format("@{0}", c.ColumnName), null, size: c.maxLenght, precision: c.precision, scale: c.scale);
                        }
                    });
                    _UpdateBankbuchungCmd.CommandText = string.Format("Update Bankkontobewegung set {0} {1} ", setList, identityList);
                    _UpdateBankbuchungCmd.Prepare();
                }
                return _UpdateBankbuchungCmd;
            }
        }

        private DbCommand _GetBankkontobewegungNotInFiBuCmd = null;
        public DbCommand GetBankkontobewegungNotInFiBuCmd {
            get {
                if(_GetBankkontobewegungNotInFiBuCmd == null) {
                    _GetBankkontobewegungNotInFiBuCmd = myRepoFactory.CreateCommand();
                    _GetBankkontobewegungNotInFiBuCmd.CommandText = "select * from Bankkontobewegung where FiBuVerbucht is null ";
                    _GetBankkontobewegungNotInFiBuCmd.Prepare();
                }
                return _GetAllBankkontenCmd;
            }
        }


        private DbCommand _GetAllAbgabentypenCmd = null;
        public DbCommand GetAllAbgabentypenCmd {
            get {
                if(_GetAllAbgabentypenCmd == null) {
                    _GetAllAbgabentypenCmd = myRepoFactory.CreateCommand();
                    _GetAllAbgabentypenCmd.CommandText =
                        "select * from [Bankbewegungstyp] " +
                        "order by [BankbewegungstypBez]";
                    _GetAllAbgabentypenCmd.Prepare();
                }
                return _GetAllAbgabentypenCmd;
            }
        }


        #endregion



        #region Konstruktor

        public BankRepo(ILogging.ILoggingFactory logFactory) {
            Logger = logFactory.GetCurrentClassLogger();
            myRepoFactory = RepositoryFactory.theRepoFactory as RepositoryFactory;
        }

        #endregion



        #region IBankRep Members

        public List<Bank> GetAllBanken() {
            List<Bank> wrkList = new List<Bank>();
            using(DbDataReader dbDr = GetAllBankenCmd.ExecuteReader()) {
                while(dbDr.Read()) {
                    Bank bank = new Bank();
                    bank.FillEntityWithDataReader(dbDr);
                    wrkList.Add(bank);
                }
            }
            bankenCache = wrkList;
            return bankenCache;
        }

        public Bank GetBankById(int bankId) {
            Bank bank = bankenCache.Where(b => b.BankID == bankId).FirstOrDefault();
            if(bank == null) {
                GetAllBanken();
                bank = bankenCache.Where(b => b.BankID == bankId).FirstOrDefault();
            }
            return bank;
        }

        public Bank GetBankenByBIC(string bic) {
            Bank bank = bankenCache.Where(b => b.BIC == bic).FirstOrDefault();
            if(bank == null) {
                GetAllBanken();
                bank = bankenCache.Where(b => b.BIC.Trim() == bic).FirstOrDefault();
            }
            return bank;
        }

        public int SaveBank(Bank bank) {
            if(bank.BankID == 0) {
                return InsertBank(bank);
            } else {
                UpdateBank(bank);
                return bank.BankID;
            }

        }

        private int InsertBank(Bank bank) {
            FillCommandParameter(InsertBankCmd, bank);
            Int32 newBankId = (Int32)InsertBankCmd.ExecuteScalar();
            bank.BankID = newBankId;
            return newBankId;
        }

        private void UpdateBank(Bank bank) {
            FillCommandParameter(UpdateBankCmd, bank);
            UpdateBankCmd.ExecuteNonQuery();
        }


        public List<Bankkonto> GetAllBankkonten() {
            List<Bankkonto> wrkList = new List<Bankkonto>();
            using(DbDataReader dbDr = GetAllBankkontenCmd.ExecuteReader()) {
                while(dbDr.Read()) {
                    Bankkonto konto = new Bankkonto();
                    konto.FillEntityWithDataReader(dbDr);
                    wrkList.Add(konto);
                }
            }
            kontoCache = wrkList;
            return kontoCache;
        }

        public Bankkonto GetBankkontoByIBAN(string IBAN) {
            Bankkonto konto = kontoCache.Where(k => k.IBAN.Trim() == IBAN).FirstOrDefault();
            if(konto == null) {
                GetAllBankkonten();
                konto = kontoCache.Where(k => k.IBAN == IBAN).FirstOrDefault();
            }
            return konto;
        }

        public Bankkonto GetBankkontoByKtoNr(string ktoNbr, int bankId) {
            Bankkonto konto = kontoCache.Where(k => k.KontoNr == ktoNbr && k.BankID == bankId).FirstOrDefault();
            if(konto == null) {
                GetAllBankkonten();
                konto = kontoCache.Where(k => k.KontoNr == ktoNbr && k.BankID == bankId).FirstOrDefault();
            }
            return konto;

        }

        public Bankkonto GetBankkontoById(int kontoId) {
            Bankkonto konto = kontoCache.Where(k => k.KontoID == kontoId).FirstOrDefault();
            if(konto == null) {
                GetAllBankkonten();
                konto = kontoCache.Where(k => k.KontoID == kontoId).FirstOrDefault();
            }
            return konto;
        }

        public Bank GetBankZuKonto(Bankkonto bankkonto) {
            return GetBankById(bankkonto.BankID);
        }

        public List<Bankkontobewegung> GetBankkontobewegungNotInFiBu() {
            List<Bankkontobewegung> wrkList = new List<Bankkontobewegung>();
            using(DbDataReader dbDr = GetBankkontobewegungNotInFiBuCmd.ExecuteReader()) {
                while(dbDr.Read()) {
                    Bankkontobewegung b = new Bankkontobewegung();
                    b.FillEntityWithDataReader(dbDr);
                    wrkList.Add(b);
                }
            }
            return wrkList;
        }


        public Bankkontobewegung FindDuplicateBankkontobewegung(Bankkontobewegung buchung) {
            Bankkontobewegung wrkBuchung = null;
            FillCommandParameter(FindDuplicateBankkontobewegungCmd, buchung);
            using(DbDataReader dbdr = FindDuplicateBankkontobewegungCmd.ExecuteReader()) {
                if(dbdr.Read()) {
                    wrkBuchung = new Bankkontobewegung();
                    wrkBuchung.FillEntityWithDataReader(dbdr);
                }
            }
            return wrkBuchung;
        }


        public int ImportiereBankkontobewegung(Bankkontobewegung buchung) {
            Bankkontobewegung wrkBuchung = null;
            if((wrkBuchung = FindDuplicateBankkontobewegung(buchung)) == null) {
                return InsertBankbuchung(buchung);
            } else {
                UpdateBankbuchung(buchung);
                return buchung.BewegungsID;
            }
        }

        public int SichereBankbuchung(Bankkontobewegung buchung) {
            int wrkBewegungsID = 0;
            try {
                if(buchung.BewegungsID <= 0) {
                    wrkBewegungsID = InsertBankbuchung(buchung);
                } else {
                    UpdateBankbuchung(buchung);
                    wrkBewegungsID = buchung.BewegungsID;
                }

            } catch(Exception ex) {
                Logger.Error("Sichern der Bankbuchung fehlgeschlagen.", ex);
                throw;
            } return wrkBewegungsID;
        }

        private int InsertBankbuchung(Bankkontobewegung buchung) {
            FillCommandParameter(InsertBankbuchungCmd, buchung);
            Int32 newBewegungsID = (Int32)InsertBankbuchungCmd.ExecuteScalar();
            buchung.BewegungsID = newBewegungsID;
            return newBewegungsID;
        }

        private void UpdateBankbuchung(Bankkontobewegung buchung) {
            FillCommandParameter(UpdateBankbuchungCmd, buchung);
            UpdateBankbuchungCmd.ExecuteNonQuery();
        }



        public List<Bankkontobewegung> GetBankkontobewegungByQueryData(BankkontobewegungQueryData qryDta) {
            List<Bankkontobewegung> resultList = new List<Bankkontobewegung>();
            List<string> whereClauses = new List<string>();
            using(DbCommand cmd = myRepoFactory.CreateCommand()) {
                cmd.CommandText = "select * from Bankkontobewegung ";
                if(qryDta.VonValutaDatum != null && ((DateTime)qryDta.VonValutaDatum) > new DateTime(1950, 01, 01)) {
                    whereClauses.Add("Valutadatum >= @VonValutaDatum");
                    cmd.AddCmdParameter(typeof(DateTime), ParameterDirection.Input, "@VonValutaDatum", (DateTime)qryDta.VonValutaDatum);
                }
                if(qryDta.BisValutaDatum != null && ((DateTime)qryDta.BisValutaDatum) < new DateTime(3000, 01, 01)) {
                    whereClauses.Add("Valutadatum <= @BisValutaDatum");
                    cmd.AddCmdParameter(typeof(DateTime), ParameterDirection.Input, "@BisValutaDatum", (DateTime)qryDta.BisValutaDatum);
                }
                if(qryDta.KontoId != null && (int)qryDta.KontoId > 0) {
                    whereClauses.Add("KontoID = @KontoId");
                    cmd.AddCmdParameter(typeof(int), ParameterDirection.Input, "@KontoId", (int)qryDta.KontoId);
                }
                if(!qryDta.VerbuchteInkludieren) {
                    whereClauses.Add("FiBuVerbucht is Null");
                }
                cmd.CommandText += whereClauses.Count > 0 ? " where " : "";
                for(int i = 0; i < whereClauses.Count; i++) {
                    cmd.CommandText += whereClauses[i];
                    cmd.CommandText += i < whereClauses.Count - 1 ? " and " : "";
                }
                cmd.Prepare();

                using(DbDataReader dbDr = cmd.ExecuteReader()) {
                    while(dbDr.Read()) {
                        Bankkontobewegung bew = new Bankkontobewegung();
                        bew.FillEntityWithDataReader(dbDr);
                        resultList.Add(bew);
                    }
                }

            }
            return resultList;
        }

        public List<Bankbewegungstyp> GetAllAbgabentypen() {
            List<Bankbewegungstyp> wrkList = new List<Bankbewegungstyp>();
            using(DbDataReader dbDr = GetAllAbgabentypenCmd.ExecuteReader()) {
                while(dbDr.Read()) {
                    Bankbewegungstyp typ = new Bankbewegungstyp();
                    typ.FillEntityWithDataReader(dbDr);
                    wrkList.Add(typ);
                }
            }
            return wrkList;
        }


        #endregion
    }
}
