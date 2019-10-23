using GyanGunj.Common;
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

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            LibName.Focus();
            AttributeGrid.KeyboardCommandProvider = new GridViewGGCommandProvider(this.AttributeGrid);
        }

        //private void AttributeGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    var grid = sender as DataGrid;
        //    var u = e.OriginalSource as UIElement;
        //    if (e.Key == Key.Enter && u != null)
        //    {
        //        var isNew = false;
        //        e.Handled = true;
        //        if (grid.CurrentColumn.DisplayIndex == grid.Columns.Count - 1)
        //        {
        //            u.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
        //            isNew = true;
        //        }
        //        else
        //            u.MoveFocus(new TraversalRequest(FocusNavigationDirection.Right));
        //        if(isNew)
        //        {
        //            u.MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
        //        }
        //    }
            
        //}
    }
}
