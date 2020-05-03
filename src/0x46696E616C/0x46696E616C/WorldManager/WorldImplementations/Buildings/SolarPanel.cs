using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using _0x46696E616C.MobHandler;
using _0x46696E616C.WorldManager.ConcreteImplementations.Resources;
using _0x46696E616C.ConcreteImplementations.Resources;
using _0x46696E616C.ConcreteImplementations;
using _0x46696E616C.TechManager.Stats;
using WorldManager;
using _0x46696E616C.Units.Attacks;

namespace _0x46696E616C.Buildings
{
    public class SolarPanel : Building, IProductionCenter
    {
        public List<int> ProductionAMinute { get; protected set; }

        public List<IResource> productionTypes { get; protected set; }

        public SolarPanel(TextureValue texture, Vector2 position, TextureValue icon, WorldHandler world, ProjectileManager proj, Stats teamStats) : base(texture, position, icon, world, proj, teamStats)
        {
            ProductionAMinute = new List<int>() { 60 };
            productionTypes = new List<IResource>() { new Energy() };

            Cost = new Wallet();
            Cost.Deposit(new Steel(), 200);
            Cost.Deposit(new Money(), 100);
            productionTypes = new List<IResource>() { new Energy() };
            ProductionAMinute = new List<int>() { 60 };
            name = "Solar Panel";
            Position = position;
            Size = new Vector2(2, 4);
            stats.Add(new Health("Health", 200));
            currentHealth = 0;
            healthBar = new HealthBar(new Rectangle(new Point((int)position.X, (int)position.Y-1), new Point((int)(Size.X*16), (int)(Size.Y))));
            BuildingDescription = "Generates power for the system.";
        }

        public override Building NewInstace(TextureValue tex, Vector2 position, TextureValue Icon)
        {
            return new SolarPanel(tex, position, Icon, world, proj, teamStats);
        }
    }
}
