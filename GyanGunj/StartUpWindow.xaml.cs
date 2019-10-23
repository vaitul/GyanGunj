using Databases;
using GyanGunj.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Utilities;
using Utilities.Interfaces;
using Utilities.Services;

namespace GyanGunj
{
    public partial class StartUpWindow : Window
    {
        public StartUpWindow()
        {
            InitializeComponent();
            LogInWindow.Loaded += StartStartupTask;
            this.ShowInTaskbar = false;
        }
        private async void StartStartupTask(object sender, RoutedEventArgs e)
        {
            Statusbar.Text = "Loading Master Data...";

            bool IsMasterDBExist = Globals.MasterDatabase.IsExist;
            await Task.Run(() =>
            {
                if (!IsMasterDBExist)
                {
                    Globals.MasterDatabase.Create();
                }
            });

            if (!IsMasterDBExist || (IsMasterDBExist && Globals.MasterDatabase.Library == null) || true)
            {
                var FirmDialog = new EditLibearyFirmDialog();
                FirmDialog.WindowStyle = WindowStyle.None;
                FirmDialog.ShowInTaskbar = false;
                FirmDialog.ShowDialog();
            }


            Statusbar.Text = "Ready.";
        }

        private void SomeKey_Pressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                //if (Utilities.Globals.ShowQuestion("Are you sure to Exit ?", "Gyan Gunj") == MessageBoxResult.Yes)
                Application.Current.Shutdown();
            }
        }
    }
}
