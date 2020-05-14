using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.TechManager.Stats;
using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using WorldManager;

namespace _0x46696E616C.Units.AllyUnit
{
    internal class Priest : OffensiveUnits
    {
        public Priest(string name, Vector2 size, float totalHealth, float currentHealth, Vector2 position, BaseUnitState state, TextureValue texture, Color color, TextureValue icon, float range, Stats teamStats) : base(name, size, totalHealth, currentHealth, position, state, texture, color, icon, range, teamStats)
        {

        }
        /// <summary>
        /// Returns a new unit based on the position
        /// </summary>
        /// <param name="currentHealth"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public override BasicUnit NewInstace(float currentHealth, Vector2 position)
        {
            return new Priest(this.name, this.Size, this.TotalHealth, currentHealth, position, BaseUnitState.Idle, this.block.texture, this.tileColor, this.Icon, this.range, teamStats);
        }
    }
}
