

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
        //Probably should add a decorator implementation for ranged and melee units this is just more simple at the moment
        public Mage(string name, Vector2 size, float totalHealth, float currentHealth, Vector2 position, BaseUnitState state, TextureValue texture, Color color, TextureValue icon, WorldHandler world, ProjectileManager projectile, float range) : base(name, size, totalHealth, currentHealth, position, state, texture, color, icon, world, range)
        {
            attack = new Ranged(projectile, stats[typeof(Range)].Value);
            stats.Add(new AttackPower("Attack", 5));
            stats.Add(new Health("Health", 10));
        }
    }
}
