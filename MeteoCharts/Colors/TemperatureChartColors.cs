using MeteoCharts.Data;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoCharts.Colors
{
    public class TemperatureChartColors
    {
        private static SKColor _tempColorFor45 = new SKColor(252,121,58);
        private static SKColor _tempColorFor30 = new SKColor(246,203,64);
        private static SKColor _tempColorFor15 = new SKColor(193,220,103);
        private static SKColor _tempColorFor0 = new SKColor(85,191,191);
        private static SKColor _tempColorForMinus11 = new SKColor(65,127,164);

        public static SKColor GetColor(TemperatureChartDataItem tempItem)
        {
            if (tempItem.chartValue >= 45) return _tempColorFor45;
            else if (tempItem.chartValue < 45 && tempItem.chartValue >= 30)
            {
                float percent = GetPercentageOfGradient(45, 15, tempItem.chartValue);
                return GetAverage(percent, _tempColorFor45, _tempColorFor30);
            }
            else if (tempItem.chartValue < 30 && tempItem.chartValue >= 15)
            {
                float percent = GetPercentageOfGradient(30, 15, tempItem.chartValue);
                return GetAverage(percent, _tempColorFor30, _tempColorFor15);
            }
            else if (tempItem.chartValue < 15 && tempItem.chartValue >= 0)
            {
                float percent = GetPercentageOfGradient(15, 15, tempItem.chartValue);
                return GetAverage(percent, _tempColorFor15, _tempColorFor0);
            }
            else if (tempItem.chartValue < 0 && tempItem.chartValue >= -11)
            {
                float percent = GetPercentageOfGradient(0, 11, tempItem.chartValue);
                return GetAverage(percent, _tempColorFor0, _tempColorForMinus11);
            }
            else return _tempColorForMinus11;
        }
        private static SKColor GetAverage(float percentage, SKColor high, SKColor low)
        {
            var w = percentage * 2 - 1;

            var w1 = (w + 1) / 2.0;
            var w2 = 1 - w1;

            int r = (Int32)(high.Red * w1 + low.Red * w2);
            int g = (Int32)(high.Green * w1 + low.Green * w2);
            int b = (Int32)(high.Blue * w1 + low.Blue * w2);

            return new SKColor((byte)r, (byte)g, (byte)b);
        }
        private static float GetPercentageOfGradient(float maxValue, float valueRange, float itemValue)
        {
            float value = maxValue - itemValue;
            float scaleValue = valueRange - value;
            return scaleValue / valueRange;
        }
    }
}
