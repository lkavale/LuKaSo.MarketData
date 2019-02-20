using System;
using System.Collections.Generic;
using System.Text;

namespace LuKaSo.MarketData.Types.Instruments
{
    public class DataAvalability
    {
        DateTime DatasourceFrom { get; set; }
        DateTime? DatasourceTo { get; set; }
        DateTime? LocalFrom { get; set; }
        DateTime? LocalTo { get; set; }

    }
}
