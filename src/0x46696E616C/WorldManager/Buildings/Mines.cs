﻿using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using _0x46696E616C.MobHandler;

namespace _0x46696E616C.Buildings
{
    public class Mines : Building, IProductionCenter
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
            healthBar = new HealthBar(new Rectangle(new Point((int)position.X, (int)position.Y - 1), new Point((int)(Size.X * 16), (int)(Size.Y))));
        }
        public override Building NewInstace(Game game, TextureValue tex, Vector2 position, TextureValue Icon)
        {
            return new Mines(game, tex, position, Icon);
        }
    }
}
