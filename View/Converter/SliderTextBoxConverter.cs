using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HelicopkkiDev.View.Converter
{
    /// <summary>
    /// Slider TextBox에 입력된 Text를 유효한 값으로 변환
    /// </summary>
    class SliderTextBoxConverter : MultiConverterBase
    {
        private double? minValue;
        private double? maxValue;
        private double validValue;

        // minValue, maxValue의 OneTime 할당 및 validValue 변경 시 호출됨
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            validValue = (double)values[0];
            if(minValue == null && maxValue == null)
            {
                minValue = (double?)values[1];
                maxValue = (double?)values[2];
            }
            return validValue;
        }

        // TextBox가 키보드 포커스를 잃었을 때 호출됨
        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            double targetValue;
            try
            {
                targetValue = Double.Parse((string)value);
                if (minValue > targetValue)
                {
                    targetValue = (double)minValue;
                }
                else if (maxValue < targetValue)
                {
                    targetValue = (double)maxValue;
                }
            }
            catch
            {
                targetValue = validValue;
            }
            return new object[] {targetValue, Binding.DoNothing, Binding.DoNothing };
        }
    }
}
