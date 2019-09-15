﻿using Databases.Domains;
using System;
using System.Collections.Generic;
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


        public static void ShowInformation(string Text, string Titile = "Gyan Gunj")
        {
            MessageBox.Show(Text, Titile, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
        }
        public static void ShowError(string Text, string Titile = "Gyan Gunj")
        {
            MessageBox.Show(Text, Titile, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
        }
        public static MessageBoxResult ShowQuestion(string Text, string Titile = "Gyan Gunj", MessageBoxButton Btns = MessageBoxButton.YesNo, MessageBoxResult DefaultBtn = MessageBoxResult.Yes)
        {
            return MessageBox.Show(Text, Titile, Btns, MessageBoxImage.Question, DefaultBtn);
        }
    }
}