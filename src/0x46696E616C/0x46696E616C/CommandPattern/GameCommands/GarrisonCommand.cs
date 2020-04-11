using _0x46696E616C.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.CommandPattern
{
    class GarrisonCommand : Command
    {
        Building building;
        public GarrisonCommand(Building building)
        {
            this.building = building;
        }

        public override void Execute(CommandComponent uc)
        {
            uc.Garrison(building);
            base.Log(uc);
        }
    }
}
