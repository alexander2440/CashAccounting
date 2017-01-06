using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using CashAccountingEntities;
using CashAccountingIRepo;

namespace CashAccountingRepoMSSQL {
    public class BelegRepo : RepoBase, IBelegRepo {

        private RepositoryFactory myRepoFactory = null;

        private ILogging.ILog Logger = null;

        #region Prepared Commands

        private DbCommand _AddBelegCmd = null;
        public DbCommand AddBelegCmd {
            get {
                if (_AddBelegCmd == null) {
                    _AddBelegCmd = myRepoFactory.CreateCommand();
                    string fieldList = string.Empty;
                    string valueList = string.Empty;
                    List<PropertyInfo> props = typeof(Beleg).GetProperties().ToList();
                    myRepoFactory.GetColumsForTable("Beleg", false).ForEach(c => {
                        PropertyInfo prop = props.Where(p => p.Name == c.ColumnName).FirstOrDefault();
                        if (prop != null) {
                            if (fieldList.Length > 0) {
                                fieldList += ", ";
                                valueList += ", ";
                            }
                            fieldList += string.Format("[{0}]", c.ColumnName);
                            valueList += string.Format("@{0}", c.ColumnName);
                            _AddBelegCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, string.Format("@{0}", c.ColumnName), null, size: c.maxLenght, precision: c.precision, scale: c.scale);
                        }
                    });
                    _AddBelegCmd.CommandText = string.Format("Insert into Beleg ({0}) OUTPUT INSERTED.BelegId values ({1})", fieldList, valueList);
                    _AddBelegCmd.Prepare();
                }
                return _AddBelegCmd;
            }
        }

