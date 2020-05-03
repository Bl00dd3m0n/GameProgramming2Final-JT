using _0x46696E616C.ConcreteImplementations;
using _0x46696E616C.ConcreteImplementations.Resources;
using _0x46696E616C.TechManager.Stats;
using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using System;
using TechHandler;

namespace _0x46696E616C.TechManager.Technologies
{
    class BuildUpgrade : StatTech, ITech
    {
        public Type technology { get; set; }

        public BuildUpgrade(Stat upgrade, TextureValue icon, Vector2 position) : base(upgrade, icon, position)
        {
            Cost = new Wallet();
            Cost.Deposit(new Likes(), 2.5f);
            Cost.Deposit(new Money(), 5.0f);
            LearnTime = 10;
        }
    }
}
