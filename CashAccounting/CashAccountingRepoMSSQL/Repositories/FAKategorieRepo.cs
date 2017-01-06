using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using CashAccountingEntities;
using CashAccountingIRepo;

namespace CashAccountingRepoMSSQL {
    public class FAKategorieRepo : RepoBase, IFAKategorieRepo {

        private RepositoryFactory myRepoFactory = null;

        private ILogging.ILog Logger = null;

        #region Prepared Commands

        private DbCommand _AddCmd = null;
        public DbCommand AddCmd {
            get {
                if(_AddCmd == null) {
                    _AddCmd = myRepoFactory.CreateCommand();
                    string fieldList = string.Empty;
                    string valueList = string.Empty;
                    List<PropertyInfo> props = typeof(FAKategorie).GetProperties().ToList();
                    myRepoFactory.GetColumsForTable("FAKategorie", false).ForEach(c => {
                        PropertyInfo prop = props.Where(p => p.Name == c.ColumnName).FirstOrDefault();
                        if(prop != null) {
                            if(fieldList.Length > 0) {
                                fieldList += ", ";
                                valueList += ", ";
                            }
                            fieldList += string.Format("[{0}]", c.ColumnName);
                            valueList += string.Format("@{0}", c.ColumnName);
                            _AddCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, string.Format("@{0}", c.ColumnName), null, size: c.maxLenght, precision: c.precision, scale: c.scale);
                        }
                    });
                    _AddCmd.CommandText = string.Format("Insert into FAKategorie ({0}) OUTPUT INSERTED.FAKategorieId values ({1})", fieldList, valueList);
                    _AddCmd.Prepare();
                }
                return _AddCmd;
            }
        }

