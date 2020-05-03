using _0x46696E616C.AstarAlgorithm;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.TechManager.Stats;
using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldManager;

namespace _0x46696E616C.Units.HostileMobManager
{
    class HeadlessHorseman : HostileMob
    {
        public HeadlessHorseman(string name, Vector2 size, float totalHealth, float currentHealth, Vector2 position, BaseUnitState state, TextureValue texture, Color color, TextureValue icon, WorldHandler world, float range, Stats teamStats) : base(name, size, totalHealth, currentHealth, position, state, texture, color, icon, world, range, teamStats)
        {
        }
        public override BasicUnit NewInstace(float currentHealth, Vector2 position)
        {
            return new HeadlessHorseman(this.name, this.Size, this.TotalHealth, currentHealth, position, BaseUnitState.Idle, TextureValue.HeadlessHorseman, Color.Red, TextureValue.HeadlessHorseman, world, this.stats[typeof(Range)].Value, this.teamStats);
        }
    }
}
