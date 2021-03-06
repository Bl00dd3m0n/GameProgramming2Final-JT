﻿using _0x46696E616C.Buildings;
using _0x46696E616C.MobHandler.Units;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldManager;

namespace _0x46696E616C.CommandPattern.Commands
{
    class BuildCommand : Command
    {
        private WorldHandler wh;
        private Vector2 position;
        public BuildCommand(Building building, WorldHandler wh, Vector2 position)
        {
            this.CommandName = "Build";
            this.wh = wh;
            this.position = position;
        }

        public override void Execute(CommandComponent uc)
        {
            uc.Build(wh, position);
            base.Execute(uc);
        }
    }
}
