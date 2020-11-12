using System;
using System.Windows.Data;

namespace VideoDisplayApp.WPF.Converters
{
    public class RawPathToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string path = value.ToString();
            if (path.StartsWith("//"))
            {
                path = "http:" + path;
            }

            return path;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
