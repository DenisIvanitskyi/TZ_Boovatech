using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace TimerApp.Infrastructures.Behaviors
{
    public class TextBoxSelectionBehavior :  Behavior<TextBox>
    {
        public static readonly DependencyProperty IsSelectedProperty
            = DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(TextBoxSelectionBehavior));

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.GotFocus += AssociatedObject_GotFocus;
            AssociatedObject.LostFocus += AssociatedObject_LostFocus;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.GotFocus -= AssociatedObject_GotFocus;
            AssociatedObject.LostFocus -= AssociatedObject_LostFocus;
        }

        private void AssociatedObject_LostFocus(object sender, RoutedEventArgs e)
        {
            IsSelected = false;
        }

        private void AssociatedObject_GotFocus(object sender, RoutedEventArgs e)
        {
            IsSelected = true;
        }
    }
}
