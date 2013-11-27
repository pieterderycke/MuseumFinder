using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MuseumFinder.Util
{
    public static class TextBoxBindingExtras
    {
        public static bool GetUpdateSourceOnEnter(DependencyObject d)
        {
            return (bool)d.GetValue(UpdateSourceOnEnterProperty);
        }

        public static void SetUpdateSourceOnEnter(DependencyObject d, bool value)
        {
            d.SetValue(UpdateSourceOnEnterProperty, value);
        }

        public static readonly DependencyProperty UpdateSourceOnEnterProperty =
            DependencyProperty.RegisterAttached(
                "UpdateSourceOnEnter",
                typeof(bool),
                typeof(TextBoxBindingExtras),
                new PropertyMetadata(false, OnEnter));

        private static void OnEnter(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = d as TextBox;
            if (textBox == null)
                return;
            else
            {
                if ((bool)e.NewValue)
                {
                    textBox.KeyUp += OnTextChanged;
                }
                else
                {
                    textBox.KeyUp -= OnTextChanged;                    
                }
            }
        }

        private static void OnTextChanged(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                TextBox textBox = (TextBox)sender;

                var bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);
                if (bindingExpression != null)
                {
                    bindingExpression.UpdateSource();
                    GetCurrentPage().Focus(); // hide soft keyboard
                }
            }
        }

        private static PhoneApplicationPage GetCurrentPage()
        {
            return (PhoneApplicationPage)((PhoneApplicationFrame)Application.Current.RootVisual).Content;
        }
    }
}
