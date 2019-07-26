using System;
using System.Collections.Generic;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GyanGunj
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            LogInWindow.Loaded += StartStartupTask;
        }

        private async void StartStartupTask(object sender, RoutedEventArgs e)
        {
            Statusbar.Text = "Starting...";
            await Task.Run(() =>
            {
                Statusbar.Dispatcher.Invoke(() =>
                {
                    Statusbar.Text = "Loading Database...";
                });
                Thread.Sleep(5000);
            });
            Statusbar.Text = "Ready.";
            //MainWindow window = new MainWindow();
            //window.Show();
            //this.Close();
        }

        private void SomeKey_Pressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if(Utilities.Globals.ShowQuestion("Are you sure to Exit ?","Gyan Gunj") == MessageBoxResult.Yes)
                    Application.Current.Shutdown();
            }
        }
    }
}
