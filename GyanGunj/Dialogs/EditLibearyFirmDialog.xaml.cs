using GyanGunj.Enums;
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
using System.Windows.Shapes;

namespace GyanGunj.Dialogs
{
    /// <summary>
    /// Interaction logic for EditLibearyFirmDialog.xaml
    /// </summary>
    /// 



    public partial class EditLibearyFirmDialog : Window
    {
        public ViewModels.EditLibraryFirm ViewModel { get; set; }
        public DialogModes DialogMode { get; set; }
        public EditLibearyFirmDialog(DialogModes mode = DialogModes.New)
        {
            var viewmodel = new ViewModels.EditLibraryFirm(mode);

            this.DialogMode = mode;
            this.DataContext = this.ViewModel = viewmodel;

            InitializeComponent();
        }
    }
}
