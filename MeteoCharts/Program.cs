using System;
using System.Collections.Generic;
using MeteoCharts.Data;
using MeteoCharts.Enums;
using MeteoCharts.Charts;
using MeteoCharts.Interfaces;
using System.Reflection;

namespace MeteoCharts
{
	internal class Program
	{
		private static readonly uint[] RainfallValues = { 30, 36, 5, 3, 0, 28 };
		private static readonly int[] TemperatureValues = {28, 19, -1, 2, -5, -11 };
        //private static readonly int[] TemperatureValues = { 30, 19, 19, 17, 14, 8, 3 };
        private static TimeSpan _timeOfToday;

		private static void Main()
		{
			_timeOfToday = new TimeSpan(0, DateTime.Now.Hour, 0, 0);

			var rainfallChartData = GetRainfallChartData();
			var temperatureChartData = GetTemperatureChartData();

            RainfallChart rainChart = new RainfallChart(rainfallChartData);
            rainChart.DrawChart(720, 150, "C:/RainfallChart.png");

            TemperatureChart tempChart = new TemperatureChart(temperatureChartData);
            tempChart.DrawChart(720, 150, "C:/TemperatureChart.png");
		}

		private static RainfallChartData GetRainfallChartData()
		{
			return new RainfallChartData
			{
				Id = "asdf",
				RainfallChartDataItems = GenerateRainfallValues()
			};
		}

		private static TemperatureChartData GetTemperatureChartData()
		{
			return new TemperatureChartData
			{
				Id = "asdf",
				TemperatureChartDataItems = GenerateTemperatureValues()
			};
		}

		private static IEnumerable<TemperatureChartDataItem> GenerateTemperatureValues()
		{
			var temperatureChartDataItems = new List<TemperatureChartDataItem>();
			for (int i = 0; i < 24; i++)
			{
				temperatureChartDataItems.Add(new TemperatureChartDataItem
				{
					Time = _timeOfToday.Add(new TimeSpan(i, 0, 0)),
					Value = TemperatureValues[i % TemperatureValues.Length],
					IconType = (WeatherIconType)(i % 3)
				});
		}
			return temperatureChartDataItems;
		}

		private static IEnumerable<RainfallChartDataItem> GenerateRainfallValues()
		{
			var rainfallChartDataItems = new List<RainfallChartDataItem>();
			for (int i = 0; i < 24; i++)
			{
				rainfallChartDataItems.Add(new RainfallChartDataItem
				{
					Time = _timeOfToday.Add(new TimeSpan(i, 0, 0)),
					Value = RainfallValues[i % RainfallValues.Length]
				});
			}
			return rainfallChartDataItems;
		}
	}
}