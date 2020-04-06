using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace _0x46696E616C.Buildings
{
    public class InternetCafe : Building, IProductionCenter
    {
        public int ProductionAMinute { get; protected set; }

        public List<IResource> productionTypes { get; protected set; }

        public InternetCafe(Game game, TextureValue texture, Vector2 position) : base(game, texture, position)
        {
            ProductionAMinute = 1;
            productionTypes = new List<IResource>() { new Money(), new Likes() };
            Cost = new Wallet();
            Cost.Deposit(new Steel(), 2000);
            Cost.Deposit(new Wood(), 0);
            Cost.Deposit(new Money(), 500);
            energyCost = 25;
            name = "InternetCafe";
            Position = position;
            Size = new Vector2(0, 0);
            TotalHealth = 500;
            CurrentHealth = 0;
        }
    }
}
