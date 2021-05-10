using lab6_7.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace lab6_7.Converters
{
    class ItemCategoryToIndex : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (int)(ItemCategory)value;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (ItemCategory)(int)value;
    }
}
