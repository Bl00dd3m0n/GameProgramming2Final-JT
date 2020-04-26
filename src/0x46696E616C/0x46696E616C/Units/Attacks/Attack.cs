using _0x46696E616C.MobHandler.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.Units.Attacks
{
    public abstract class AttackType
    {
        public float range { get; protected set; }
        public AttackType(float range)
        {
            this.range = range;
        }
        //Animation needs to be implemented here
        public virtual void Attack(IEntity target, IUnit Attacker, float Damage)
        {
            target.Damage(Damage);
        }
    }
}
