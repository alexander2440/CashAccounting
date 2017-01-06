using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CashAccountingRepoMSSQL {
    internal static class ExtensionMethods {

        public static void FillEntityWithDataReader<T>(this T entity, DbDataReader dataReader) {

            List<PropertyInfo> props = typeof(T).GetProperties().ToList();
            foreach(DataRow row in dataReader.GetSchemaTable().Rows) {
                PropertyInfo prop = props.Where(p => p.Name == (string)row["ColumnName"]).FirstOrDefault();
                if(prop != null) {
                    try {
                        prop.SetValue(entity, dataReader[prop.Name] == DBNull.Value ? null : dataReader[prop.Name]);
                    } catch(IndexOutOfRangeException ex) {
                        throw new Exception($"Error while setting the value for property '{prop.Name}'.", ex);
                    }
                }
            }

        }


        internal static DbParameter AddCmdParameter(this DbCommand cmd, DbType dbType, ParameterDirection direction, string parameterName, object parameterValue,
                                                    int? size = null, byte? precision = null, byte? scale = null) {
            DbParameter par = cmd.CreateParameter();
            par.DbType = dbType;
            par.Direction = direction;
            par.ParameterName = parameterName;
            if (size != null) {
                par.Size = (int)size;
            }
            if (precision != null && scale != null) {
                ((IDbDataParameter)par).Precision = (byte)precision;
                ((IDbDataParameter)par).Scale = (byte)scale;
            }
            par.Value = parameterValue ?? DBNull.Value;
            cmd.Parameters.Add(par);
            return par;
        }

        internal static DbParameter AddCmdParameter(this DbCommand cmd, Type typ, ParameterDirection direction, string parameterName, object parameterValue,
                                            int? size = null, byte? precision = null, byte? scale = null) {
            DbParameter par = cmd.CreateParameter();
            par.DbType = convertTypeToDbType(typ);
            par.Direction = direction;
            par.ParameterName = parameterName;
            if (size != null) {
                par.Size = (int)size;
            }
            if (precision != null && scale != null) {
                ((IDbDataParameter)par).Precision = (byte)precision;
                ((IDbDataParameter)par).Scale = (byte)scale;
            }
            par.Value = parameterValue ?? DBNull.Value;
            cmd.Parameters.Add(par);
            return par;
        }


        public static T GetNullableDbReaderField<T>(this object dbReaderField) {
            return dbReaderField == DBNull.Value ? default(T) : (T)dbReaderField;
        }



        private static DbType convertTypeToDbType(Type typ) {
            TypeCode tc;

            Type uTyp = Nullable.GetUnderlyingType(typ);
            if (uTyp == null) {
                tc = Type.GetTypeCode(typ);
            } else {
                tc = Type.GetTypeCode(uTyp);
            }
            switch (tc) {
                case TypeCode.Boolean:
                    return DbType.Boolean;
                case TypeCode.Byte:
                    return DbType.Byte;
                case TypeCode.Char:
                    return DbType.String;
                case TypeCode.DBNull:
                    return DbType.Object;
                case TypeCode.DateTime:
                    return DbType.DateTime;
                case TypeCode.Decimal:
                    return DbType.Decimal;
                case TypeCode.Double:
                    return DbType.Double;
                case TypeCode.Empty:
                    return DbType.Object;
                case TypeCode.Int16:
                    return DbType.Int16;
                case TypeCode.Int32:
                    return DbType.Int32;
                case TypeCode.Int64:
                    return DbType.Int64;
                case TypeCode.Object:
                    return DbType.Object;
                case TypeCode.SByte:
                    return DbType.SByte;
                case TypeCode.Single:
                    return DbType.Single;
                case TypeCode.String:
                    return DbType.String;
                case TypeCode.UInt16:
                    return DbType.UInt16;
                case TypeCode.UInt32:
                    return DbType.UInt32;
                case TypeCode.UInt64:
                    return DbType.UInt64;
                default:
                    return DbType.Object;
            }
        }


    }
}
