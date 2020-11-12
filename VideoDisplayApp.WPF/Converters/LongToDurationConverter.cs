using System;
using System.Windows.Data;

namespace VideoDisplayApp.WPF.Converters
{
    public class LongToDurationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            long.TryParse(value.ToString(), out long original);
            TimeSpan t = TimeSpan.FromSeconds(original);
            string answer = string.Format("{0:D2}:{1:D2}:{2:D2}",
                 t.Hours,
                 t.Minutes,
                 t.Seconds,
                 t.Milliseconds);

            return answer;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
