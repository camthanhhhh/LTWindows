using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
namespace Photo.Converters
{
    public class FontFamilyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string fontFamilyName)
            {
                return new FontFamily(fontFamilyName);
            }
            return new FontFamily("Segoe UI"); // Phông chữ mặc định nếu không có giá trị
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // Không cần thiết nếu bạn không muốn chuyển ngược giá trị
            throw new NotImplementedException();
        }
    }
}


