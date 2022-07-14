using Common.Mvvm;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Windows.Input;
using TimerApp.Infrastructures.DisplayTimer;
using TimerApp.Infrastructures.Services.Timer;

namespace TimerApp.DisplayTimer
{
    public class DisplayTimeViewModel : PropertyChangedBase
    {
        private ITimerService _timerService;
        private ITimerStrategyChanging _timerStrategyChanging;
        private double _displayTimer;
        private bool _isTimerInited;

        public DisplayTimeViewModel()
        {
            OnDeactivateViewModelCommand = new ActionCommand(OnDeactivateViewModelHandle);
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

        public ITimerService TimerService
        {
            get => _timerService;
            set
            {
                _timerService = value;
                OnPropertyChanged(nameof(TimerService));
                if (!_isTimerInited)
                    InitializeTimerService(value);
            }
        }

        public ITimerStrategyChanging TimerStrategyChanging
        {
            get => _timerStrategyChanging;
            set
            {
                _timerStrategyChanging = value;
                OnPropertyChanged(nameof(TimerStrategyChanging));
            }
        }

        public ICommand OnDeactivateViewModelCommand { get; }

        private void InitializeTimerService(ITimerService value)
        {
            if (value != null)
            {
                _isTimerInited = true;
                value.OnEverySecondChanged += TimerService_OnEverySecondChanged;
                value.OnTimerStop += OnTimerStop;
            }
        }

        private void OnDeactivateViewModelHandle()
        {
            if (_timerService != null)
            {
                _timerService.OnEverySecondChanged -= TimerService_OnEverySecondChanged;
                _timerService.OnTimerStop -= OnTimerStop;
                _timerService.Stop();
                _timerService.Dispose();
            }
        }

        private void TimerService_OnEverySecondChanged(object sender, double e)
        {
            if (sender is ITimerService timerService)
            {
                var isShouldUpdateTime = TimerStrategyChanging?.IsShouldToUpdateTime(timerService) == true;
                if (isShouldUpdateTime)
                    DisplayTime = timerService.SecondToFinish;
            }
        }

        private void OnTimerStop(object sender, EventArgs e)
        {
            DisplayTime = 0;
        }
    }
}
