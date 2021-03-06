﻿using _0x46696E616C.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.CommandPattern.GameCommands
{
    class RepairCommand : Command
    {
        Building building;
        public RepairCommand(Building building)
        {
            this.building = building;
        }

        public override void Execute(CommandComponent uc)
        {
            uc.Repair(building);
            base.Log(uc);
        }
    }
}

