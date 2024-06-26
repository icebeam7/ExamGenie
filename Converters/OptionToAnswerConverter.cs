using System.Globalization;

namespace AISchoolManagementApp.Converters
{
    public class OptionToAnswerConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == null || values[1] == null)
                return Colors.Black;

            var option = values[0].ToString();
            var reveal = (bool)values[1];
            var answer = parameter.ToString();

            return !(reveal && answer == option)
                ? Colors.Black : Colors.Green;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
