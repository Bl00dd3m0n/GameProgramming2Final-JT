using _0x46696E616C.WorldManager.ConcreteImplementations.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.WorldManager.WorldImplementations.Buildings
{
    public interface IResourceCharge
    {
        List<int> ChargeAMinute { get; }
        List<IResource> ChargeTypes { get; }
    }
}
