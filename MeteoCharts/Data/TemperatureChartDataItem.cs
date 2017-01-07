using System;
using MeteoCharts.Enums;
using MeteoCharts.Charts.ChartObjects;
using SkiaSharp;

namespace MeteoCharts.Data
{
	public class TemperatureChartDataItem : ChartItem
	{
		public TimeSpan Time { get; set; }
		public int Value { get; set; }
		public WeatherIconType IconType { get; set; }

        public SKColor Color { get; set; }
    }
}