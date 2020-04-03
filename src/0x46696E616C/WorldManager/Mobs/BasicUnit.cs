using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.Units;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.CommandPattern.Commands
{
    class BasicUnit : IUnit
    {
        enum UnitState { Build, Attack, Flee, }

        public string name { get; private set; }

        public Vector2 Size { get; private set; }

        public float TotalHealth { get; private set; }

        public float CurrentHealth { get; private set; }

        public Vector2 Position { get; private set; }

        public BaseUnitState State { get; private set; }

        public void Damage(float amount)
        {
            CurrentHealth -= amount;
            if(CurrentHealth <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            
        }

        public void QueueBuild()
        {
            throw new NotImplementedException();
        }
    }
}