        private DbCommand _UpdateCmd = null;
        public DbCommand UpdateCmd {
            get {
                if(_UpdateCmd == null) {
                    _UpdateCmd = myRepoFactory.CreateCommand();
                    string setList = string.Empty;
                    string identityList = string.Empty;
                    List<PropertyInfo> props = typeof(FAKategorie).GetProperties().ToList();
                    myRepoFactory.GetColumsForTable("FAKategorie", true).ForEach(c => {
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
                            _UpdateCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, string.Format("@{0}", c.ColumnName), null, size: c.maxLenght, precision: c.precision, scale: c.scale);
                        }
                    });
                    _UpdateCmd.CommandText = string.Format("Update FAKategorie set {0} {1} ", setList, identityList);
                    _UpdateCmd.Prepare();
                }
                return _UpdateCmd;
            }
        }

        private DbCommand _RemoveByIdCmd = null;
        public DbCommand RemoveByIdCmd {
            get {
                if(_RemoveByIdCmd == null) {
                    _RemoveByIdCmd = myRepoFactory.CreateCommand();
                    string whereClause = string.Empty;
                    List<PropertyInfo> props = typeof(FAKategorie).GetProperties().ToList();
                    myRepoFactory.GetColumsForTable("FAKategorie", true).ForEach(c => {
                        PropertyInfo prop = props.Where(p => p.Name == c.ColumnName).FirstOrDefault();
                        if(prop != null && c.IsIdentity) {
                            if(whereClause.Length == 0) {
                                whereClause = "where ";
                            } else {
                                whereClause += " and ";
                            }
                            whereClause += string.Format("[{0}] = @{0}", c.ColumnName);
                            _RemoveByIdCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, string.Format("@{0}", c.ColumnName), null, size: c.maxLenght, precision: c.precision, scale: c.scale);
                        }
                    });
                    _RemoveByIdCmd.CommandText = string.Format("delete from FAKategorie {0} ", whereClause);
                    _RemoveByIdCmd.Prepare();
                }
                return _RemoveByIdCmd;
            }
        }

        private DbCommand _FindFAKategorieByIdCmd = null;
        public DbCommand FindFAKategorieByIdCmd {
            get {
                if(_FindFAKategorieByIdCmd == null) {
                    _FindFAKategorieByIdCmd = myRepoFactory.CreateCommand();
                    string whereClause = string.Empty;
                    List<PropertyInfo> props = typeof(FAKategorie).GetProperties().ToList();
                    myRepoFactory.GetColumsForTable("FAKategorie", true).ForEach(c => {
                        PropertyInfo prop = props.Where(p => p.Name == c.ColumnName).FirstOrDefault();
                        if(prop != null && c.IsIdentity) {
                            if(whereClause.Length == 0) {
                                whereClause = "where ";
                            } else {
                                whereClause += " and ";
                            }
                            whereClause += string.Format("[{0}] = @{0}", c.ColumnName);
                            _FindFAKategorieByIdCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, string.Format("@{0}", c.ColumnName), null, size: c.maxLenght, precision: c.precision, scale: c.scale);
                        }
                    });

                    _FindFAKategorieByIdCmd.CommandText = string.Format("select top 1 * from FAKategorie {0}", whereClause);
                    _FindFAKategorieByIdCmd.Prepare();
                }
                return _FindFAKategorieByIdCmd;
            }
        }


        #endregion



        #region Konstruktor

        public FAKategorieRepo(ILogging.ILoggingFactory logFactory) {
            Logger = logFactory.GetCurrentClassLogger();
            myRepoFactory = RepositoryFactory.theRepoFactory as RepositoryFactory;
        }

        #endregion



        #region IFAKategorieRep Member

        public int Add(FAKategorie faKategorie) {
            FillCommandParameter(AddCmd, faKategorie);
            faKategorie.FAKategorieId = (int)AddCmd.ExecuteScalar();
            return faKategorie.FAKategorieId;
        }

        public void Update(FAKategorie faKategorie) {
            FillCommandParameter(UpdateCmd, faKategorie);
            int x = UpdateCmd.ExecuteNonQuery();
        }

        public void RemoveById(int id) {
            FillCommandParameter(RemoveByIdCmd, new object[] { id });
            int anzahl = RemoveByIdCmd.ExecuteNonQuery();
        }

        public FAKategorie FindFAKategorieById(int id) {
            FAKategorie myEntity = null;
            FillCommandParameter(FindFAKategorieByIdCmd, new object[] { id });
            using(DbDataReader dbDr = FindFAKategorieByIdCmd.ExecuteReader()) {
                if(dbDr.Read()) {
                    myEntity = new FAKategorie();
                    myEntity.FillEntityWithDataReader(dbDr);
                }
            }
            return myEntity;
        }

        public List<FAKategorie> GetFAKategorieByQueryData(FAKategorieQueryData queryData) {
            List<FAKategorie> wrkList = new List<FAKategorie>();

            // Durch das ständig anders aussehende SQL-Statement wird in diesem Fall von einem vorgefertigten
            // Prepared-Statement wie in den anderen Fällen abgesehen und das DbCommand jedesmal neu,
            // entsprechend den queryData erstellt.
            using(DbCommand cmd = myRepoFactory.CreateCommand()) {
                cmd.CommandText = "select * from FAKategorie k1 ";
                if(queryData != null) {
                    List<string> whereClauses = new List<string>();
                    if(!string.IsNullOrWhiteSpace(queryData.FAKennzeichenEA)) {
                        whereClauses.Add("FAKennzeichenEA = @FAKennzeichenEA");
                        cmd.AddCmdParameter(DbType.String, ParameterDirection.Input, "@FAKennzeichenEA",
                                            queryData.FAKennzeichenEA, size: 1, precision: null, scale: null);
                    }
                    if(queryData.GueltFuerJahr != null) {
                        whereClauses.Add("FAJahrGueltigAb = (select top 1 FAJahrGueltigAb " +
                                         "                   from FAKategorie k2 " +
                                         "                   where FAJahrGueltigAb <= @FAJahrGueltigAb " +
                                         "                   order by FAJahrGueltigAb desc) ");
                        cmd.AddCmdParameter(DbType.Int32, ParameterDirection.Input, "@FAJahrGueltigAb",
                                            queryData.GueltFuerJahr, null, 4, 0);
                    }

                    string whereClause = string.Empty;
                    whereClauses.ForEach((w) => {
                        if(string.IsNullOrWhiteSpace(whereClause)) {
                            whereClause += " where " + w + " ";
                        } else {
                            whereClause += " and " + w + " ";
                        }
                    });
                    cmd.CommandText += whereClause;
                }
                cmd.Prepare();
                using(DbDataReader dbDr = cmd.ExecuteReader()) {
                    while(dbDr.Read()) {
                        FAKategorie faKat = new FAKategorie();
                        faKat.FillEntityWithDataReader(dbDr);
                        wrkList.Add(faKat);
                    }
                }
            }
            return wrkList;
        }


        #endregion




    }
}
