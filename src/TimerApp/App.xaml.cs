using System.Windows;
using TimerApp.Shell;

namespace TimerApp
{
    public partial class App : Application
    {
        private ShellViewModel _shellViewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _shellViewModel = new ShellViewModel();
            _shellViewModel.ActivateViewModel();

            MainWindow = _shellViewModel.View;
            MainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            if (_shellViewModel != null)
                _shellViewModel.DeactivateViewModel();
        }
    }
}
