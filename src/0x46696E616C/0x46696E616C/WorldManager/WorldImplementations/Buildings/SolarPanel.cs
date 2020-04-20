using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using _0x46696E616C.MobHandler;
using _0x46696E616C.WorldManager.ConcreteImplementations.Resources;
using _0x46696E616C.ConcreteImplementations.Resources;
using _0x46696E616C.ConcreteImplementations;

namespace _0x46696E616C.Buildings
{
    public class SolarPanel : Building, IProductionCenter
    {
        public List<int> ProductionAMinute { get; protected set; }

        public List<IResource> productionTypes { get; protected set; }

        public SolarPanel(Game game, TextureValue texture, Vector2 position, TextureValue icon) : base(game, texture, position, icon)
        {
            ProductionAMinute = new List<int>() { 60 };
            productionTypes = new List<IResource>() { new Energy() };

            Cost = new Wallet();
            Cost.Deposit(new Steel(), 200);
            Cost.Deposit(new Money(), 100);
            energyCost = 5;//Negative value for production of energy
            name = "Solar Panel";
            Position = position;
            Size = new Vector2(2, 4);
            TotalHealth = 200;
            CurrentHealth = 0;
            healthBar = new HealthBar(new Rectangle(new Point((int)position.X, (int)position.Y-1), new Point((int)(Size.X*16), (int)(Size.Y))));
            BuildingDescription = "Generates power for the system.";
        }
        public override Building NewInstace(Game game, TextureValue tex, Vector2 position, TextureValue Icon)
        {
            return new SolarPanel(game, tex, position, Icon);
        }
    }
}
