using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0x46696E616C.MobHandler.Units;

namespace _0x46696E616C.Units.Attacks
{
    class Melee : AttackType
    {
        public Melee(float range) : base(range)
        {
        }

        public override void Attack(IEntity target, IUnit Attacker, float Damage)
        {
            base.Attack(target, Attacker, Damage);
        }
    }
}
