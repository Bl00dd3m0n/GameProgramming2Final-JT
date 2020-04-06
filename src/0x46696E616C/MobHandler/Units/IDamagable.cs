using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.Units
{
    public interface IDamagable
    {
        float TotalHealth { get; }
        float CurrentHealth { get; }

        void Die();
        void Damage(float value);
    }
}
