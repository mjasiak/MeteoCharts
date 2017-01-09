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
        private static SKColor _hiTempColor = new SKColor(247, 200, 58);
        private static SKColor _loTempColor = new SKColor(46, 129, 197);

        public static SKColor GetColor(TemperatureChartDataItem tempItem)
        {
            if (tempItem.Value < 30 && tempItem.Value > -15)
            {
                float value = 30 - tempItem.Value;
                float scaleValue = 45 - value;
                float percent = scaleValue / 45;
                return GetAverage(percent, _hiTempColor, _loTempColor);
            }
            else if (tempItem.Value <= -15) return _loTempColor;
            else return _hiTempColor;
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
    }
}
