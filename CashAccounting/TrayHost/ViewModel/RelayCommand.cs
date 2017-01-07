using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TrayHost.ViewModel {
    public class RelayCommand : ICommand {

        readonly Func<Boolean> _canExecute;
        readonly Action<Object> _executePar;
        readonly Action _execute;

        #region Konstruktoren

        public RelayCommand(Action<Object> execute) : this(execute, null) { }
        public RelayCommand(Action execute) : this(execute, null) { }

        public RelayCommand(Action execute, Func<Boolean> canExecute) {
            if(execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action<Object> execute, Func<Boolean> canExecute) {
            if(execute == null)
                throw new ArgumentNullException("execute");
            _executePar = execute;
            _canExecute = canExecute;
        }
        #endregion

        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged(EventArgs e) {
            EventHandler handler = CanExecuteChanged;
            if(handler != null) {
                handler(this, e);
            }
        }

        public Boolean CanExecute(Object parameter) {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(Object parameter) {
            if(_execute != null) { _execute(); RaiseCanExecuteChanged(null); }
            if(_executePar != null) { _executePar(parameter); RaiseCanExecuteChanged(null); }
        }

    }
}
