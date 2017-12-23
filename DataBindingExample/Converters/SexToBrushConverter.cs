using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace DataBindingExample.Converters
{
    class SexToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter?.ToString() == "darker")
                return (bool)value ?
                    Brushes.DarkBlue :
                    Brushes.Crimson;

            return (bool)value ?
                Brushes.CornflowerBlue :
                Brushes.Pink;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
