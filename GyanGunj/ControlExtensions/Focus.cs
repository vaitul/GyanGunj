using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace GyanGunj.ControlExtensions
{
    public class Focus
    {
        public static string GetNext(DependencyObject obj)
        {
            return (string)obj.GetValue(NextProperty);
        }

        public static void SetNext(DependencyObject obj, string value)
        {
            obj.SetValue(NextProperty, value);
        }

        // Using a DependencyProperty as the backing store for Next.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NextProperty =
            DependencyProperty.RegisterAttached("Next", typeof(string), typeof(Focus), new PropertyMetadata(new PropertyChangedCallback(NextCallBack)));



        public static string GetPrevious(DependencyObject obj)
        {
            return (string)obj.GetValue(PreviousProperty);
        }

        public static void SetPrevious(DependencyObject obj, string value)
        {
            obj.SetValue(PreviousProperty, value);
        }

        // Using a DependencyProperty as the backing store for Previous.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviousProperty =
            DependencyProperty.RegisterAttached("Previous", typeof(string), typeof(Focus), new PropertyMetadata(new PropertyChangedCallback(PreviousCallBack)));

        private static void NextCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FrameworkElement;

            if (e.OldValue is string)
                control.PreviewKeyDown -= FocusNext;
            if (e.NewValue is string)
                control.PreviewKeyDown += FocusNext;
        }
        private static void PreviousCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FrameworkElement;

            if (e.OldValue is string)
                control.PreviewKeyDown -= FocusPrevious;
            if (e.NewValue is string)
                control.PreviewKeyDown += FocusPrevious;
        }

        private static void FocusNext(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter && e.Key != Key.Return)
                return;

            bool doFocus = true;

            if (!(sender is FrameworkElement element))
                return;
            //for textboc has except return
            if (element is TextBox txtbox)
            {
                txtbox.Text = txtbox.Text.Trim(new char[]{ ' ','\r'});
                txtbox.CaretIndex = txtbox.Text.Length;
                if (txtbox.AcceptsReturn && txtbox.Text.Length > 0)
                    if (txtbox.Text[txtbox.Text.Length - 1] != '\n')
                        doFocus = false;
                    else
                    {
                        txtbox.Text = txtbox.Text.Substring(0, txtbox.Text.Length - 1);
                        txtbox.CaretIndex = txtbox.Text.Length-1;
                    }
            }

            var FocusTo = GetNext(element);
            if (string.IsNullOrWhiteSpace(FocusTo))
                return;
            if (VisualTreeHelper.GetParent(element) is FrameworkElement parent && parent.FindName(FocusTo) is FrameworkElement focusToElement)
            {
                if (focusToElement is TextBox Totxtbox)
                {
                    if (Totxtbox.AcceptsReturn)
                    {
                        e.Handled = true;
                    }
                }
                else if (focusToElement is RadGridView grid && grid.CanUserInsertRows)
                {
                    GyanGunj.Common.GridUtils.BeginRowEditing(grid, grid.Items.Count - 1);
                }

                if (doFocus)
                    focusToElement.Focus();
            }
        }

        private static void FocusPrevious(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (e.Key != Key.Tab || e.KeyboardDevice.Modifiers != ModifierKeys.Shift)
                return;
            if (!(sender is FrameworkElement element))
                return;
            var FocusTo = GetPrevious(element);
            if (string.IsNullOrWhiteSpace(FocusTo))
                return;
            if (VisualTreeHelper.GetParent(element) is FrameworkElement parent && parent.FindName(FocusTo) is FrameworkElement focusToElement)
                focusToElement.Focus();
        }
    }
}
