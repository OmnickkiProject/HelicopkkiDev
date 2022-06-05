using System;
using System.Windows;

namespace HelicopkkiDev.Library
{
    static class ConvertHelper
    {
        /// <summary>
        /// 실수 범위의 degree를 -180도 초과 180도 이하의 degree로 변환
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double ToDeg180(this double degree)
        {
            degree %= 360.0;
            if (degree <= -180.0)
            {
                degree += 360.0;
            }
            else if (degree > 180.0)
            {
                degree -= 360.0;
            }
            return degree;
        }

        /// <summary>
        /// value를 min 이상 max 이하의 범위로 변환
        /// </summary>
        /// <param name="d"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static double ToRange(this double value, double min, double max)
        {
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
            return value;
        }

        /// <summary>
        /// value를 min 이상의 범위로 변환
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        public static double ToMinRange(this double value, double min)
        {
            return value < min ? min : value;
        }

        /// <summary>
        /// value를 max 이하의 범위로 변환
        /// </summary>
        /// <param name="value"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static double ToMaxRange(this double value, double max)
        {
            return value > max ? max : value;
        }

        /// <summary>
        /// degree를 radian으로 변환
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static double ToRadian(this double degree)
        {
            return (degree % 360.0) * Math.PI / 180.0;
        }

        /// <summary>
        /// 회전된 객체를 DragDelta시 받는 deltaX와 deltaY를 Screen 좌표계의 벡터로 변환
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static Vector DeltaToScreenGrid(double deltaX, double deltaY, double degree)
        {
            double rad = degree.ToRadian();
            var result = new Vector
            (
                deltaX * Math.Cos(rad) - deltaY * Math.Sin(rad),
                deltaX * Math.Sin(rad) + deltaY * Math.Cos(rad)
            );
            return result;
        }
    }
}
