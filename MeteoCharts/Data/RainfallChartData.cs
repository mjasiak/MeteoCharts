using System.Collections.Generic;

namespace MeteoCharts.Data
{
	public class RainfallChartData
	{
		public string Id { get; set; }
		public IEnumerable<RainfallChartDataItem> RainfallChartDataItems { get; set; }
	}
}