using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace TForTea.WinRT.Converters
{
    public class InvertedBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return GetVisibility(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private object GetVisibility(object value)
        {
            if (!(value is bool))
                return Visibility.Visible;
            bool objValue = (bool)value;
            if (objValue)
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }
    }
}