using _0x46696E616C.Buildings;
using _0x46696E616C.MobHandler.Units;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.CommandPattern.GameCommands
{
    class SetSpawnPointCommand : Command
    {
        Vector2 position;
        Building building;
        public SetSpawnPointCommand(Vector2 position, Building building)
        {
            this.CommandName = "Train Command";
            this.position = position;
            this.building = building;
        }

        public override void Execute(CommandComponent uc)
        {
            uc.SetSpawnPoint(position, building);
            this.Log(uc);
        }
    }
}
