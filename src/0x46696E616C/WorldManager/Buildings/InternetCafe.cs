using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using _0x46696E616C.MobHandler;

namespace _0x46696E616C.Buildings
{
    public class InternetCafe : Building, IProductionCenter
    {
        public List<int> ProductionAMinute { get; protected set; }

        public List<IResource> productionTypes { get; protected set; }

        public InternetCafe(Game game, TextureValue texture, Vector2 position, TextureValue icon) : base(game, texture, position, icon)
        {
            ProductionAMinute = new List<int>() { 5, 1 };
            productionTypes = new List<IResource>() { new Money(), new Likes() };
            Cost = new Wallet();
            Cost.Deposit(new Steel(), 2000);
            Cost.Deposit(new Wood(), 0);
            Cost.Deposit(new Money(), 200);
            energyCost = 25;
            name = "InternetCafe";
            Position = position;
            Size = new Vector2(2, 2);
            TotalHealth = 500;
            CurrentHealth = 0;
            healthBar = new HealthBar(new Rectangle(new Point((int)position.X, (int)position.Y - 1), new Point((int)(Size.X * 16), (int)(Size.Y))));
        }
        public override Building NewInstace(Game game, TextureValue tex, Vector2 position, TextureValue Icon)
        {
            return new InternetCafe(game, tex, position, Icon);
        }
    }
}
