using System.Windows.Controls;
using TimerApp.Base.Visual;

namespace TimerApp.DisplayTimer
{
    public partial class ChangeTimerView : UserControl
    {
        public ChangeTimerView()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is IFocusGot focusGot)
                focusGot.OnFocusGot(sender);
        }

        private void TextBox_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is IFocusLost focusLost)
                focusLost.OnFocusLost(sender);
        }
    }
}
