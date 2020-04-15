using _0x46696E616C.Buildings;
using _0x46696E616C.MobHandler.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.CommandPattern.GameCommands
{
    class TrainCommand: Command
    {
        IUnit unit;
        Building building;
        public TrainCommand(IUnit unit, Building building)
        {
            this.CommandName = "Train Command";
            this.unit = unit;
            this.building = building;
        }

        public override void Execute(CommandComponent uc)
        {
            uc.Train(building, unit);
            this.Log(uc);
        }
    }
}
