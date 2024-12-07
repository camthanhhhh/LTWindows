using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photo.Converters
{
    public class HersheyFontConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string fontName)
            {
                var result = fontName switch
                {
                    "Simplex" => "Segoe UI",
                    "Plain" => "Arial",
                    "Duplex" => "Times New Roman",
                    "Complex" => "Courier New",
                    "Triplex" => "Comic Sans MS",
                    "ComplexSmall" =>"Verdana",
                    "ScriptSimplex" => "Georgia",
                    "ScriptComplex" => "Tahoma",
                    _ => "Segoe UI",
                };

                return result;
            }

            return "Segoe UI";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
