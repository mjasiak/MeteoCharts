using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoCharts.Colors
{
    public class RainfallChartColors
    {
        private static SKColor _hiRainColor = new SKColor(25, 83, 147);
        private static SKColor _loRainColor = new SKColor(85, 221, 235);
   
        public static SKShader GetRainColor(int canvasWidth, int canvasHeight)
        {
            SKPoint start = new SKPoint(canvasWidth / 2, (canvasHeight*0.75f)+20);
            SKPoint end = new SKPoint(canvasWidth / 2, 0);
            SKColor[] colors = new SKColor[2] { _loRainColor, _hiRainColor };
            return SKShader.CreateLinearGradient(start, end, colors, null, SKShaderTileMode.Clamp);
        }
    }
}
