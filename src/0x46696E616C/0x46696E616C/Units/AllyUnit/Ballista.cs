﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.ConcreteImplementations;
using _0x46696E616C.ConcreteImplementations.Resources;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.TechManager.Stats;
using _0x46696E616C.Units.Attacks;
using _0x46696E616C.WorldManager.ConcreteImplementations.Resources;
using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using WorldManager;

namespace _0x46696E616C.Units.AllyUnit
{
    internal class Ballista : OffensiveUnits
    {
        ProjectileManager projectile;
        public Ballista(string name, Vector2 size, float totalHealth, float currentHealth, Vector2 position, BaseUnitState state, TextureValue texture, Color color, TextureValue icon, ProjectileManager projectile, float range, Stats teamStats) : base(name, size, totalHealth, currentHealth, position, state, texture, color, icon, range, teamStats)
        {
            Cost = new Wallet();
            Cost.Deposit(new Steel(), 1);
            Cost.Deposit(new Wood(), 5);
            Cost.Deposit(new Money(), 20);

            attack = new Ranged(projectile, TextureValue.Arrow);

            stats.Add(new MeleeDamage("Attack", 50));
            stats.Add(new Health("Health", 10));
            this.projectile = projectile;
        }
        /// <summary>
        /// Returns a new unit based on the position
        /// </summary>
        /// <param name="currentHealth"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public override BasicUnit NewInstace(float currentHealth, Vector2 position)
        {
            return new Ballista(this.name, this.Size, this.TotalHealth, currentHealth, position, BaseUnitState.Idle, this.block.texture, this.tileColor, this.Icon, projectile, this.stats[typeof(Range)].Value, teamStats);
        }
    }
}
