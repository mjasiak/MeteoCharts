using MeteoCharts.Data;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoCharts.Colors
{
    public class ColorsRepository
    {
        private static SKColor _hiTempColor = new SKColor(247, 200, 58);
        private static SKColor _loTempColor = new SKColor(46, 129, 197);
        //private static SKColor _hiTempColor = new SKColor(69, 104, 245);
        //private static SKColor _loTempColor = new SKColor(255, 85, 0);
        private static SKColor _hiRainColor = new SKColor(25,83,147);
        private static SKColor _loRainColor = new SKColor(85,221,235);

        public static SKColor GetColor(TemperatureChartDataItem tempItem)
        {

            if (tempItem.Value < 30 && tempItem.Value > -15)
            {
                float value = 30 - tempItem.Value;
                float scaleValue = 45 - value;
                float percent = scaleValue / 45;
                return GetAverageTest(percent, _hiTempColor, _loTempColor);
            }
            else if (tempItem.Value <= -15) return _loTempColor;
            else return _hiTempColor;
        }
        public static SKShader GetRainColor(int canvasWidth, int canvasHeight)
        {
            SKPoint start = new SKPoint(canvasWidth / 2, canvasHeight);
            SKPoint end = new SKPoint(canvasWidth / 2, 0);
            SKColor[] colors = new SKColor[2] { _loRainColor, _hiRainColor };
            return SKShader.CreateLinearGradient(start, end, colors, null, SKShaderTileMode.Clamp);
        }

        private static SKColor GetAverage(SKColor high, SKColor low, float percent)
        {
            int rh, gh, bh, rl, gl, bl, ra,ga,ba;
            rh = high.Red;
            gh = high.Green;
            bh = high.Blue;
            rl = low.Red;
            gl = low.Green;
            bl = low.Blue;
            ra = (Int32)((rh + rl) * percent);
            ga = (Int32)((gh + gl) * percent);
            ba = (Int32)((bh + bl) * percent);

            return new SKColor((byte)ra,(byte)ga,(byte)ba);
        }
        private static SKColor GetAverageTest(float percentage, SKColor high, SKColor low)
        {
            var w = percentage * 2 - 1;

            var w1 = (w + 1) / 2.0;
            var w2 = 1 - w1;

            int r = (Int32)(high.Red * w1 + low.Red * w2);
            int g = (Int32)(high.Green * w1 + low.Green * w2);
            int b = (Int32)(high.Blue * w1 + low.Blue * w2);

            return new SKColor((byte)r,(byte)g,(byte)b);
        }
    }
}
