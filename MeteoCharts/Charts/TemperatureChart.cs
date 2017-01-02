using MeteoCharts.Interfaces;
using MeteoCharts.Render;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoCharts.Charts
{
    public class TemperatureChart : IChartable
    {
        public void GenerateChart()
        {
            using (var surface = SKSurface.Create(640, 480, SKColorType.Rgb565, SKAlphaType.Premul))
            {
                SKCanvas canvas = surface.Canvas;

                // clear the canvas / fill with white
                canvas.Clear(SKColors.White);

                // set up drawing tools
                using (var paint = new SKPaint())
                {
                    paint.IsAntialias = true;
                    paint.Color = new SKColor(0x2c, 0x3e, 0x50);
                    paint.StrokeCap = SKStrokeCap.Round;

                        // draw the Xamagon line
                    canvas.DrawLine(0,10,10,20, paint);
                }
                    ImageRender.Render(surface);
                }
            }
    }
}
    


