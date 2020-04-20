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

        public SetSpawnPointCommand(Vector2 position)
        {
            this.CommandName = "Train Command";
            this.position = position;
        }

        public override void Execute(CommandComponent uc)
        {
            uc.SetSpawnPoint(position, building);
            this.Log(uc);
        }
        public override string Description()
        {
            return $"Click to get a flag placement for the spawn point of units trained in this \n building";
        }
    }
}
