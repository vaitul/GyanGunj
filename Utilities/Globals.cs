using Databases;
using Databases.Domains;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Utilities
{
    public static partial class Globals
    {
        private static User _CurrentUser;
        public static User CurrentUser
        {
            get
            {
                if (_CurrentUser == null)
                    _CurrentUser = new User() { Id = 0, Name = "Anonymous" };
                return _CurrentUser;
            }
            set
            {
                if (_CurrentUser.Id == 0)
                    _CurrentUser = value;
            }
        }
        public static string FirmLogoSource
        {
            get 
            {
                string path = AppDomain.CurrentDomain.BaseDirectory+"Images\\Logo_only_Large.png";
                var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Images");
                if (dir.GetFiles("FirmLogo.*").Length > 0)
                    path = dir.GetFiles("FirmLogo.*").First().FullName;
                return path; 
            }
        }


        private static MasterDatabase _MasterDatabase;
        public static MasterDatabase MasterDatabase
        {
            get
            {
                if (_MasterDatabase == null)
                    _MasterDatabase = new MasterDatabase();
                return _MasterDatabase;
            }
        }


        public static void ShowInformation(string Text, string Titile = "GyanGunj Library Management System")
        {
            MessageBox.Show(Text, Titile, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
        }
        public static void ShowError(string Text, string Titile = "GyanGunj Library Management System")
        {
            MessageBox.Show(Text, Titile, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
        }
        public static MessageBoxResult ShowQuestion(string Text, string Titile = "GyanGunj Library Management System", MessageBoxButton Btns = MessageBoxButton.YesNo, MessageBoxResult DefaultBtn = MessageBoxResult.Yes)
        {
            return MessageBox.Show(Text, Titile, Btns, MessageBoxImage.Question, DefaultBtn);
        }
    }
}
