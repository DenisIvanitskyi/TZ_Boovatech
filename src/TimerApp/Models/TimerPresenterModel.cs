﻿
using Common.Mvvm;
using TimerApp.Enums;

namespace TimerApp.Models
{
    public class TimerPresenterModel : PropertyChangedBase
    {
        private double _displayTime;
        private TimeFormatState _timeFormatState;

        public double DisplayTime
        {
            get => _displayTime;
            set
            {
                _displayTime = value;
                OnPropertyChanged(nameof(DisplayTime));
            }
        }

        public TimeFormatState TimeFormatState
        {
            get => _timeFormatState;
            set
            {
                _timeFormatState = value;
                OnPropertyChanged(nameof(TimeFormatState));
            }
        }
    }
}
