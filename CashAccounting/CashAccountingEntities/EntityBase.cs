using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CashAccountingEntities {
    public abstract class EntityBase : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;
        


        protected void ChangeProperty<T>(T newValue, [CallerMemberName]string propName = "") {
            PropertyInfo prpInf = this.GetType().GetProperty(propName);
            if(prpInf == null) {
                throw new ArgumentException($"No property found with name {propName}.", "propName");
            }

            T oldValue = (T)prpInf.GetValue(this);
            prpInf.SetValue(newValue, this);
            if(!oldValue.Equals(newValue)) {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            }

        }

    }
}
