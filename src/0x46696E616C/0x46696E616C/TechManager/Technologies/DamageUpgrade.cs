using _0x46696E616C.ConcreteImplementations;
using _0x46696E616C.ConcreteImplementations.Resources;
using _0x46696E616C.TechManager.Stats;
using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechHandler;

namespace _0x46696E616C.TechManager.Technologies
{
    class DamageUpgrade : StatTech, ITech
    {
        public Type technology { get; set; }

        public DamageUpgrade(Stat upgrade, TextureValue icon, Vector2 position) : base(upgrade, icon, position)
        {
            cost = new Wallet();
            cost.Deposit(new Likes(), 25);
            cost.Deposit(new Money(), 50);
        }

    }
}
