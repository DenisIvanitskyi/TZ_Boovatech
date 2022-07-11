using System;
using System.Windows;
using System.Windows.Controls;

namespace Common.Controls
{
    public partial class TextBoxExtended : TextBox
    {
        public static readonly DependencyProperty IsSelectedProperty
            = DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(TextBoxExtended));

        public TextBoxExtended()
        {
            InitializeComponent();

            GotFocus += OnTextBoxGotFocus;
            LostFocus += OnTextBoxLostFocus;
        }

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        ~TextBoxExtended()
        {
            GotFocus -= OnTextBoxGotFocus;
            LostFocus -= OnTextBoxLostFocus;
        }

        private void OnTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            IsSelected = false;
        }

        private void OnTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            IsSelected = true;
        }
    }
}
