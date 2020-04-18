using _0x46696E616C.MobHandler;
using _0x46696E616C.MobHandler.Units;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobHandler.Units
{
    public class Dummy : IEntity
    {
        public Dummy(string name, Vector2 position, Vector2 size, HealthBar healthBar, float totalHealth, float currentHealth)
        {
            this.name = name;
            Position = position;
            Size = size;
            this.healthBar = healthBar;
            TotalHealth = totalHealth;
            CurrentHealth = currentHealth;
        }

        public string name { get; private set; }

        public Vector2 Position { get; private set; }

        public Vector2 Size    { get; private set; }

        public HealthBar healthBar { get; set; }

        public float TotalHealth { get; private set; }

        public float CurrentHealth { get; private set; }


        public void Damage(float value)
        {
            throw new NotImplementedException();
        }

        public void Die()
        {
            throw new NotImplementedException();
        }
    }
}
