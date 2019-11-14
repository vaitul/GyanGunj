using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;
using Utilities;

namespace GyanGunj
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
        }
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.LogError(e.Exception.ToString());
            Utilities.Globals.ShowError(e.Exception.GetType().FullName + " : " + e.Exception.Message, "Exception Handled By Globaly");
            e.Handled = true;
        }
    }
}
