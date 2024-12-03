using System;
using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using OpenCvSharp;
using Windows.UI;
namespace Photo.Converters
{
    public class ScalarToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Scalar scalar)
            {
                // Tạo màu từ các giá trị ARGB
                var color = Color.FromArgb(255, (byte)scalar.Val2, (byte)scalar.Val1, (byte)scalar.Val0);
                return new SolidColorBrush(color);
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}