        private DbCommand _RemoveBelegByIdCmd = null;
        public DbCommand RemoveBelegByIdCmd {
            get {
                if (_RemoveBelegByIdCmd == null) {
                    _RemoveBelegByIdCmd = myRepoFactory.CreateCommand();
                    string whereClause = string.Empty;
                    List<PropertyInfo> props = typeof(Beleg).GetProperties().ToList();
                    myRepoFactory.GetColumsForTable("Beleg", true).ForEach(c => {
                        PropertyInfo prop = props.Where(p => p.Name == c.ColumnName).FirstOrDefault();
                        if (prop != null && c.IsIdentity) {
                            if (whereClause.Length == 0) {
                                whereClause = "where ";
                            } else {
                                whereClause += " and ";
                            }
                            whereClause += string.Format("[{0}] = @{0}", c.ColumnName);
                            _RemoveBelegByIdCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, string.Format("@{0}", c.ColumnName), null, size: c.maxLenght, precision: c.precision, scale: c.scale);
                        }
                    });
                    _RemoveBelegByIdCmd.CommandText = string.Format("delete from Beleg {0} ", whereClause);
                    _RemoveBelegByIdCmd.Prepare();
                }
                return _RemoveBelegByIdCmd;
            }
        }

        private DbCommand _FindBelegByIdCmd = null;
        public DbCommand FindBelegByIdCmd {
            get {
                if (_FindBelegByIdCmd == null) {
                    _FindBelegByIdCmd = myRepoFactory.CreateCommand();
                    string whereClause = string.Empty;
                    List<PropertyInfo> props = typeof(Beleg).GetProperties().ToList();
                    myRepoFactory.GetColumsForTable("Beleg", true).ForEach(c => {
                        PropertyInfo prop = props.Where(p => p.Name == c.ColumnName).FirstOrDefault();
                        if (prop != null && c.IsIdentity) {
                            if (whereClause.Length == 0) {
                                whereClause = "where ";
                            } else {
                                whereClause += " and ";
                            }
                            whereClause += string.Format("[{0}] = @{0}", c.ColumnName);
                            _FindBelegByIdCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, string.Format("@{0}", c.ColumnName), null, size: c.maxLenght, precision: c.precision, scale: c.scale);
                        }
                    });

                    _FindBelegByIdCmd.CommandText = string.Format("select top 1 * from Beleg {0}", whereClause);
                    _FindBelegByIdCmd.Prepare();
                }
                return _FindBelegByIdCmd;
            }
        }

        private DbCommand _UpdateBelegkopfCmd = null;
        public DbCommand UpdateBelegkopfCmd {
            get {
                if (_UpdateBelegkopfCmd == null) {
                    _UpdateBelegkopfCmd = myRepoFactory.CreateCommand();
                    string setList = string.Empty;
                    string identityList = string.Empty;
                    List<PropertyInfo> props = typeof(Beleg).GetProperties().ToList();
                    myRepoFactory.GetColumsForTable("Beleg", true).ForEach(c => {
                        PropertyInfo prop = props.Where(p => p.Name == c.ColumnName).FirstOrDefault();
                        if (prop != null) {
                            if (c.IsIdentity) {
                                if (identityList.Length == 0) {
                                    identityList = "where ";
                                } else {
                                    identityList += " and ";
                                }
                                identityList += string.Format("[{0}] = @{0}", c.ColumnName);
                            } else {
                                if (setList.Length > 0) {
                                    setList += ", ";
                                }
                                setList += string.Format("[{0}] = @{0}", c.ColumnName);
                            }
                            _UpdateBelegkopfCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, string.Format("@{0}", c.ColumnName), null, size: c.maxLenght, precision: c.precision, scale: c.scale);
                        }
                    });
                    _UpdateBelegkopfCmd.CommandText = string.Format("Update Beleg set {0} {1} ", setList, identityList);
                    _UpdateBelegkopfCmd.Prepare();
                }
                return _UpdateBelegkopfCmd;
            }
        }

        private DbCommand _AddBelegPositionCmd = null;
        public DbCommand AddBelegPositionCmd {
            get {
                if (_AddBelegPositionCmd == null) {
                    _AddBelegPositionCmd = myRepoFactory.CreateCommand();
                    string fieldList = string.Empty;
                    string valueList = string.Empty;
                    List<PropertyInfo> props = typeof(Belegposition).GetProperties().ToList();
                    myRepoFactory.GetColumsForTable("Belegposition", false).ForEach(c => {
                        PropertyInfo prop = props.Where(p => p.Name == c.ColumnName).FirstOrDefault();
                        if (prop != null) {
                            if (fieldList.Length > 0) {
                                fieldList += ", ";
                                valueList += ", ";
                            }
                            fieldList += string.Format("[{0}]", c.ColumnName);
                            valueList += string.Format("@{0}", c.ColumnName);
                            _AddBelegPositionCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, string.Format("@{0}", c.ColumnName), null, size: c.maxLenght, precision: c.precision, scale: c.scale);
                        }
                    });
                    _AddBelegPositionCmd.CommandText = string.Format("Insert into Belegposition ({0}) OUTPUT INSERTED.BelegpositionId values ({1})", fieldList, valueList);
                    _AddBelegPositionCmd.Prepare();
                }
                return _AddBelegPositionCmd;
            }
        }

        private DbCommand _UpdateBelegPositionCmd = null;
        public DbCommand UpdateBelegPositionCmd {
            get {
                if (_UpdateBelegPositionCmd == null) {
                    _UpdateBelegPositionCmd = myRepoFactory.CreateCommand();
                    string setList = string.Empty;
                    string identityList = string.Empty;
                    List<PropertyInfo> props = typeof(Belegposition).GetProperties().ToList();
                    myRepoFactory.GetColumsForTable("Belegposition", true).ForEach(c => {
                        PropertyInfo prop = props.Where(p => p.Name == c.ColumnName).FirstOrDefault();
                        if (prop != null) {
                            if (c.IsIdentity) {
                                if (identityList.Length == 0) {
                                    identityList = "where ";
                                } else {
                                    identityList += " and ";
                                }
                                identityList += string.Format("[{0}] = @{0}", c.ColumnName);
                            } else {
                                if (setList.Length > 0) {
                                    setList += ", ";
                                }
                                setList += string.Format("[{0}] = @{0}", c.ColumnName);
                            }
                            _UpdateBelegPositionCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, string.Format("@{0}", c.ColumnName), null, size: c.maxLenght, precision: c.precision, scale: c.scale);
                        }
                    });
                    _UpdateBelegPositionCmd.CommandText = string.Format("Update Belegposition set {0} {1} ", setList, identityList);
                    _UpdateBelegPositionCmd.Prepare();
                }
                return _UpdateBelegPositionCmd;
            }
        }

        private DbCommand _RemoveBelegPositionByIdCmd = null;
        public DbCommand RemoveBelegPositionByIdCmd {
            get {
                if (_RemoveBelegPositionByIdCmd == null) {
                    _RemoveBelegPositionByIdCmd = myRepoFactory.CreateCommand();
                    string typename = typeof(Belegposition).Name;

                    ColumnDescription IdCol = myRepoFactory.GetColumsForTable(typename, true).Where(w => w.IsIdentity).FirstOrDefault();
                    if (IdCol == null)
                        throw new ConstraintException($"No ID-column found for table '{typename}'");
                    PropertyInfo prop = typeof(Belegposition).GetProperty(IdCol.ColumnName);
                    if (prop == null)
                        throw new ConstraintException($"No property named '{IdCol.ColumnName}' found in type '{typename}'");

                    _RemoveBelegPositionByIdCmd.CommandText = $"delete from Belegposition where [{IdCol.ColumnName}] = @{IdCol.ColumnName} ";
                    _RemoveBelegPositionByIdCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, $"@{IdCol.ColumnName}",
                        null, size: IdCol.maxLenght, precision: IdCol.precision, scale: IdCol.scale);
                    _RemoveBelegPositionByIdCmd.Prepare();
                }
                return _RemoveBelegPositionByIdCmd;
            }
        }

        private DbCommand _FindBelegPositionenByBelegIdCmd = null;
        public DbCommand FindBelegPositionenByBelegIdCmd {
            get {
                if (_FindBelegPositionenByBelegIdCmd == null) {
                    _FindBelegPositionenByBelegIdCmd = myRepoFactory.CreateCommand();
                    _FindBelegPositionenByBelegIdCmd.CommandText = string.Format("select * from Belegposition where BelegId = @BelegId");
                    _FindBelegPositionenByBelegIdCmd.AddCmdParameter(typeof(int), ParameterDirection.Input, "@BelegId", null, size: null, precision: null, scale: null);
                    _FindBelegPositionenByBelegIdCmd.Prepare();
                }
                return _FindBelegPositionenByBelegIdCmd;
            }
        }

        private DbCommand _AddBelegZahlungCmd = null;
        public DbCommand AddBelegZahlungCmd {
            get {
                if (_AddBelegZahlungCmd == null) {
                    _AddBelegZahlungCmd = myRepoFactory.CreateCommand();
                    string fieldList = string.Empty;
                    string valueList = string.Empty;
                    List<PropertyInfo> props = typeof(BelegZahlungKopf).GetProperties().ToList();
                    myRepoFactory.GetColumsForTable("BelegZahlungKopf", false).ForEach(c => {
                        PropertyInfo prop = props.Where(p => p.Name == c.ColumnName).FirstOrDefault();
                        if (prop != null) {
                            if (fieldList.Length > 0) {
                                fieldList += ", ";
                                valueList += ", ";
                            }
                            fieldList += string.Format("[{0}]", c.ColumnName);
                            valueList += string.Format("@{0}", c.ColumnName);
                            _AddBelegZahlungCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, string.Format("@{0}", c.ColumnName), null, size: c.maxLenght, precision: c.precision, scale: c.scale);
                        }
                    });
                    _AddBelegZahlungCmd.CommandText = string.Format("Insert into BelegZahlungKopf ({0}) OUTPUT INSERTED.ZahlungKopfId values ({1})", fieldList, valueList);
                    _AddBelegZahlungCmd.Prepare();
                }
                return _AddBelegZahlungCmd;
            }
        }

        private DbCommand _UpdateBelegZahlungCmd = null;
        public DbCommand UpdateBelegZahlungCmd {
            get {
                if (_UpdateBelegZahlungCmd == null) {
                    _UpdateBelegZahlungCmd = myRepoFactory.CreateCommand();
                    string setList = string.Empty;
                    string identityList = string.Empty;
                    List<PropertyInfo> props = typeof(BelegZahlungKopf).GetProperties().ToList();
                    myRepoFactory.GetColumsForTable("BelegZahlungKopf", true).ForEach(c => {
                        PropertyInfo prop = props.Where(p => p.Name == c.ColumnName).FirstOrDefault();
                        if (prop != null) {
                            if (c.IsIdentity) {
                                if (identityList.Length == 0) {
                                    identityList = "where ";
                                } else {
                                    identityList += " and ";
                                }
                                identityList += string.Format("[{0}] = @{0}", c.ColumnName);
                            } else {
                                if (setList.Length > 0) {
                                    setList += ", ";
                                }
                                setList += string.Format("[{0}] = @{0}", c.ColumnName);
                            }
                            _UpdateBelegZahlungCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, string.Format("@{0}", c.ColumnName), null, size: c.maxLenght, precision: c.precision, scale: c.scale);
                        }
                    });
                    _UpdateBelegZahlungCmd.CommandText = string.Format("Update BelegZahlungKopf set {0} {1} ", setList, identityList);
                    _UpdateBelegZahlungCmd.Prepare();
                }
                return _UpdateBelegZahlungCmd;
            }
        }

        private DbCommand _RemoveBelegZahlungByIdCmd = null;
        public DbCommand RemoveBelegZahlungByIdCmd {
            get {
                if (_RemoveBelegZahlungByIdCmd == null) {
                    _RemoveBelegZahlungByIdCmd = myRepoFactory.CreateCommand();
                    string whereClause = string.Empty;
                    List<PropertyInfo> props = typeof(BelegZahlungKopf).GetProperties().ToList();
                    myRepoFactory.GetColumsForTable("BelegZahlungKopf", true).ForEach(c => {
                        PropertyInfo prop = props.Where(p => p.Name == c.ColumnName).FirstOrDefault();
                        if (prop != null && c.IsIdentity) {
                            if (whereClause.Length == 0) {
                                whereClause = "where ";
                            } else {
                                whereClause += " and ";
                            }
                            whereClause += string.Format("[{0}] = @{0}", c.ColumnName);
                            _RemoveBelegZahlungByIdCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, string.Format("@{0}", c.ColumnName), null, size: c.maxLenght, precision: c.precision, scale: c.scale);
                        }
                    });
                    _RemoveBelegZahlungByIdCmd.CommandText = string.Format("delete from BelegZahlungKopf {0} ", whereClause);
                    _RemoveBelegZahlungByIdCmd.Prepare();
                }
                return _RemoveBelegZahlungByIdCmd;
            }
        }

        private DbCommand _FindBelegZahlungByBelegIdCmd = null;
        public DbCommand FindBelegZahlungByBelegIdCmd {
            get {
                if (_FindBelegZahlungByBelegIdCmd == null) {
                    _FindBelegZahlungByBelegIdCmd = myRepoFactory.CreateCommand();
                    _FindBelegZahlungByBelegIdCmd.CommandText = string.Format("select * from BelegZahlungKopf where BelegId = @BelegId");
                    _FindBelegZahlungByBelegIdCmd.AddCmdParameter(typeof(int), ParameterDirection.Input, "@BelegId", null, size: null, precision: null, scale: null);
                    _FindBelegZahlungByBelegIdCmd.Prepare();
                }
                return _FindBelegZahlungByBelegIdCmd;
            }
        }

        private DbCommand _GetLandAllCmd = null;
        public DbCommand GetLandAllCmd {
            get {
                if (_GetLandAllCmd == null) {
                    _GetLandAllCmd = myRepoFactory.CreateCommand();
                    _GetLandAllCmd.CommandText = "select * from Land ";
                    _GetLandAllCmd.Prepare();
                }
                return _GetLandAllCmd;
            }
        }


        private DbCommand _AddBelegZahlungPositionCmd = null;
        public DbCommand AddBelegZahlungPositionCmd {
            get {
                if (_AddBelegZahlungPositionCmd == null) {
                    _AddBelegZahlungPositionCmd = myRepoFactory.CreateCommand();
                    string fieldList = string.Empty;
                    string valueList = string.Empty;
                    List<PropertyInfo> props = typeof(BelegZahlungPosition).GetProperties().ToList();
                    myRepoFactory.GetColumsForTable("BelegZahlungPosition", false).ForEach(c => {
                        PropertyInfo prop = props.Where(p => p.Name == c.ColumnName).FirstOrDefault();
                        if (prop != null) {
                            if (fieldList.Length > 0) {
                                fieldList += ", ";
                                valueList += ", ";
                            }
                            fieldList += string.Format("[{0}]", c.ColumnName);
                            valueList += string.Format("@{0}", c.ColumnName);
                            _AddBelegZahlungPositionCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, string.Format("@{0}", c.ColumnName), null, size: c.maxLenght, precision: c.precision, scale: c.scale);
                        }
                    });
                    _AddBelegZahlungPositionCmd.CommandText = string.Format("Insert into BelegZahlungPosition ({0}) OUTPUT INSERTED.ZahlungPositionId values ({1})", fieldList, valueList);
                    _AddBelegZahlungPositionCmd.Prepare();
                }
                return _AddBelegZahlungPositionCmd;
            }
        }

        private DbCommand _UpdateBelegZahlungPositionCmd = null;
        public DbCommand UpdateBelegZahlungPositionCmd {
            get {
                if (_UpdateBelegZahlungPositionCmd == null) {
                    _UpdateBelegZahlungPositionCmd = myRepoFactory.CreateCommand();
                    string setList = string.Empty;
                    string identityList = string.Empty;
                    List<PropertyInfo> props = typeof(BelegZahlungPosition).GetProperties().ToList();
                    myRepoFactory.GetColumsForTable("BelegZahlungPosition", true).ForEach(c => {
                        PropertyInfo prop = props.Where(p => p.Name == c.ColumnName).FirstOrDefault();
                        if (prop != null) {
                            if (c.IsIdentity) {
                                if (identityList.Length == 0) {
                                    identityList = "where ";
                                } else {
                                    identityList += " and ";
                                }
                                identityList += string.Format("[{0}] = @{0}", c.ColumnName);
                            } else {
                                if (setList.Length > 0) {
                                    setList += ", ";
                                }
                                setList += string.Format("[{0}] = @{0}", c.ColumnName);
                            }
                            _UpdateBelegZahlungPositionCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, string.Format("@{0}", c.ColumnName), null, size: c.maxLenght, precision: c.precision, scale: c.scale);
                        }
                    });
                    _UpdateBelegZahlungPositionCmd.CommandText = string.Format("Update BelegZahlungPosition set {0} {1} ", setList, identityList);
                    _UpdateBelegZahlungPositionCmd.Prepare();
                }
                return _UpdateBelegZahlungPositionCmd;
            }
        }

        private DbCommand _RemoveBelegZahlungPositionByIdCmd = null;
        public DbCommand RemoveBelegZahlungPositionByIdCmd {
            get {
                if (_RemoveBelegZahlungPositionByIdCmd == null) {
                    _RemoveBelegZahlungPositionByIdCmd = myRepoFactory.CreateCommand();
                    string whereClause = string.Empty;
                    List<PropertyInfo> props = typeof(BelegZahlungPosition).GetProperties().ToList();
                    myRepoFactory.GetColumsForTable("BelegZahlungPosition", true).ForEach(c => {
                        PropertyInfo prop = props.Where(p => p.Name == c.ColumnName).FirstOrDefault();
                        if (prop != null && c.IsIdentity) {
                            if (whereClause.Length == 0) {
                                whereClause = "where ";
                            } else {
                                whereClause += " and ";
                            }
                            whereClause += string.Format("[{0}] = @{0}", c.ColumnName);
                            _RemoveBelegZahlungPositionByIdCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, string.Format("@{0}", c.ColumnName), null, size: c.maxLenght, precision: c.precision, scale: c.scale);
                        }
                    });
                    _RemoveBelegZahlungPositionByIdCmd.CommandText = string.Format("delete from BelegZahlungPosition {0} ", whereClause);
                    _RemoveBelegZahlungPositionByIdCmd.Prepare();
                }
                return _RemoveBelegZahlungPositionByIdCmd;
            }
        }

        private DbCommand _FindBelegZahlungPositionenByZahlungIdCmd = null;
        public DbCommand FindBelegZahlungPositionenByZahlungIdCmd {
            get {
                if (_FindBelegZahlungPositionenByZahlungIdCmd == null) {
                    _FindBelegZahlungPositionenByZahlungIdCmd = myRepoFactory.CreateCommand();
                    _FindBelegZahlungPositionenByZahlungIdCmd.CommandText = string.Format("select * from BelegZahlungPosition where ZahlungKopfId = @ZahlungKopfId");
                    _FindBelegZahlungPositionenByZahlungIdCmd.AddCmdParameter(typeof(int), ParameterDirection.Input, "@ZahlungKopfId", null, size: null, precision: null, scale: null);
                    _FindBelegZahlungPositionenByZahlungIdCmd.Prepare();
                }
                return _FindBelegZahlungPositionenByZahlungIdCmd;
            }
        }

        private DbCommand _BelegZahlungPosSetBelegPosToNullCmd = null;
        public DbCommand BelegZahlungPosSetBelegPosToNullCmd {
            get {
                if (_BelegZahlungPosSetBelegPosToNullCmd == null) {
                    string updColName = "BelegpositionId";
                    string tablename = typeof(BelegZahlungPosition).Name;
                    ColumnDescription col4Update = myRepoFactory.GetColumsForTable(tablename, true)
                        .Where(w => w.ColumnName == updColName).FirstOrDefault();
                    if (col4Update == null)
                        throw new ConstraintException($"Column '{updColName}' in table '{tablename}' not found.");
                    PropertyInfo prop = typeof(BelegZahlungPosition).GetProperty(updColName);
                    if (prop == null)
                        throw new ConstraintException($"Property '{updColName}' in type '{tablename}' not found.");

                    _BelegZahlungPosSetBelegPosToNullCmd = myRepoFactory.CreateCommand();
                    _BelegZahlungPosSetBelegPosToNullCmd.CommandText = 
                        $"Update {tablename} set {updColName} = null where {updColName} = @{updColName} ";
                    _BelegZahlungPosSetBelegPosToNullCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, $"@{updColName}", 
                        null, size: col4Update.maxLenght, precision: col4Update.precision, scale: col4Update.scale);
                    _BelegZahlungPosSetBelegPosToNullCmd.Prepare();
                }
                return _BelegZahlungPosSetBelegPosToNullCmd;
            }
        }

        private DbCommand _BelegZahlungSetBelegToNullCmd = null;
        public DbCommand BelegZahlungSetBelegToNullCmd {
            get {
                if (_BelegZahlungSetBelegToNullCmd == null) {
                    string updColName = "BelegId";
                    string tablename = typeof(BelegZahlungKopf).Name;
                    ColumnDescription col4Update = myRepoFactory.GetColumsForTable(tablename, true)
                        .Where(w => w.ColumnName == updColName).FirstOrDefault();
                    if (col4Update == null)
                        throw new ConstraintException($"Column '{updColName}' in table '{tablename}' not found.");
                    PropertyInfo prop = typeof(BelegZahlungKopf).GetProperty(updColName);
                    if (prop == null)
                        throw new ConstraintException($"Property '{updColName}' in type '{tablename}' not found.");

                    _BelegZahlungSetBelegToNullCmd = myRepoFactory.CreateCommand();
                    _BelegZahlungSetBelegToNullCmd.CommandText =
                        $"Update {tablename} set {updColName} = null where {updColName} = @{updColName} ";
                    _BelegZahlungSetBelegToNullCmd.AddCmdParameter(prop.PropertyType, ParameterDirection.Input, $"@{updColName}",
                        null, size: col4Update.maxLenght, precision: col4Update.precision, scale: col4Update.scale);
                    _BelegZahlungSetBelegToNullCmd.Prepare();
                }
                return _BelegZahlungSetBelegToNullCmd;
            }
        }

        #endregion



        #region Konstruktor

        public BelegRepo(ILogging.ILoggingFactory logFactory) {
            Logger = logFactory.GetCurrentClassLogger();
            myRepoFactory = RepositoryFactory.theRepoFactory as RepositoryFactory;
        }

        #endregion



        #region IBelegRep Member

        public int SaveBeleg(Beleg beleg) {
            if (beleg.BelegId == 0) {
                return AddBeleg(beleg);
            } else {
                UpdateBeleg(beleg);
                return beleg.BelegId;
            }
        }

        private int AddBeleg(Beleg beleg) {
            FillCommandParameter(AddBelegCmd, beleg);
            int newId = (int)AddBelegCmd.ExecuteScalar();
            beleg.BelegId = newId;
            beleg.Positionen.ForEach((p) => {
                p.BelegId = beleg.BelegId;
                int posId = SaveBelegPosition(beleg.BelegId, p);
                p.BelegpositionId = posId;
            });
            return beleg.BelegId;
        }

        private void UpdateBeleg(Beleg beleg) {
            UpdateBelegkopf(beleg);
            // bestehende Positionen aktualisieren bzw. neue Positionen hinzufügen
            beleg.Positionen.ForEach((p) => {
                SaveBelegPosition(beleg.BelegId, p);
            });
            // entfernte Positionen lokalisieren und aus Beleg entfernen
            List<Belegposition> altePos = FindBelegPositionenByBelegId(beleg.BelegId);
            altePos.ForEach(ap => {
                if (beleg.Positionen.Where(pos => pos.BelegpositionId == ap.BelegpositionId).Count() == 0) {
                    RemoveBelegPositionById(ap.BelegpositionId);
                }
            });
        }

        public void RemoveBelegById(int belegId) {
            FillCommandParameter(RemoveBelegByIdCmd, new object[] { belegId });
            int count = RemoveBelegByIdCmd.ExecuteNonQuery();
        }

        public Beleg FindBelegById(int belegId, bool includeBelegPosition) {
            Beleg beleg = null;
            FillCommandParameter(FindBelegByIdCmd, new object[] { belegId });

            using (DbDataReader dbDr = FindBelegByIdCmd.ExecuteReader()) {
                if (dbDr.Read()) {
                    beleg = new Beleg();
                    beleg.FillEntityWithDataReader(dbDr);
                    if (includeBelegPosition) {
                        beleg.Positionen = FindBelegPositionenByBelegId(beleg.BelegId);
                    } else {
                        beleg.Positionen = new List<Belegposition>();
                    }
                    beleg.Zahlungen = FindBelegZahlungByBelegId(beleg.BelegId);
                }
            }
            return beleg;
        }

        public List<Beleg> GetBelegByQueryData(BelegQueryData queryData, bool includeBelegPosition, bool includePayment) {

            List<Beleg> wrkList = new List<Beleg>();

            using (DbCommand cmd = myRepoFactory.CreateCommand()) {
                cmd.CommandText = "select * from Beleg ";
                List<string> whereConditions = genereateConditions(queryData, cmd);
                if (whereConditions.Count > 0) {
                    string condCombined = whereConditions.Select(w => w).Aggregate((s1, s2) => s1 + " and " + s2);
                    cmd.CommandText += "where " + condCombined;
                }
                using (DbDataReader dbDr = cmd.ExecuteReader()) {
                    while (dbDr.Read()) {
                        Beleg b = new Beleg();
                        b.FillEntityWithDataReader(dbDr);
                        if (includeBelegPosition) {
                            FindBelegPositionenByBelegId(b.BelegId).ForEach(p => b.Positionen.Add(p));
                        }
                        if (includePayment) {
                            FindBelegZahlungByBelegId(b.BelegId).ForEach(z => b.Zahlungen.Add(z));
                        }
                        wrkList.Add(b);
                    }
                }

            }

            return wrkList;
        }

        private List<string> genereateConditions(BelegQueryData queryData, DbCommand cmd) {
            List<string> cond = new List<string>();
            if (queryData.VonDatum != null) {
                string par = "@VonDatum";
                cond.Add(string.Format("BelegDatum >= {0}", par));
                cmd.AddCmdParameter(typeof(DateTime), ParameterDirection.Input, par, (DateTime)queryData.VonDatum);
            }
            if (queryData.BisDatum != null) {
                string par = "@BisDatum";
                cond.Add(string.Format("BelegDatum <= {0}", par));
                cmd.AddCmdParameter(typeof(DateTime), ParameterDirection.Input, par, (DateTime)queryData.BisDatum);
            }
            if (!string.IsNullOrWhiteSpace(queryData.BelegnummerIntern)) {
                string par = "@BelegnummerIntern";
                cond.Add(string.Format("BelegnummerIntern = {0}", par));
                cmd.AddCmdParameter(typeof(string), ParameterDirection.Input, par, queryData.BelegnummerIntern);

            }
            return cond;
        }

        public void UpdateBelegkopf(Beleg beleg) {
            FillCommandParameter(UpdateBelegkopfCmd, beleg);
            int count = UpdateBelegkopfCmd.ExecuteNonQuery();
        }


        public int SaveBelegPosition(int belegId, Belegposition belegPosition) {
            belegPosition.BelegId = belegId;
            if (belegPosition.BelegpositionId == 0) {
                return AddBelegPosition(belegId, belegPosition);
            } else {
                UpdateBelegPosition(belegPosition);
                return belegPosition.BelegpositionId;
            }
        }

        private int AddBelegPosition(int belegId, Belegposition belegPosition) {
            FillCommandParameter(AddBelegPositionCmd, belegPosition);
            int newPosId = (int)AddBelegPositionCmd.ExecuteScalar();
            belegPosition.BelegpositionId = newPosId;
            return belegPosition.BelegpositionId;
        }

        private void UpdateBelegPosition(Belegposition belegPosition) {
            FillCommandParameter(UpdateBelegPositionCmd, belegPosition);
            int anzUpdatedRecords = (int)UpdateBelegPositionCmd.ExecuteNonQuery();
        }

        public void RemoveBelegPositionById(int belegPositionId) {
            UnlinkBelegZahlungsPositionen(belegPositionId);
            FillCommandParameter(RemoveBelegPositionByIdCmd, new object[] { belegPositionId });
            int count = RemoveBelegPositionByIdCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Alle Zahlungspositionen von einer Belegposition lösen (z.B. wenn die Belegposition
        /// gelöscht werden soll, aber die Zahlung erhalten bleibt...)
        /// </summary>
        /// <param name="belegPositionId"></param>
        private void UnlinkBelegZahlungsPositionen(int belegPositionId) {
            FillCommandParameter(BelegZahlungPosSetBelegPosToNullCmd, new object[] { belegPositionId });
            BelegZahlungPosSetBelegPosToNullCmd.ExecuteNonQuery();
        }

        public List<Belegposition> FindBelegPositionenByBeleg(Beleg beleg) {
            return FindBelegPositionenByBelegId(beleg.BelegId);
        }

        public List<Belegposition> FindBelegPositionenByBelegId(int belegId) {
            List<Belegposition> positionen = new List<Belegposition>();
            FillCommandParameter(FindBelegPositionenByBelegIdCmd, new object[] { belegId });
            using (DbDataReader dbDr = FindBelegPositionenByBelegIdCmd.ExecuteReader()) {
                while (dbDr.Read()) {
                    Belegposition pos = new Belegposition();
                    pos.FillEntityWithDataReader(dbDr);
                    positionen.Add(pos);
                }
            }
            return positionen.Count == 0 ? null : positionen;
        }



        public int SaveBelegZahlung(int belegId, BelegZahlungKopf belegZahlung) {
            belegZahlung.BelegId = belegId;
            if (belegZahlung.ZahlungKopfId == 0) {
                return AddBelegZahlung(belegId, belegZahlung);
            } else {
                UpdateBelegZahlung(belegZahlung);
                return belegZahlung.ZahlungKopfId;
            }
        }

        private int AddBelegZahlung(int belegId, BelegZahlungKopf belegZahlung) {
            FillCommandParameter(AddBelegZahlungCmd, belegZahlung);
            int newZahlungId = (int)AddBelegZahlungCmd.ExecuteScalar();
            belegZahlung.ZahlungKopfId = newZahlungId;
            belegZahlung.ZahlungsPositionen.ForEach(p => {
                p.ZahlungKopfId = newZahlungId;
                SaveBelegZahlungPosition(newZahlungId, p);
            });
            return belegZahlung.ZahlungKopfId;
        }

        private void UpdateBelegZahlung(BelegZahlungKopf belegZahlung) {
            FillCommandParameter(UpdateBelegZahlungCmd, belegZahlung);
            int anzUpdatedRecords = (int)UpdateBelegZahlungCmd.ExecuteNonQuery();

            // bestehende Positionen aktualisieren bzw. neue Positionen hinzufügen
            belegZahlung.ZahlungsPositionen.ForEach((p) => {
                SaveBelegZahlungPosition(belegZahlung.ZahlungKopfId, p);
            });
            // entfernte Positionen lokalisieren und aus Beleg entfernen
            List<BelegZahlungPosition> altePos = FindBelegZahlungPositionZahlungId(belegZahlung.ZahlungKopfId);
            altePos.ForEach(ap => {
                if (belegZahlung.ZahlungsPositionen.Where(pos => pos.ZahlungPositionId == ap.ZahlungPositionId).Count() == 0) {
                    RemoveBelegZahlungPositionById(ap.ZahlungPositionId);
                }
            });
        }

        public void RemoveBelegZahlungById(int belegZahlungId) {
            FillCommandParameter(RemoveBelegZahlungByIdCmd, new object[] { belegZahlungId });
            int count = RemoveBelegZahlungByIdCmd.ExecuteNonQuery();
        }

        public List<BelegZahlungKopf> FindBelegZahlungByBeleg(Beleg beleg) {
            return FindBelegZahlungByBelegId(beleg.BelegId);
        }

        public List<BelegZahlungKopf> FindBelegZahlungByBelegId(int belegId) {
            List<BelegZahlungKopf> positionen = new List<BelegZahlungKopf>();
            FillCommandParameter(FindBelegZahlungByBelegIdCmd, new object[] { belegId });
            using (DbDataReader dbDr = FindBelegZahlungByBelegIdCmd.ExecuteReader()) {
                while (dbDr.Read()) {
                    BelegZahlungKopf zahl = new BelegZahlungKopf();
                    zahl.FillEntityWithDataReader(dbDr);
                    zahl.ZahlungsPositionen = FindBelegZahlungPositionByZahlung(zahl);
                    positionen.Add(zahl);
                }
            }
            return positionen;
        }



        public int SaveBelegZahlungPosition(int zahlungId, BelegZahlungPosition belegZahlungPosition) {
            belegZahlungPosition.ZahlungKopfId = zahlungId;
            if (belegZahlungPosition.ZahlungPositionId == 0) {
                return AddBelegZahlungPosition(zahlungId, belegZahlungPosition);
            } else {
                UpdateBelegZahlungPosition(belegZahlungPosition);
                return belegZahlungPosition.ZahlungPositionId;
            }
        }

        private int AddBelegZahlungPosition(int zahlungId, BelegZahlungPosition belegZahlungPosition) {
            FillCommandParameter(AddBelegZahlungPositionCmd, belegZahlungPosition);
            int newPosId = (int)AddBelegZahlungPositionCmd.ExecuteScalar();
            belegZahlungPosition.ZahlungPositionId = newPosId;
            return belegZahlungPosition.ZahlungPositionId;
        }

        private void UpdateBelegZahlungPosition(BelegZahlungPosition belegZahlungPosition) {
            FillCommandParameter(UpdateBelegZahlungPositionCmd, belegZahlungPosition);
            int anzUpdatedRecords = (int)UpdateBelegZahlungPositionCmd.ExecuteNonQuery();
        }

        public void RemoveBelegZahlungPositionById(int belegZahlungPositionId) {
            FillCommandParameter(RemoveBelegZahlungPositionByIdCmd, new object[] { belegZahlungPositionId });
            int count = RemoveBelegZahlungPositionByIdCmd.ExecuteNonQuery();
        }

        public List<BelegZahlungPosition> FindBelegZahlungPositionByZahlung(BelegZahlungKopf belegZahlung) {
            return FindBelegZahlungPositionZahlungId(belegZahlung.ZahlungKopfId);
        }

        public List<BelegZahlungPosition> FindBelegZahlungPositionZahlungId(int zahlungId) {
            List<BelegZahlungPosition> positionen = new List<BelegZahlungPosition>();
            FillCommandParameter(FindBelegZahlungPositionenByZahlungIdCmd, new object[] { zahlungId });
            using (DbDataReader dbDr = FindBelegZahlungPositionenByZahlungIdCmd.ExecuteReader()) {
                while (dbDr.Read()) {
                    BelegZahlungPosition pos = new BelegZahlungPosition();
                    pos.FillEntityWithDataReader(dbDr);
                    positionen.Add(pos);
                }
            }
            return positionen.Count == 0 ? null : positionen;
        }




        public List<Land> GetLandAll() {
            List<Land> wrkList = new List<Land>();
            using (DbDataReader dbDr = GetLandAllCmd.ExecuteReader()) {
                while (dbDr.Read()) {
                    Land land = new Land();
                    land.FillEntityWithDataReader(dbDr);
                    wrkList.Add(land);
                }
            }
            return wrkList;

        }

        #endregion
    }
}
