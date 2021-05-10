﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace lab6_7.Converters
{
    class ImageToResolvedImage : IValueConverter
    {
        const string errorImagePath = "Images/image_load_failed.png";
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //string path = value.ToString();
            //bool exsists = File.Exists(path);
            //return new BitmapImage(new Uri(exsists ? path : errorImagePath, exsists ? UriKind.Absolute : UriKind.Relative));
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(@"/lab6-7;component/Images/image_load_failed.png", UriKind.Relative);
            img.EndInit();
            return img;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}