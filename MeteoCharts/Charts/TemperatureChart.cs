using MeteoCharts.Charts.ChartObjects;
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
            MathChart(spaceBetweenValues);
            using (var surface = SKSurface.Create(canvasWidth, canvasHeight, SKColorType.Rgb565, SKAlphaType.Premul))
            {
                SKCanvas canvas = surface.Canvas;
                canvas.Clear(SKColors.White);  
                                  
                canvas = DrawChartAxis(canvas);
                canvas = DrawChartBezier(canvas,spaceBetweenValues);
                canvas = DrawChartValues(canvas);
                
                ImageRender.Render(surface, pathSaveFile);
            }
        }
        private void MathChart(int spaceBetween)
        {
            canvasWidth = _tempChartData.TemperatureChartDataItems.Count() * spaceBetween;
            _chartSetting = SetChartRange(_chartSetting, GetValues(), canvasHeight);

            GetValueAndColorOfItem();

            MathChartAxis(_chartSetting, canvasHeight);
            MathChartValues(spaceBetween);         
        }     
        
        private void MathChartValues(int spaceBetween)
        {
            int space = 0;
            foreach (var chartObj in _tempChartData.TemperatureChartDataItems)
            {
                MathChartValue(chartObj, space);
                space += spaceBetween;
            }
        }

        private SKCanvas DrawChartAxis(SKCanvas canvas)
        {
            SKPaint paint = new SKPaint() { Color = new SKColor(208, 208, 208), IsAntialias = true, StrokeCap = SKStrokeCap.Round };
            SKPaint paint2 = new SKPaint() { Color = new SKColor(45, 45, 45), IsAntialias = true, TextSize = 30.0f, IsStroke = false, TextAlign = SKTextAlign.Right };

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
                canvas.DrawLine(axis.x0 + (0.08f * canvasHeight), axis.y0, axis.x1 + (0.09f * canvasHeight), axis.y1, paint);
                canvas.DrawText(axis.value.ToString(), axis.x0 + (0.06f * canvasHeight), axis.y0 + 10, paint2);
            }
            return canvas;
        }
        private SKCanvas DrawChartValues(SKCanvas canvas)
        {
            SKPaint paint = new SKPaint() { Color = new SKColor(0, 0, 0), StrokeCap = SKStrokeCap.Round };
            SKPaint paintValues = new SKPaint() { Color = new SKColor(20, 20, 20), TextSize = 48.0f, TextAlign = SKTextAlign.Center, IsAntialias = true, FakeBoldText=true };
            SKPaint paintCircle = new SKPaint() { Color = new SKColor(254, 254, 254) };
            SKPaint paintHour = new SKPaint() { Color = new SKColor(0, 0, 0), TextSize = 30.0f, TextAlign = SKTextAlign.Center, IsAntialias = true };

            foreach (var obj in _tempChartData.TemperatureChartDataItems)
            {
                canvas.DrawCircle(obj.x, obj.y, 8,paint);                               
                canvas.DrawText(obj.chartValue.ToString() + "°", obj.x + 7, obj.y - 35, paintValues);
                canvas.DrawLine(obj.x, obj.y, obj.x, _chartSetting.heightOfAxis+20,paint);
                canvas.DrawCircle(obj.x, obj.y, 5, paintCircle);

                if(_tempChartData.TemperatureChartDataItems.First() == obj) canvas.DrawText("TERAZ", obj.x, _chartSetting.heightOfAxis * 1.05f+40, paintHour);
                else canvas.DrawText(obj.Time.ToString(@"hh\:mm"), obj.x, _chartSetting.heightOfAxis * 1.05f+40, paintHour);
            }
            return canvas;
        }
        private SKCanvas DrawChartBezier(SKCanvas canvas, int spaceBetweenValues)
        {
            SKPath path = new SKPath();
            SKPaint paint = new SKPaint() { Color= new SKColor(0, 0, 0), IsAntialias= true, StrokeWidth = 4, Style=SKPaintStyle.Stroke };
                                
            float m = 0;
            float dx1 = 0;
            float dy1 = 0;
            float dx2 = 0;
            float dy2 = 0;

            float f = 0.35f;
            float t = 0.6f;

            TemperatureChartDataItem prevObj = _tempChartData.TemperatureChartDataItems.First();
            path.MoveTo(prevObj.x, prevObj.y);
            for (int i = 1; i <= _tempChartData.TemperatureChartDataItems.Count()-2; i++)
            {              
                TemperatureChartDataItem currObj = _tempChartData.TemperatureChartDataItems.ToList()[i];
                TemperatureChartDataItem nextObj = _tempChartData.TemperatureChartDataItems.ToList()[i+1];
                if(nextObj != null) {
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
            }
            canvas.DrawPath(path, paint);
            return canvas;
        }

        private float gradient(TemperatureChartDataItem a, TemperatureChartDataItem b)
        {
            return (b.y - a.y) / (b.x - a.x);
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
                if (tempItem.Value > item.Value) tempItem.willFall = true;
                else if(tempItem.Value < item.Value) tempItem.willClimb = true;
                item.chartValue = item.Value;
                item.Color = new SKColor();
                tempItem = item;
            }
        }

    }
}
    


