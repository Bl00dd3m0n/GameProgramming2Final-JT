﻿using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using _0x46696E616C.MobHandler;
using _0x46696E616C.WorldManager.ConcreteImplementations.Resources;
using _0x46696E616C.ConcreteImplementations.Resources;
using _0x46696E616C.ConcreteImplementations;
using _0x46696E616C.WorldManager.WorldImplementations.Buildings;
using _0x46696E616C.TechManager.Stats;
using WorldManager;
using _0x46696E616C.Units.Attacks;

namespace _0x46696E616C.Buildings
{
    public class InternetCafe : Building, IProductionCenter, IResourceCharge
    {
        public List<int> ProductionAMinute { get; protected set; }

        public List<IResource> productionTypes { get; protected set; }

        public List<int> ChargeAMinute { get; protected set; }

        public List<IResource> ChargeTypes { get; protected set; }

        public InternetCafe(TextureValue texture, Vector2 position, TextureValue icon, ProjectileManager proj, Stats teamStats) : base(texture, position, icon, proj, teamStats)
        {
            ProductionAMinute = new List<int>() { 5, 1 };
            productionTypes = new List<IResource>() { new Money(), new Likes() };
            Cost = new Wallet();
            Cost.Deposit(new Steel(), 200);
            Cost.Deposit(new Wood(), 100);
            Cost.Deposit(new Iron(), 100);
            ChargeAMinute = new List<int>() { 25 };
            ChargeTypes = new List<IResource>() { new Energy() };
            name = "Internet Cafe";
            Position = position;
            stats.Add(new Health("Health", 500));
            currentHealth = 0;
            healthBar = new HealthBar(new Rectangle(new Point((int)position.X, (int)position.Y - 1), new Point((int)(Size.X * 16), (int)(Size.Y))));
            BuildingDescription = "Internet Cafe used for units to generate money and likes in.";
        }
        public override Building NewInstace(TextureValue tex, Vector2 position, TextureValue Icon)
        {
            return new InternetCafe(tex, position, Icon, proj, teamStats);
        }
    }
}
