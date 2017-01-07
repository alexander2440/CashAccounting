using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrayHost.View {
    /// <summary>
    /// Interaktionslogik für LiveLogView.xaml
    /// </summary>
    public partial class LiveLogView : UserControl {
        public LiveLogView() {
            InitializeComponent();
            ((INotifyCollectionChanged)(this.LogList.Items)).CollectionChanged += LiveLogView_CollectionChanged;
        }

        void LiveLogView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action == NotifyCollectionChangedAction.Add) {
                this.LogList.ScrollIntoView(e.NewItems[0]);
            }
        }

        public void CopyLogExecuted(object sender, ExecutedRoutedEventArgs e) {
            ListBox lb = sender as ListBox;
            if (lb != null && lb.SelectedItems.Count>0) {
                string txt = String.Empty;
                foreach(LogRecord l in lb.SelectedItems) {
                    txt += l.ToString() + Environment.NewLine;
                }
                Clipboard.Clear();
                Clipboard.SetText(txt);
            }

            // Copy log item to the clipboard
        }

        public void CanExecuteCopyLog(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }
    }
}
