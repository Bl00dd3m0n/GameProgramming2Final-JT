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

namespace _0x46696E616C.Buildings
{
    public class Mines : Building
    {

        public List<int> ProductionAMinute { get; protected set; }

        public List<IResource> productionTypes { get; protected set; }

        public Mines(Game game, TextureValue texture, Vector2 position, TextureValue icon) : base(game, texture, position, icon)
        {
            ProductionAMinute = new List<int>() { 0 };
            productionTypes = new List<IResource>() { new Iron() };
            Cost = new Wallet();
            Cost.Deposit(new Iron(), 20);
            Cost.Deposit(new Wood(), 200);
            Cost.Deposit(new Money(), 100);
            energyCost = 5;
            name = "Mines";
            Position = position;
            Size = new Vector2(1, 1);
            TotalHealth = 200;
            CurrentHealth = 0;
            tags.Add("Iron Collector");
            healthBar = new HealthBar(new Rectangle(new Point((int)position.X, (int)position.Y - 1), new Point((int)(Size.X * 16), (int)(Size.Y))));
            BuildingDescription = "Used as a drop off point for iron ore";
        }
        public override Building NewInstace(Game game, TextureValue tex, Vector2 position, TextureValue Icon)
        {
            return new Mines(game, tex, position, Icon);
        }

        public override void Collect(Wallet resource)
        {
            Wallet wallet = new Wallet();
            wallet.Deposit(resource);
            worldComponent.Deposit(wallet);
        }
    }
}
