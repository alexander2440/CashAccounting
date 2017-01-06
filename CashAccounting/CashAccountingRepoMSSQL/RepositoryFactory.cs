using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CashAccountingIRepo;
using CashAccountingShared;

namespace CashAccountingRepoMSSQL {

    public class RepositoryFactory : IRepositoryFactory {

        private IBelegRepo theBelegRepo = null;
        private IFAKategorieRepo theFAKategorieRepo = null;
        private IBankRepo theBankRepo = null;

        private Dictionary<string, List<ColumnDescription>> columnNameCache = new Dictionary<string, List<ColumnDescription>>();

        private static IRepositoryFactory _theRepoFactory = null;
        public static IRepositoryFactory theRepoFactory {
            get { return _theRepoFactory; }
            private set { _theRepoFactory = value; }
        }

        private ILogging.ILoggingFactory _LoggingFactory;
        public ILogging.ILoggingFactory LoggingFactory {
            get { return _LoggingFactory; }
            set { _LoggingFactory = value; }
        }



        #region DB-Initialisierung

        public string MyDbProviderName { get; set; }
        public string MyConnectionString { get; set; }
        public DbProviderFactory RepoDbProvider { get; set; }


        internal DbConnection _MyDbConnection = null;
        public DbConnection MyDbConnection {
            get {
                if(_MyDbConnection != null && (_MyDbConnection.State == System.Data.ConnectionState.Broken || _MyDbConnection.State == System.Data.ConnectionState.Closed)) {
                    MyDbConnection = null;
                }
                if(_MyDbConnection == null) {
                    _MyDbConnection = RepoDbProvider.CreateConnection();
                    _MyDbConnection.ConnectionString = MyConnectionString;
                    _MyDbConnection.Open();
                    FillColumnNameCache();
                }
                return _MyDbConnection;
            }
            set {
                if(_MyDbConnection != null && _MyDbConnection != value) {
                    // Ändert sich eine bestehende DbConnection, dann müssen alle
                    // Repositories entsorgt und neu erstellt werden.
                    DisposeRepositiories();
                    // Das bestende DbConnection-Objekt wird entsorgt
                    _MyDbConnection.Close();
                    _MyDbConnection.Dispose();

                    columnNameCache.Clear();
                }
                _MyDbConnection = value;
            }
        }

        private void FillColumnNameCache() {
            columnNameCache.Clear();
            using(DbCommand cmd = _MyDbConnection.CreateCommand()) {
                cmd.CommandText = "select t.name tablename, c.name colname, c.is_identity, max_length, [precision], [scale]  " +
                                  "from sys.columns c " +
                                  "inner join sys.tables t on c.object_id = t.object_id " +
                                  "order by t.name, column_id";
                using(DbDataReader dbDr = cmd.ExecuteReader()) {
                    bool found = dbDr.Read();
                    while(found) {
                        string vglTablename = (string)dbDr["tablename"];
                        List<ColumnDescription> colDes = new List<ColumnDescription>();
                        while(found && (string)dbDr["tablename"] == vglTablename) {
                            ColumnDescription cd = new ColumnDescription();
                            cd.ColumnName = (string)dbDr["colname"];
                            cd.IsIdentity = (bool)dbDr["is_identity"];
                            cd.maxLenght = (short)dbDr["max_length"];
                            cd.precision = (byte)dbDr["precision"];
                            cd.scale = (byte)dbDr["scale"];
                            colDes.Add(cd);
                            found = dbDr.Read();
                        }
                        columnNameCache.Add(vglTablename.Trim().ToUpper(), colDes);
                    }

                }
            }
        }


        public DbCommand CreateCommand() {
            return MyDbConnection.CreateCommand();
        }

        #endregion


        #region Konstruktor(en)

        public RepositoryFactory(ILogging.ILoggingFactory loggingFactory) {
            LoggingFactory = loggingFactory;
            if(theRepoFactory != null) {
                throw new ApplicationException(
                    string.Format("An instance of the Type {0} is allready instantiated. Please use {0}.{1} instead",
                    this.GetType().Name, this.GetPropertyName(() => theRepoFactory)));
            }
            MyConnectionString = ConfigurationManager.ConnectionStrings["EARTest"].ConnectionString;
            MyDbProviderName = ConfigurationManager.ConnectionStrings["EARTest"].ProviderName;
            RepoDbProvider = DbProviderFactories.GetFactory(MyDbProviderName);
            theRepoFactory = this;
        }

        #endregion


        #region DB-Helper Methoden

        internal List<string> GetColumNamesForTable(string tablename, bool includeIdentityColumns = false) {
            string wrkTabName = tablename.Trim().ToUpper();
            if(columnNameCache.ContainsKey(wrkTabName)) {
                return columnNameCache[wrkTabName]
                    .Where(cd => !cd.IsIdentity || includeIdentityColumns)
                    .Select(cd => cd.ColumnName).ToList();
            }

            return null;
        }


        internal List<ColumnDescription> GetColumsForTable(string tablename, bool includeIdentityColumns = false) {
            string wrkTabName = tablename.Trim().ToUpper();
            if(columnNameCache.ContainsKey(wrkTabName)) {
                return columnNameCache[wrkTabName]
                    .Where(cd => !cd.IsIdentity || includeIdentityColumns).ToList();
            }

            return null;
        }

        private void DisposeRepositiories() {
            List<PropertyInfo> props = this.GetType().GetProperties(BindingFlags.Instance).ToList();
            foreach(PropertyInfo prop in props) {
                object propVal = prop.GetValue(this);
                if(propVal is RepoBase) {
                    ((RepoBase)propVal).Dispose();
                    prop.SetValue(this, null);
                }
            }
        }

        #endregion


        #region IRepositoryFactory Member

        public IBelegRepo GetBelegRepo() {
            if(theBelegRepo == null) {
                theBelegRepo = new BelegRepo(LoggingFactory);
            }
            return theBelegRepo;
        }

        public IFAKategorieRepo GetFAKagegorieRepo() {
            if(theFAKategorieRepo == null) {
                theFAKategorieRepo = new FAKategorieRepo(LoggingFactory);
            }
            return theFAKategorieRepo;
        }

        public IBankRepo GetBankRepo() {
            if(theBankRepo == null) {
                theBankRepo = new BankRepo(LoggingFactory);
            }
            return theBankRepo;
        }

        #endregion
    }
}
