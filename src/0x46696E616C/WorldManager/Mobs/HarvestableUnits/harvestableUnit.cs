using _0x46696E616C;
using _0x46696E616C.MobHandler;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldManager.Mobs.HarvestableUnits
{
    class HarvestableUnit : IHarvestable
    {
        public IResource type { get; private set; }
        public string name { get; private set; }

        public Vector2 Size { get; private set; }

        public float TotalHealth { get; private set; }

        public float CurrentHealth { get; private set; }

        public Vector2 Position { get; private set; }

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
