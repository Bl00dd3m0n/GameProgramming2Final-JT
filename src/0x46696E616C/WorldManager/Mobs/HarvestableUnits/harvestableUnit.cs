﻿using _0x46696E616C;
using _0x46696E616C.MobHandler;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldManager.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace WorldManager.Mobs.HarvestableUnits
{
    abstract class HarvestableUnit : ModifiableTile, IHarvestable
    {


        public IResource type { get; private set; }
        public string name { get; private set; }

        public Vector2 Size { get; private set; }

        public float TotalHealth { get; private set; }

        public float CurrentHealth { get; private set; }

        public Vector2 Position { get; private set; }

        protected HarvestableUnit(Game game, TextureValue texture, IResource type, string name, Vector2 size, float totalHealth, float currentHealth, Vector2 position) : base(game, texture, position)
        {
            this.type = type;
            this.name = name;
            this.Size = size;
            this.TotalHealth = totalHealth;
            this.CurrentHealth = currentHealth;
            this.Position = position;
        }


        public void Damage(float damage)
        {
            CurrentHealth -= damage;
            if(CurrentHealth <=0)
            {
                Die();
            }
        }

        public void Die()
        {
            throw new NotImplementedException();
        }

        public Wallet<IResource> Harvest(float efficiency)
        {
            Wallet<IResource> Harvest = new Wallet<IResource>();
            Harvest.Deposit(type, (int)(1 * efficiency));
            Damage(1 * efficiency);
            return Harvest;
        }
    }
}
