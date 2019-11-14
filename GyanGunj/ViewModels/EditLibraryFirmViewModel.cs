using GyanGunj.Common;
using GyanGunj.Dialogs;
using GyanGunj.Enums;
using GyanGunj.Extensions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Utilities;

namespace GyanGunj.ViewModels
{
    public enum Windows
    {
        DataWindow = 1,
        LogoWindow,
        AdminWindow
    }
    public class EditLibraryFirm : BaseViewModel
    {
        #region Control Related Fields
        public string Title { get; set; }
        public string SaveButtonContent { get; set; }
        public Visibility DataWindowVisibility { get; set; }
        public Visibility LogoWindowVisibility { get; set; }
        public Visibility AdminWindowVisibility { get; set; }
        public ImageSource LogoImageSource { get; set; }
        #endregion

        #region Ctor

        public EditLibraryFirm(DialogModes dialogMode = DialogModes.New)
        {
            this.Attributes = new ObservableCollection<Databases.Domains.MasterAttribute>(Globals.MasterDatabase.Attributes);
            this.Attributes.Add(new Databases.Domains.MasterAttribute());
            this.Attributes.CollectionChanged += Attributes_CollectionChanged;

            this._SaveCommand = new DelegateCommand(Save);
            this._PreviousCommand = new DelegateCommand(Previous,CanPreviousExecue);
            this._CancelCommand = new DelegateCommand(Cancel, CanCancelExecue);
            this._AttrRowEditingEndedCommand = new DelegateCommand(AttrRowEditEnded);
            this._AddLogoCommand = new DelegateCommand(AddLogo);

            this.DialogMode = dialogMode;
            this.ActiveWindow = Windows.DataWindow;

            //LogoImageSource = new ImageSource("/Images/Logo_Only_Large.png");

            //exteranl things
            this.Countries = new List<string>() { "INDIA" };
            this.States = new List<string>() { "GUJARAT" };
        }


        #endregion

        #region ICommands

