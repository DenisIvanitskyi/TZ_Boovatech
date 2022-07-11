using Common.Mvvm;
using System;
using TimerApp.Enums;
using TimerApp.Models;
using TimerApp.Services.Timer;

namespace TimerApp.DisplayTimer
{
    public class ChangeTimerViewModel : BaseViewModel<ChangeTimerView>, IDisplaySencondsViewModel
    {
        private readonly ITimerService _timerService;
        private TimerPresenterModel _timerPresenterModel;
        private bool _isInputHasFocus;

        public ChangeTimerViewModel(ITimerService timerService)
        {
            _timerService = timerService;
            TimerPresenterModel = new TimerPresenterModel() { TimeFormatState = TimeFormatState.FullTime };
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

                if (IsInputHasFocus)
                    OnFocusGot();
                else
                    OnFocusLost();
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

        public override void ActivateViewModel()
        {
            base.ActivateViewModel();
            if(_timerService != null)
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
            }
        }

        protected override ChangeTimerView CreateView()
            => new ChangeTimerView() { DataContext = this };

        private void TimerService_OnEverySecondChanged(object sender, double e)
        {
            if(sender is ITimerService timerService)
            {
                DisplayTime = timerService.SecondToFinish;
                OnPropertyChanged(nameof(TimerPresenterModel));
            }
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

            _timerService.Start(timeSpan);
        }
    }
}
