using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoCharts.Charts.ChartObjects
{
    public abstract class Chart
    {
        protected ChartSetting _chartSetting = new ChartSetting();
        protected List<ChartAxis> _chartAxis = new List<ChartAxis>();
        protected int canvasWidth, canvasHeight, spaceBetween;

        protected void MathChartAxis(ChartSetting chartSetting, int canvasHeight)
        {
            float height = SetChartHeight(canvasHeight);
            int row = chartSetting.valuesRangeInScale;
            int value = chartSetting.maxInScale;
            while (row >= 0)
            {
                float rowHeight = height - (row * chartSetting.oneInScale);
                ChartAxis axis = new ChartAxis(0, rowHeight + 20, canvasWidth, rowHeight + 20, value);
                value -= 10;
                row -= 10;
                _chartAxis.Add(axis);
            }
        }
        protected void MathChartValues<T>(T chartItemLists) where T : IEnumerable<ChartItem>
        {
            int space = 0;
            foreach (var chartObj in chartItemLists)
            {
                MathChartValue(chartObj);
                space += spaceBetween;
            }
        }
        protected void MathChartValue(ChartItem chartObject)
        {
            chartObject.x = spaceBetween + (canvasHeight-(canvasHeight * 0.80f));
            chartObject.y = GetHeightOfValueInPixels(_chartSetting, chartObject) + 20;
        }
        protected SKCanvas DrawHours<T>(T chartListItems, SKCanvas canvas) where T : IEnumerable<ChartItem>
        {
            SKPaint paintHour = new SKPaint() { Color = new SKColor(0, 0, 0), TextSize = 30.0f, TextAlign = SKTextAlign.Center, IsAntialias = true };

            foreach (var obj in chartListItems)
            {
                if (DateTime.Now.Hour == obj.Time.Hours) canvas.DrawText("TERAZ", obj.x, _chartSetting.heightOfAxis * 1.05f + 40, paintHour);
                else canvas.DrawText(obj.Time.ToString(@"hh\:mm"), obj.x, _chartSetting.heightOfAxis * 1.05f + 40, paintHour);
            }
            return canvas;
        }

        protected abstract SKCanvas DrawChartAxis(SKCanvas canvas);
        protected abstract SKCanvas DrawChartValues(SKCanvas canvas);
        protected abstract void MathChart();

        protected ChartSetting SetChartRange(ChartSetting chartSetting, IEnumerable<int> values, int canvasHeight)
        {
            int min = values.First();
            int max = values.First();
            foreach (var value in values)
            {
                if (min > value) min = value;
                if (max < value) max = value;
            }

            chartSetting.min = min;
            chartSetting.max = max;

            chartSetting.setInScale(min, max);
            chartSetting.heightOfAxis = SetChartHeight(canvasHeight);

            chartSetting.oneInScale = chartSetting.heightOfAxis / chartSetting.valuesRangeInScale;
            return chartSetting;
        }

        private float SetChartHeight(float height)
        {
            return height * 0.75f;
        }
        private float GetHeightOfValueInPixels(ChartSetting chartSett, ChartItem chartObj)
        {
            int minus = chartSett.maxInScale - chartObj.chartValue;
            int minusInScale = chartSett.valuesRangeInScale - minus;
            return chartSett.heightOfAxis - (minusInScale * chartSett.oneInScale);
        }
    }
}
