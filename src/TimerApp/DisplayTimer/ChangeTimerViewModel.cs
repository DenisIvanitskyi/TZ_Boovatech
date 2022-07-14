using Common.Mvvm;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Windows.Input;
using TimerApp.Infrastructures.Enums;
using TimerApp.Infrastructures.Models;
using TimerApp.Infrastructures.Services.Timer;

namespace TimerApp.DisplayTimer
{
    public class ChangeTimerViewModel : PropertyChangedBase
    {
        private ITimerService _timerService;
        private TimerPresenterModel _timerPresenterModel;
        private bool _isInputHasFocus;
        private bool _isTimerInited;

        public ChangeTimerViewModel()
        {
            TimerPresenterModel = new TimerPresenterModel() { TimeFormatState = TimeFormatState.FullTime };

            OnDeactivateViewModelCommand = new ActionCommand(OnDeactivateViewModelHandle);
        }

        public double DisplayTime
        {
            get => TimerPresenterModel.DisplayTime;
            set
            {
                TimerPresenterModel.DisplayTime = value;
                OnPropertyChanged(nameof(DisplayTime));
            }
        }

        public bool IsInputHasFocus
        {
            get => _isInputHasFocus;
            set
            {
                _isInputHasFocus = value;
                OnPropertyChanged(nameof(IsInputHasFocus));
                OnInputFocusChanged();
            }
        }

        public TimerPresenterModel TimerPresenterModel
        {
            get => _timerPresenterModel;
            set
            {
                _timerPresenterModel = value;
                OnPropertyChanged(nameof(TimerPresenterModel));
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
                    OnTimerServiceInit(value);
            }
        }

        public ICommand OnDeactivateViewModelCommand { get; }

        private void OnTimerServiceInit(ITimerService timerService)
        {
            if (timerService != null)
            {
                _isTimerInited = true;
                timerService.OnEverySecondChanged += TimerService_OnEverySecondChanged;
            }
        }

        private void OnDeactivateViewModelHandle()
        {
            if (_timerService != null)
            {
                _timerService.OnEverySecondChanged -= TimerService_OnEverySecondChanged;
            }
        }

        private void TimerService_OnEverySecondChanged(object sender, double e)
        {
            if (sender is ITimerService timerService)
            {
                DisplayTime = timerService.SecondToFinish;
                OnPropertyChanged(nameof(TimerPresenterModel));
            }
        }

        private void OnInputFocusChanged()
        {
            if (IsInputHasFocus)
                OnFocusGot();
            else
                OnFocusLost();
        }

        public void OnFocusGot()
        {
            _timerService?.Stop();
            TimerPresenterModel.TimeFormatState = TimeFormatState.Second;
        }

        public void OnFocusLost()
        {
            var timeSpan = TimeSpan.FromSeconds(DisplayTime);

            TimerPresenterModel.TimeFormatState = TimeFormatState.FullTime;
            TimerPresenterModel.DisplayTime = timeSpan.TotalSeconds;
            OnPropertyChanged(nameof(TimerPresenterModel));

            if (DisplayTime > 0)
                _timerService?.Start(timeSpan);
        }
    }
}
