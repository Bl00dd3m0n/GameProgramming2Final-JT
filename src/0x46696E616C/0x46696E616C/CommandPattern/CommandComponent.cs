using _0x46696E616C.Buildings;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.Resources;
using _0x46696E616C.WorldManager.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.CommandPattern
{
    class CommandComponent
    {
        List<IResource> resources;
        List<IUnit> units;
        List<IEntity> selectedUnits;
        Wallet<IResource> player_Wallet;
        internal void Build(Building building)
        {
            Wallet<IResource> wallet = player_Wallet.Withdraw(building.Cost);
            if(wallet != null) // If the player has the resources
            {
                foreach(IEntity unit in selectedUnits)
                {
                    unit.QueueBuild();
                }
            }
            else
            {

            }
        }
    }
}
