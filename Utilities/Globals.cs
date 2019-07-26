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
        public static void ShowInformation(string Text, string Titile = "Gyan Gunj")
        {
            MessageBox.Show(Text, Titile, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
        }
        public static void ShowError(string Text, string Titile = "Gyan Gunj")
        {
            MessageBox.Show(Text, Titile, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
        }
        public static MessageBoxResult ShowQuestion(string Text, string Titile = "Gyan Gunj",MessageBoxButton Btns = MessageBoxButton.YesNo,MessageBoxResult DefaultBtn = MessageBoxResult.Yes)
        {
            return MessageBox.Show(Text, Titile, Btns, MessageBoxImage.Question, DefaultBtn);
        }
    }
}
