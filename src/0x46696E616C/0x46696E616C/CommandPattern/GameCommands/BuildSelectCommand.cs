﻿using _0x46696E616C.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.CommandPattern.GameCommands
{
    class BuildSelectCommand : Command
    {
        public Building build { get; private set; }
        public BuildSelectCommand(Building build)
        {
            this.CommandName = "Select Building";
            this.build = build;
        }

        public override void Execute(CommandComponent uc)
        {
            uc.SelectBuild(build);
            this.Log(uc);
        }
        public override string Description()
        {
            return $"{build.Description()}";
        }
    }
}
