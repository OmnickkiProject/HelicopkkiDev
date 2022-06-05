using HelicopkkiDev.Library;
using HelicopkkiDev.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace HelicopkkiDev.View.Converter
{
    /// <summary>
    /// CharacterShadowDirection을 일관성 있게 조작할 수 있도록 변환
    /// </summary>
    class ShadowDirectionConverter : ConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (90.0 - (double)value).ToDeg180();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
