using System;
using System.Globalization;
using System.Windows.Data;

namespace TimerApp.Converters
{
    public class FromSecondToDisplayFormatConverter : IValueConverter
    {
        public const string TimeFormat = @"hh\:mm\:ss";

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double second)
            {
                var timeSpan = TimeSpan.FromSeconds(second);
                return timeSpan.ToString(TimeFormat);
            }

            throw new ArgumentException($"Type of {nameof(value)} must be double");
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string formatedTimeString
                && TimeSpan.TryParse(formatedTimeString, out var result))
            {
                return result.TotalSeconds;
            }

            return 0;
        }
    }
}
