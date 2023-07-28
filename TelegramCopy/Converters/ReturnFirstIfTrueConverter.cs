using System.Globalization;

namespace TelegramCopy.Converters
{
	public class ReturnFirstIfTrueConverter : IMultiValueConverter
    {
        #region -- IMultiValueConverter implementation --

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            object result = null;

            if (values is not null && values.Length > 2 && values[0] is bool isTrue)
            {
                if (isTrue)
                {
                    result = values[1];
                }
                else
                {
                    result = values[2];
                }
            }

            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is not bool b || targetTypes.Any(t => !t.IsAssignableFrom(typeof(bool))))
            {
                return null;
            }

            if (b)
            {
                return targetTypes.Select(t => (object)true).ToArray();
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}

