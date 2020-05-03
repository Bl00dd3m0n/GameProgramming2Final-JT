

using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.TechManager.Stats;
using _0x46696E616C.Units.Attacks;
using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using System.Collections.Generic;
using WorldManager;

namespace _0x46696E616C.Units.HostileMobManager
{
    class Mage : HostileMob
    {
        ProjectileManager projectile;
        //Probably should add a decorator implementation for ranged and melee units this is just more simple at the moment
        public Mage(string name, Vector2 size, float totalHealth, float currentHealth, Vector2 position, BaseUnitState state, TextureValue texture, Color color, TextureValue icon, WorldHandler world, ProjectileManager projectile, float range, Stats teamStats) : base(name, size, totalHealth, currentHealth, position, state, texture, color, icon, world, range, teamStats)
        {
            this.projectile = projectile;
            attack = new Ranged(projectile, stats[typeof(Range)].Value);
            stats.Add(new MeleeDamage("Attack", 5));
            stats.Add(new Health("Health", 10));
        }
        public override BasicUnit NewInstace(float currentHealth, Vector2 position)
        {
            return new Mage(this.name, this.Size, this.TotalHealth, currentHealth, position, BaseUnitState.Idle, TextureValue.Mage, Color.Red, TextureValue.Mage, world, this.projectile, this.range, this.teamStats);
        }
    }
}
