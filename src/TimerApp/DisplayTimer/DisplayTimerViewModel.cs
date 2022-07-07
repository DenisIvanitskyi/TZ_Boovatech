using Common.Mvvm;
using TimerApp.Services.Timer;

namespace TimerApp.DisplayTimer
{
    public class DisplayTimeViewModel : BaseViewModel<DisplayTimeView>, IDisplaySencondsViewModel
    {
        private ITimerService _timerService;
        private ITimerStrategyChanging _timerStrategyChanging;
        private double _displayTimer;

        public DisplayTimeViewModel(ITimerService timerService, ITimerStrategyChanging timerStrategyChanging)
        {
            _timerService = timerService;
            _timerStrategyChanging = timerStrategyChanging;
        }

        public double DisplayTime
        {
            get => _displayTimer;
            private set
            {
                _displayTimer = value;
                OnPropertyChanged(nameof(DisplayTime));
            }
        }

        public override void ActivateViewModel()
        {
            base.ActivateViewModel();
            if (_timerService != null)
            {
                _timerService.OnEverySecondChanged += TimerService_OnEverySecondChanged;
            }
        }



        public override void DeactivateViewModel()
        {
            base.DeactivateViewModel();
            if (_timerService != null)
            {
                _timerService.OnEverySecondChanged -= TimerService_OnEverySecondChanged;
                _timerService.Stop();
                _timerService.Dispose();
            }
        }

        protected override DisplayTimeView CreateView()
            => new DisplayTimeView() { DataContext = this };

        private void TimerService_OnEverySecondChanged(object sender, double e)
        {
            if (sender is ITimerService timerService)
            {
                var isShouldUpdateTime = _timerStrategyChanging?.IsShouldToUpdateTime(timerService) == true;
                if (isShouldUpdateTime)
                    DisplayTime = timerService.SecondToFinish;
            }
        }
    }
}
