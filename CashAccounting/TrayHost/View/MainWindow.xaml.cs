using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrayHost.ViewModel;


namespace TrayHost.View {
    /// <summary>
    ///     MainWindow für den Tray Service Host. Wird in ein Notify Icon Minimized.
    /// </summary>
    public partial class MainWindow : Window {

        private MainViewModel MyViewModel = null;

        private System.Windows.Forms.NotifyIcon notifyIcon;

        public MainWindow() {

            InitializeComponent();
            this.Hide();                 // Dieses Windows startet Minimized und Unsichtbar (nur im Tray sichtbar)

            this.LevelCombo.ItemsSource = Enum.GetValues(typeof(ILogging.Level)).Cast<ILogging.Level>();

            MyViewModel = (MainViewModel)DataContext;
            MyViewModel.AddChildVm(this.LiveLogView.DataContext);

            this.Title = "Wpf Trayhost - CashAccounting";
            string startMessage = "";
            try {
                MyViewModel.StartService();
                startMessage = "CashAccounting started.";
            } catch(Exception ex) {
                startMessage = $"Error starting CashAccounting: '{ex.Message}'.";
            }

            CreateNotifyIcon(startMessage);
        }

        protected override void OnStateChanged(EventArgs e) {
            if(WindowState == WindowState.Minimized) {
                this.Hide();            // Immer wenn wir minimized werden unsichtbar schalten (kein Icon in der Task Leiste sichtbar).
            }
            base.OnStateChanged(e);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            MessageBoxResult choose = MessageBox.Show("Service Host wirklich Beenden ? " + Environment.NewLine
                                                    + "(Bei 'Nein' wird es nur in den Tray minimized)",
                                                      "Beenden/Minimize", MessageBoxButton.YesNoCancel);
            if(choose == MessageBoxResult.No) {
                e.Cancel = true;
                this.WindowState = WindowState.Minimized;
            } else if(choose == MessageBoxResult.Cancel) {
                e.Cancel = true;
            } else {
                // Wir schließen wirklich -> Tray Icon ausblenden.
                notifyIcon.Visible = false;
            }
        }

        private void Window_Closed(object sender, EventArgs e) {
            MyViewModel.StopService();
        }


        #region NotifyIcon
        // Bitte für System.Windows.Forms kein globales using einfügen!
        private void CreateNotifyIcon(string bubbleText) {
            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Icon = TrayHost.Properties.Resources.TrayIcon;

            System.Windows.Forms.ContextMenuStrip menu = new System.Windows.Forms.ContextMenuStrip();
            System.Windows.Forms.ToolStripMenuItem item;
            System.Windows.Forms.ToolStripSeparator sep;

            //About.
            item = new System.Windows.Forms.ToolStripMenuItem();
            item.Text = "About";
            item.Click += new EventHandler(item_About);
            //item.Image = Resources.AboutImage;
            menu.Items.Add(item);

            // Separator.
            sep = new System.Windows.Forms.ToolStripSeparator();
            menu.Items.Add(sep);

            // Exit.
            item = new System.Windows.Forms.ToolStripMenuItem();
            item.Text = "Exit";
            item.Click += new EventHandler(item_Exit);
            //item.Image = Resources.ExitImage;
            menu.Items.Add(item);

            notifyIcon.ContextMenuStrip = menu;

            notifyIcon.DoubleClick +=
                delegate (object sender, EventArgs args) {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                    this.BringIntoView();
                };

            notifyIcon.Visible = true;

            notifyIcon.BalloonTipTitle = "WCF Service gestartet";
            notifyIcon.BalloonTipText = bubbleText;

            notifyIcon.ShowBalloonTip(1500);


        }

        void notifyIcon_BalloonTipClicked(object sender, EventArgs e) {
            this.Close();
        }

        void notifyIcon_BalloonTipClosed(object sender, EventArgs e) {
            this.Close();
        }

        void item_About(object sender, EventArgs e) {
            AboutWindow about = new AboutWindow();
            about.ShowDialog();
        }

        void item_Exit(object sender, EventArgs e) {
            this.Close();
        }

        # endregion

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            AboutWindow about = new AboutWindow();
            about.ShowDialog();
        }



    }
}
