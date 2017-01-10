using MeteoCharts.Charts.ChartObjects;
using MeteoCharts.Colors;
using MeteoCharts.Data;
using MeteoCharts.Enums;
using MeteoCharts.Interfaces;
using MeteoCharts.Render;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MeteoCharts.Charts
{
    public class TemperatureChart : Chart, IChartable
    {
        private TemperatureChartData _tempChartData;      
        public TemperatureChart(TemperatureChartData tempChartData)
        {
            _tempChartData = tempChartData;                
        }

        public void DrawChart(int canvasHeight, int spaceBetweenValues, string pathSaveFile)
        {
            this.canvasHeight = canvasHeight;
            this.spaceBetween = spaceBetweenValues;
            this.axisPercentOnCanvas = 0.75f;
            MathChart();
            using (var surface = SKSurface.Create(canvasWidth, canvasHeight, SKColorType.Rgb565, SKAlphaType.Premul))
            {
                SKCanvas canvas = surface.Canvas;
                canvas.Clear(SKColors.White);  
                                  
                canvas = DrawChartAxis(canvas);
                canvas = DrawChartScale(canvas);
                canvas = DrawChartBezier(canvas,spaceBetweenValues);
                canvas = DrawChartValues(canvas);
                canvas = DrawChartValuesEnchancers(canvas);
                canvas = DrawHours<IEnumerable<TemperatureChartDataItem>>(_tempChartData.TemperatureChartDataItems, canvas);
                canvas = DrawChartImages(canvas);
                
                ImageRender.Render(surface, pathSaveFile);
            }
        }
        protected override void MathChart()
        {
            canvasWidth = _tempChartData.TemperatureChartDataItems.Count() * spaceBetween;
            _chartSetting = SetChartRange(_chartSetting, GetValues(), canvasHeight);
            GetValueAndColorOfItem();
            MathChartAxis(_chartSetting, canvasHeight);
            MathChartValues<IEnumerable<TemperatureChartDataItem>>(_tempChartData.TemperatureChartDataItems);  
        }                   

        protected override SKCanvas DrawChartAxis(SKCanvas canvas)
        {
            SKPaint paint = new SKPaint() { Color = new SKColor(208, 208, 208), IsAntialias = true, StrokeCap = SKStrokeCap.Round };

            foreach (var axis in _chartAxis)
            {
                if (axis.value != 0)
                {
                    paint.Color = new SKColor(172, 172, 172);
                    paint.StrokeWidth = 1;
                }
                else {
                    paint.Color = new SKColor(125, 127, 126);
                    paint.StrokeWidth = 2;
                }
                canvas.DrawLine(axis.x0 + (0.1f * canvasHeight), axis.y0, axis.x1 + (canvasHeight - (0.84f * canvasHeight)), axis.y1, paint);
            }
            return canvas;
        }
        protected override SKCanvas DrawChartValues(SKCanvas canvas)
        {            
            SKPaint paintValues = new SKPaint() { Color = new SKColor(20, 20, 20), TextSize = 48.0f, TextAlign = SKTextAlign.Center, IsAntialias = true, FakeBoldText=true };
                       
            foreach (var obj in _tempChartData.TemperatureChartDataItems)
            {               
                canvas.DrawText(obj.chartValue.ToString() + "°", obj.x + 7, obj.y - 35, paintValues);               
            }
            return canvas;
        }
        protected override SKCanvas DrawChartScale(SKCanvas canvas)
        {
            SKPaint paint = new SKPaint() { Color = new SKColor(45, 45, 45), IsAntialias = true, TextSize = 30.0f, IsStroke = false, TextAlign = SKTextAlign.Right };

            foreach (var axis in _chartAxis)
            {
                if (axis.value != 0)
                {
                    paint.Color = new SKColor(45, 45, 45);
                    paint.FakeBoldText = false;
                }
                else
                {
                    paint.Color = new SKColor(65, 69, 68);
                    paint.FakeBoldText = true;
                }
                canvas.DrawText(axis.value.ToString(), axis.x0 + (canvasHeight - (0.92f * canvasHeight)), axis.y0 + 10, paint);
            }
            return canvas;
        }
        private SKCanvas DrawChartValuesEnchancers(SKCanvas canvas)
        {
            SKPaint whiteCircle = new SKPaint() { Color = new SKColor(254, 254, 254) };
            foreach (var obj in _tempChartData.TemperatureChartDataItems)
            {
                SKPaint paint = new SKPaint() { Color = obj.Color, StrokeCap = SKStrokeCap.Round };
                canvas.DrawLine(obj.x, obj.y, obj.x, _chartSetting.heightOfAxis + 20, paint);
                canvas.DrawCircle(obj.x, obj.y, 8, paint);
                canvas.DrawCircle(obj.x, obj.y, 5, whiteCircle);
            }
            return canvas;
        }
        private SKCanvas DrawChartBezier(SKCanvas canvas, int spaceBetweenValues)
        {                                          
            float m = 0;
            float dx1 = 0;
            float dy1 = 0;
            float dx2 = 0;
            float dy2 = 0;

            float f = 0.37f;
            float t = 0.6f;

            TemperatureChartDataItem prevObj = _tempChartData.TemperatureChartDataItems.First();           
            for (int i = 1; i <= _tempChartData.TemperatureChartDataItems.Count()-2; i++)
            {
                SKPath path = new SKPath();
                path.MoveTo(prevObj.x, prevObj.y);
                TemperatureChartDataItem currObj = _tempChartData.TemperatureChartDataItems.ToList()[i];
                TemperatureChartDataItem nextObj = _tempChartData.TemperatureChartDataItems.ToList()[i+1];

                SKPoint prevPoint = new SKPoint(prevObj.x, prevObj.y);
                SKPoint currPoint = new SKPoint(currObj.x, currObj.y);

                SKColor[] colors = new SKColor[2] { prevObj.Color, currObj.Color };

                var shader = SKShader.CreateLinearGradient(prevPoint,currPoint,colors,null,SKShaderTileMode.Clamp);
                SKPaint paint = new SKPaint() { IsAntialias = true, StrokeWidth = 4, Style = SKPaintStyle.Stroke, Shader = shader };

                if (nextObj != null) {
                    m = gradient(prevObj, nextObj);
                    dx2 = (nextObj.x - currObj.x) * -f;
                    dy2 = dx2 * m * t;
                }
                else {
                    dx2 = 0;
                    dy2 = 0;
                }

                float xc = (prevObj.x + nextObj.x) / 2;
                float yc = (prevObj.y + nextObj.y) / 2;

                path.CubicTo(prevObj.x - dx1, prevObj.y - dy1, currObj.x + dx2, currObj.y + dy2, currObj.x, currObj.y);

                dx1 = dx2;
                dy1 = dy2;
                prevObj = currObj;
                canvas.DrawPath(path, paint);
            }           
            return canvas;
        }
        private SKCanvas DrawChartImages(SKCanvas canvas)
        {
            int imageWidth = 50;
            foreach (var obj in _tempChartData.TemperatureChartDataItems)
            {
                Stream fileStream = File.OpenRead("Charts/ChartObjects/Images/"+obj.IconType+".png");
                using (var stream = new SKManagedStream(fileStream))
                using (var bitmap = SKBitmap.Decode(stream))
                using (var paint = new SKPaint())
                {
                    canvas.DrawBitmap(bitmap, SKRect.Create(obj.x-imageWidth/2,_chartSetting.heightOfAxis * 1.17f,imageWidth, 50), paint);
                }
            }
                return canvas;
        }

        private IEnumerable<int> GetValues()
        {
            List<int> values = new List<int>();
            foreach(var value in _tempChartData.TemperatureChartDataItems)
            {
                values.Add(value.Value);
            }
            return values;
        }
        private void GetValueAndColorOfItem()
        {
            TemperatureChartDataItem tempItem = _tempChartData.TemperatureChartDataItems.First();
            foreach(var item in _tempChartData.TemperatureChartDataItems)
            {
                item.chartValue = item.Value;
                item.Color = TemperatureChartColors.GetColor(item);
                tempItem = item;
            }
        }       
        private float gradient(TemperatureChartDataItem a, TemperatureChartDataItem b)
        {
            return (b.y - a.y) / (b.x - a.x);
        }
    }
}
    


