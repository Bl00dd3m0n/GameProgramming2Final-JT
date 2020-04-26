using _0x46696E616C.ConcreteImplementations;
using _0x46696E616C.TechManager.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechHandler;

namespace _0x46696E616C.TechManager.Technologies
{
    class DamageUpgrade : ITech
    {
        public int Level { get; protected set; }

        public Type technology { get; set; }

        Wallet cost { get; set; }

    }
}
