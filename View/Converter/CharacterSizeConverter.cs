using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace HelicopkkiDev.View.Converter
{
    /// <summary>
    /// CharacterSize와, CharacterMaxSize에 대한 CharacterSize의 percentage 간 상호 변환
    /// </summary>
    class CharacterSizeConverter : MultiConverterBase
    {
        Vector currentSize;
        Vector maxSize;

        // CharacterSize가 빠른 메뉴로 조작될 때 호출됨
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            currentSize = (Vector)values[0];
            maxSize = (Vector)values[1];
            return currentSize.Length / maxSize.Length * 100.0;
        }

        // CharacterSize가 Slider로 조작될 때 호출됨
        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            Vector targetSize = (double)value * 0.01 * maxSize;
            object[] values = { targetSize, Binding.DoNothing };
            return values;
        }
    }
}
