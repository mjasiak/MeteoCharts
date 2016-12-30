using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoCharts.Charts
{
    public class TemperatureChart
    {
        public void RenderChart()
        {
            using (var surface = SKSurface.Create(width: 640, height: 480, SKColorType.N_32, SKAlphaType.Premul))
            {
                SKCanvas canvas = surface.Canvas;

                // Your drawing code goes here.
                Stream fileStream = File.OpenRead("MyImage.png");

                // clear the canvas / fill with white
                canvas.DrawColor(SKColors.White);

                // decode the bitmap from the stream
                using (var stream = new SKManagedStream(fileStream))
                using (var bitmap = SKBitmap.Decode(stream))
                using (var paint = new SKPaint())
                {
                    canvas.DrawBitmap(bitmap, SKRect.Create(Width, Height), paint);
                }           
            }
        }
    }
}

