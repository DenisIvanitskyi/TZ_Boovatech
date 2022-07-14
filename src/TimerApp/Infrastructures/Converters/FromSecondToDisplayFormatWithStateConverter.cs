using System;
using System.Globalization;
using System.Windows.Data;
using TimerApp.Infrastructures.Enums;
using TimerApp.Infrastructures.Models;

namespace TimerApp.Infrastructures.Converters
{
    public class FromSecondToDisplayFormatWithStateConverter : IValueConverter
    {
        private readonly FromSecondToDisplayFormatConverter _fromSecondToDisplayFormatConverter;
        private TimerPresenterModel _timerPresenterModel;

        public FromSecondToDisplayFormatWithStateConverter()
        {
            _fromSecondToDisplayFormatConverter = new FromSecondToDisplayFormatConverter();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimerPresenterModel timerPresenterModel)
            {
                _timerPresenterModel = timerPresenterModel;
                if (timerPresenterModel.TimeFormatState == TimeFormatState.FullTime)
                    return _fromSecondToDisplayFormatConverter.Convert(timerPresenterModel.DisplayTime, targetType, parameter, culture);
                return timerPresenterModel.DisplayTime;
            }

            throw new ArgumentNullException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value.ToString(), out var seconds))
                _timerPresenterModel.DisplayTime = seconds;
            return _timerPresenterModel;
        }
    }
}
