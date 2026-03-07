using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ExpenseManager.WPF;

public class AmountColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is decimal amount)
        {
            return amount < 0
                ? new SolidColorBrush(Color.FromRgb(200, 50, 50))
                : new SolidColorBrush(Color.FromRgb(40, 150, 40));
        }
        return Brushes.Black;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
