using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using _0x46696E616C.MobHandler;
using WorldManager.Buildings;
using System;
using _0x46696E616C.ConcreteImplementations;
using _0x46696E616C.ConcreteImplementations.Resources;
using _0x46696E616C.WorldManager.ConcreteImplementations.Resources;
using _0x46696E616C.WorldManager.WorldImplementations.Buildings;
using _0x46696E616C.TechManager.Stats;
using WorldManager;
using _0x46696E616C.Units.Attacks;

namespace _0x46696E616C.Buildings
{
    public class Mines : Building, IResourceCharge
    {
        public List<int> ChargeAMinute { get; protected set; }

        public List<IResource> ChargeTypes { get; protected set; }

        public List<int> ProductionAMinute { get; protected set; }

        public List<IResource> productionTypes { get; protected set; }

        public Mines(TextureValue texture, Vector2 position, TextureValue icon, WorldHandler world, ProjectileManager proj) : base(texture, position, icon, world, proj)
        {
            ProductionAMinute = new List<int>() { 0 };
            productionTypes = new List<IResource>() { new Iron() };
            Cost = new Wallet();
            Cost.Deposit(new Iron(), 20);
            Cost.Deposit(new Wood(), 20);
            Cost.Deposit(new Money(), 10);
            ChargeAMinute = new List<int>() { 0 };
            ChargeTypes = new List<IResource>() { new Energy() };
            name = "Mines";
            Position = position;
            Size = new Vector2(1, 1);
            stats.Add(new Health("Health", 200));
            CurrentHealth = 0;
            tags.Add("Iron Collector");
            healthBar = new HealthBar(new Rectangle(new Point((int)position.X, (int)position.Y - 1), new Point((int)(Size.X * 16), (int)(Size.Y))));
            BuildingDescription = "Used as a drop off point for iron ore";
        }


        public override Building NewInstace(TextureValue tex, Vector2 position, TextureValue Icon)
        {
            return new Mines(tex, position, Icon, world, proj);
        }

        public override void Collect(Wallet resource)
        {
            Wallet wallet = new Wallet();
            wallet.Deposit(resource);
            worldComponent.Deposit(wallet);
        }
    }
}
