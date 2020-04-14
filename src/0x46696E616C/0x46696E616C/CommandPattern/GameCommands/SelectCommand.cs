using _0x46696E616C.MobHandler.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.CommandPattern.GameCommands
{
    class SelectCommand : Command
    {
        List<IUnit> units;
        IUnit unit;
        public SelectCommand(IUnit unit)
        {
            this.unit = unit;
            units = null;
        }
        public SelectCommand(List<IUnit> units)
        {
            this.units = units;
            unit = null;
        }
        public override void Execute(CommandComponent uc)
        {
            if (units != null) uc.Select(units);
            else if (unit != null) uc.Select(unit);
            else throw new Exception("no unit selected");//This should never happen if it does throw error
            base.Execute(uc);
        }
    }
}
