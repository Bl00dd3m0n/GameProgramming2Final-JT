using _0x46696E616C;
using _0x46696E616C.ConcreteImplementations;
using _0x46696E616C.MobHandler;
using _0x46696E616C.WorldManager.ConcreteImplementations.Resources;
using Microsoft.Xna.Framework;
using MobHandler;
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
    public abstract class HarvestableUnit : ModifiableTile, IHarvestable
    {


        public IResource type { get; private set; }

        protected HarvestableUnit(TextureValue texture, IResource type, string name, Vector2 size, float totalHealth, float currentHealth, Vector2 position, Color color) : base(texture, position, color)
        {
            this.type = type;
            this.name = name;
            this.Size = size;
            this.TotalHealth = totalHealth;
            this.CurrentHealth = currentHealth;
            this.Position = position;
            healthBar = new HealthBar(new Rectangle(position.ToPoint()-new Point(0, (int)(size.Y*16+1)), size.ToPoint()));
        }


        public override void Damage(float damage)
        {
            base.Damage(damage);
        }

        public override void Die()
        {
            base.Die();
        }

        public Wallet Harvest(float efficiency)
        {
            Wallet Harvest = new Wallet();
            Harvest.Deposit(type, (int)(1 * efficiency));
            Damage(1 * efficiency);
            return Harvest;
        }

        public void Return(Wallet wal)
        {
            this.CurrentHealth+= wal.Count(type);
        }
    }
}
