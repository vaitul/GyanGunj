using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GyanGunj.LmsControls
{
    /// <summary>
    /// Interaction logic for LmsTextBox.xaml
    /// </summary>
    public partial class LmsTextBox : TextBox
    {
        public LmsTextBox()
        {
            InitializeComponent();
        }
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
        }
    }
}
