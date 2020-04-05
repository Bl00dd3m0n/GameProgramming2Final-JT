using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.WorldManager.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.MobHandler
{
    public interface IHarvestable : IEntity
    {
        IResource type { get; }
        Wallet Harvest(float efficiency);
    }
}
