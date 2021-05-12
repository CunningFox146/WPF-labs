using lab6_7.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace lab6_7.Converters
{
    class SearchTypeToIndex : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (int)(SearchType)value;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (SearchType)(int)value;
    }
}
