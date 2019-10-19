using GyanGunj.Dialogs;
using GyanGunj.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GyanGunj.ViewModels
{
    public class EditLibraryFirm : BaseViewModel
    {
        #region Control Related Fields

        public string Title { get; set; }
        public Visibility CancelButtonVisibility { get; set; }

        #endregion

        #region Ctor

        public EditLibraryFirm(DialogModes dialogMode = DialogModes.New)
        {
            this.DialogMode = dialogMode;
        }

        #endregion

        #region Properties

        private DialogModes _DialogMode;
        public DialogModes DialogMode
        {
            get { return _DialogMode; }
            set
            {
                _DialogMode = value;
                if (_DialogMode == DialogModes.New)
                {
                    this.Title = "Fill Library Firm Data";
                    this.CancelButtonVisibility = Visibility.Collapsed;
                }
                else if (_DialogMode == DialogModes.Edit)
                {
                    this.Title = "Editing Library Firm Data";
                    this.CancelButtonVisibility = Visibility.Visible;
                }
                else
                {
                    this.Title = "Library Firm Data";
                    this.CancelButtonVisibility = Visibility.Visible;
                }
            }
        }


        #endregion
    }
}
