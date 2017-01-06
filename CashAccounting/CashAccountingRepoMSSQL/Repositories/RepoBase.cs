using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CashAccountingRepoMSSQL {
    public abstract class RepoBase : IDisposable {
        #region IDisposable Members


        protected virtual void FillCommandParameter<T>(DbCommand cmd, T entity) {
            foreach(DbParameter par in cmd.Parameters) {
                string parName = par.ParameterName.Substring(1);
                SetDbCommanParameter(par, typeof(T).GetProperty(parName).GetValue(entity));
            }
        }

        protected virtual void FillCommandParameter(DbCommand cmd, params object[] parValues) {
            if(cmd.Parameters.Count != parValues.LongLength) {
                throw new ArgumentException(string.Format("DbCommand expects {0} parameters to be supplied put {1} have been provided!"));
            }
            int i = 0;
            foreach(DbParameter par in cmd.Parameters) {
                string parName = par.ParameterName.Substring(1);
                SetDbCommanParameter(par, parValues[i]);
            }
        }

        private void SetDbCommanParameter(DbParameter par, object parValue) {
            par.Value = parValue ?? DBNull.Value;
        }

        public void Dispose() {
            // Dispose all Commands
            List<PropertyInfo> props = this.GetType().GetProperties(BindingFlags.Instance).ToList();
            foreach(PropertyInfo prop in props) {
                object propVal = prop.GetValue(this);
                if(propVal is DbCommand) {
                    ((DbCommand)propVal).Dispose();
                    prop.SetValue(this, null);
                }
            }
        }

        #endregion
    }
}
