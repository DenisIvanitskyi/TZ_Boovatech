using Common.Mvvm;
using System.Collections.ObjectModel;
using TimerApp.Base.TimerStrategyChanging;
using TimerApp.Services.Timer;

namespace TimerApp.Shell
{
    public class ShellViewModel : BaseViewModel<ShellView>
    {
        private ObservableCollection<IBaseViewModel> _items;

        public ShellViewModel()
        {

        }

        public ObservableCollection<IBaseViewModel> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public override void ActivateViewModel()
        {
            base.ActivateViewModel();

            var timerService = new TimerService();
            var evenTimerStrategy = new EvenTimerStrategy();
            var noEvenTimerStrategy = new NoEvenTimerStrategy();

            Items = new ObservableCollection<IBaseViewModel>()
            {
                new DisplayTimer.ChangeTimerViewModel(timerService),
                new DisplayTimer.DisplayTimeViewModel(timerService, noEvenTimerStrategy),
                new DisplayTimer.DisplayTimeViewModel(timerService, evenTimerStrategy),
            };

            foreach (var viewModel in Items)
                viewModel.ActivateViewModel();
        }

        public override void DeactivateViewModel()
        {
            base.DeactivateViewModel();

            foreach (var viewModel in Items)
                viewModel.DeactivateViewModel();
        }

        protected override ShellView CreateView()
            => new ShellView() { DataContext = this };
    }
}
