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

        /// <summary>
        /// Sets the value of the property throug the backing field, which has to exist with
        /// the naming convention {backingFieldPrefix} + {propName}.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="newValue">New value for the property.</param>
        /// <param name="backingFieldPrefix">Prefix for the backing field of the property. Default is '_'</param>
        /// <param name="propName">Name of property which value is about to be set.</param>
        protected void ChangeProperty<T>(T newValue, string backingFieldPrefix = "_", [CallerMemberName]string propName = "") {
            // Check if property is available at all.
            PropertyInfo prpInf = this.GetType().GetProperty(propName);
            if(prpInf == null) {
                throw new ArgumentException($"No property found with name {propName}.", "propName");
            }
            // Get the backing field. This is needed to set the value. Setting the value
            // via PropertyInfo.SetValue() would cause a stack overflow due to calling the setter
            // in a loop.
            FieldInfo prpField = this.GetType().GetField($"{backingFieldPrefix}{propName}");
            if(prpField == null) {
                throw new ArgumentException($"No field with name '{backingFieldPrefix}{propName}' found for propety '{propName}'.");
            }

            // Get the old value for later comparison.
            T oldValue = (T)prpField.GetValue(this);
            // set the value of the backing field, hence the value of the property.
            prpField.SetValue(this, newValue);
            // Raise PropertyChanged if necessary.
            if(!oldValue.Equals(newValue)) {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            }

        }


    }
}
