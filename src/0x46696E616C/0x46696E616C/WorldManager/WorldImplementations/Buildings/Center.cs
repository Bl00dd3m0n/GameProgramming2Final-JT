using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Microsoft.Xna.Framework;
using _0x46696E616C.MobHandler.Units;
using TechHandler;
using _0x46696E616C.MobHandler;
using WorldManager.Buildings;
using System;
using _0x46696E616C.ConcreteImplementations;
using _0x46696E616C.ConcreteImplementations.Resources;

namespace _0x46696E616C.Buildings
{
    public class Center : Building, ICollectionCenter<Wallet>
    {
        public Center(Game game, TextureValue texture, Vector2 position, TextureValue icon) : base(game, texture, position, icon)
        {
            Cost = new Wallet();
            Cost.Deposit(new Steel(), 1000);
            Cost.Deposit(new Wood(), 200);
            Cost.Deposit(new Money(), 100);
            energyCost = 0;
            name = "Center";
            Position = position;
            Size = new Vector2(4, 4);
            TotalHealth = 5000;
            CurrentHealth = 0;
            tags.Add("Iron Collector");
            tags.Add("Wood Collector");
            healthBar = new HealthBar(new Rectangle(new Point((int)position.X, (int)position.Y - 1), new Point((int)(Size.X * 16), (int)(Size.Y))));
            BuildingDescription = "The center allows the user to train Civilians";
        }
        public override Building NewInstace(Game game, TextureValue tex, Vector2 position, TextureValue Icon)
        {
            return new Center(game, tex, position, Icon);
        }

        public override void Collect(Wallet resource)
        {
            worldComponent.Deposit(resource);
        }
    }
}
