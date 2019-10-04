using Databases;
using System;
using System.Collections.Generic;
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
        }

        private async void StartStartupTask(object sender, RoutedEventArgs e)
        {
            Statusbar.Text = "Loading Master Data...";

            await Task.Run(() =>
            {
                if (!Globals.MasterDatabase.IsExist)
                {
                    Globals.MasterDatabase.Create();
                }
            });
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
