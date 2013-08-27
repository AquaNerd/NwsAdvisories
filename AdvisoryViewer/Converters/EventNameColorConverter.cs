using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using NWS.Util;

namespace AdvisoryViewer.Converters {
    [ValueConversion(typeof(string), typeof(Brush))]
    public class EventNameColorConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Dictionary<string, string> colorList = AlertColors.GetColors("WeatherAlertColors.csv");
            string color = "#" + colorList[(string)value].ToString();
            return (SolidColorBrush)(new BrushConverter().ConvertFrom(color));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
