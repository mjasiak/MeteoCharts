using MeteoCharts.Charts;
using MeteoCharts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoCharts.NinjectConsole
{
    public class NinjectBindings : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IChartable>().To<TemperatureChart>();
        }
    }
}
