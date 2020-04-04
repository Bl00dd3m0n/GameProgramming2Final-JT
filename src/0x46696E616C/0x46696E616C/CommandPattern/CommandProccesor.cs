using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.WorldManager.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldManager;

namespace _0x46696E616C.CommandPattern
{
    class CommandProccesor
    {
        List<IResource> resources;
        List<IUnit> units;
        List<IUnit> selectedUnits;
        Wallet<IResource> player_Wallet;
        WorldHandler wh;
    }
}
