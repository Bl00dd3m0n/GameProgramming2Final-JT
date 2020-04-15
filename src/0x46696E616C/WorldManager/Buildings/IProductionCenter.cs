using _0x46696E616C.WorldManager.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.Buildings
{
    public interface IProductionCenter
    {
        List<int> ProductionAMinute { get; }
        List<IResource> productionTypes { get; }
    }
}
