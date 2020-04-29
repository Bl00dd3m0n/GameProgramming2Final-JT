using NationBuilder.TileHandlerLibrary;
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

        public InternetCafe(TextureValue texture, Vector2 position, TextureValue icon, WorldHandler world, ProjectileManager proj) : base(texture, position, icon, world, proj)
        {
            ProductionAMinute = new List<int>() { 5, 1 };
            productionTypes = new List<IResource>() { new Money(), new Likes() };
            Cost = new Wallet();
            Cost.Deposit(new Steel(), 2000);
            Cost.Deposit(new Wood(), 1000);
            Cost.Deposit(new Iron(), 100);
            ChargeAMinute = new List<int>() { 25 };
            ChargeTypes = new List<IResource>() { new Energy() };
            name = "Internet Cafe";
            Position = position;
            Size = new Vector2(2, 2);
            stats.Add(new Health("Health", 500));
            CurrentHealth = 0;
            healthBar = new HealthBar(new Rectangle(new Point((int)position.X, (int)position.Y - 1), new Point((int)(Size.X * 16), (int)(Size.Y))));
            BuildingDescription = "Internet Cafe used for units to generate money and likes in.";
        }
        public override Building NewInstace(TextureValue tex, Vector2 position, TextureValue Icon)
        {
            return new InternetCafe(tex, position, Icon, world, proj);
        }
    }
}