        private DelegateCommand _SaveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _SaveCommand;
            }
        }

        private DelegateCommand _CancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _CancelCommand;
            }
        }
        private DelegateCommand _PreviousCommand;
        public ICommand PreviousCommand
        {
            get
            {
                return _PreviousCommand;
            }
        }

        private DelegateCommand _AttrRowEditingEndedCommand;
        public ICommand AttrRowEditingEndedCommand
        {
            get
            {
                return _AttrRowEditingEndedCommand;
            }
        }

        private DelegateCommand _AddLogoCommand;
        public ICommand AddLogoCommand
        {
            get
            {
                return _AddLogoCommand;
            }
        }

        #endregion

        #region Properties

        private Byte[] _ImageLogo;
        public Byte[] ImageLogo
        {
            get { return _ImageLogo; }
            set { _ImageLogo = value; }
        }


        public DialogModes DialogMode { get; set; }

        private Windows _ActiveWindow;
        public Windows ActiveWindow
        {
            get { return _ActiveWindow; }
            set
            {
                _ActiveWindow = value;
                if (value == Windows.DataWindow)
                {
                    DataWindowVisibility = Visibility.Visible;
                    LogoWindowVisibility = Visibility.Collapsed;
                    AdminWindowVisibility = Visibility.Collapsed;
                    if (DialogMode == DialogModes.New)
                    {
                        this.Title = "Fill Library Firm Data";
                    }
                    else
                    {
                        this.Title = "Edit Library Firm Data";
                    }
                    SaveButtonContent = "NEXT";
                }
                else if (value == Windows.LogoWindow)
                {
                    DataWindowVisibility = Visibility.Collapsed;
                    LogoWindowVisibility = Visibility.Visible;
                    AdminWindowVisibility = Visibility.Collapsed;
                    if (DialogMode == DialogModes.New)
                    {
                        this.Title = "Add Library Logo";
                        SaveButtonContent = "Next";
                    }
                    else
                    {
                        this.Title = "Change Library Logo";
                        SaveButtonContent = "SAVE";
                    }
                }
                else if (value == Windows.AdminWindow)
                {
                    DataWindowVisibility = Visibility.Collapsed;
                    LogoWindowVisibility = Visibility.Collapsed;
                    AdminWindowVisibility = Visibility.Visible;

                    this.Title = "Add Library Admin";
                    SaveButtonContent = "SAVE";
                }
                OnPropertyChanged("Title");
                OnPropertyChanged("DataWindowVisibility");
                OnPropertyChanged("LogoWindowVisibility");
                OnPropertyChanged("AdminWindowVisibility");
                OnPropertyChanged("SaveButtonContent");
            }
        }


        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { SetValue(ref _Name, value); }
        }

        private string _Address;
        public string Address
        {
            get { return _Address; }
            set { SetValue(ref _Address, value); }
        }

        private string _PinCode;
        public string PinCode
        {
            get { return _PinCode; }
            set { SetValue(ref _PinCode, value); }
        }

        private string _City;
        public string City
        {
            get { return _City; }
            set { SetValue(ref _City, value); }
        }

        private List<string> _Countries;
        public List<string> Countries
        {
            get { return _Countries; }
            set { SetValue(ref _Countries, value); }
        }

        private string _Country;
        public string Country
        {
            get { return _Country; }
            set { SetValue(ref _Country, value); }
        }

        private List<string> _States;
        public List<string> States
        {
            get { return _States; }
            set { SetValue(ref _States, value); }
        }

        private string _State;
        public string State
        {
            get { return _State; }
            set { SetValue(ref _State, value); }
        }

        private string _Phone;
        public string Phone
        {
            get { return _Phone; }
            set { SetValue(ref _Phone, value); }
        }

        private string _Mobile;
        public string Mobile
        {
            get { return _Mobile; }
            set { SetValue(ref _Mobile, value); }
        }

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { SetValue(ref _Email, value); }
        }

        private string _WebSite;
        public string WebSite
        {
            get { return _WebSite; }
            set { SetValue(ref _WebSite, value); }
        }

        private ObservableCollection<Databases.Domains.MasterAttribute> _Attributes;
        public ObservableCollection<Databases.Domains.MasterAttribute> Attributes
        {
            get { return _Attributes; }
            set { SetValue(ref _Attributes, value); }
        }


        #endregion

        #region Methods

        private void Attributes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                if (e.OldItems[0] is Databases.Domains.MasterAttribute attr)
                {
                    Globals.MasterDatabase.DeleteAttr(attr.Id.GetValueOrDefault());
                }
            }
        }
        
        private bool CanPreviousExecue(object obj)
        {
            return ActiveWindow != Windows.DataWindow;
        }
        public void Previous(object param)
        {
            ActiveWindow -= 1;
        }

        public void Save(object parameter)
        {
            if (this.Validate())
            {
                if (SaveButtonContent != "SAVE")
                {
                    ActiveWindow += 1;
                    return;
                }

                var entity = this.ToEntity();
                Globals.MasterDatabase.Insert(entity);

                var attr = Attributes.Where(x => x.Id.GetValueOrDefault() == 0 && !string.IsNullOrEmpty(x.Name?.Trim()) && !string.IsNullOrEmpty(x.Value?.Trim()));
                if (attr?.Any() == true)
                {
                    Globals.MasterDatabase.Insert(attr);
                }

                var updatedAttrs = Attributes.Where(x => EditedRowsId.Contains(x.Id.GetValueOrDefault()));
                foreach (var item in updatedAttrs)
                {
                    Globals.MasterDatabase.Update(item);
                }
                if (parameter is EditLibearyFirmDialog control)
                    control.Close();
            }
        }
        private bool CanCancelExecue(object parameter)
        {
            return this.DialogMode == DialogModes.Edit;
        }
        private void Cancel(object parameter)
        {
            if (parameter is EditLibearyFirmDialog control)
                control.Close();
        }
        public bool Validate()
        {
            if (string.IsNullOrEmpty(this.Name?.Trim()) || string.IsNullOrEmpty(this.City?.Trim()))
            {
                Globals.ShowError("Library Name and City must required !");
                return false;
            }
            return true;
        }
        public void AddLogo(object param)
        {
            var Dialog = new OpenFileDialog()
            {
                Filter = "Images (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg",
                FilterIndex = 0
            };
            if (Dialog.ShowDialog() != true) { return; }
            
            ImageLogo = File.ReadAllBytes(Dialog.FileName);

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(ImageLogo);
            bitmapImage.EndInit();
            bitmapImage.Freeze();
            LogoImageSource = bitmapImage;
            OnPropertyChanged("LogoImageSource");
        }

        HashSet<int> EditedRowsId = new HashSet<int>();
        private void AttrRowEditEnded(object parameter)
        {
            var row = (parameter as Telerik.Windows.Controls.RadGridView).SelectedItem as Databases.Domains.MasterAttribute;

            if (row?.Id > 0)
                EditedRowsId.Add(row.Id.GetValueOrDefault());
            return;
        }

        #endregion
    }
}
