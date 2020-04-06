using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace _0x46696E616C.Buildings
{
    public class Mines : Building, IProductionCenter
    {

        public int ProductionAMinute { get; protected set; }

        public List<IResource> productionTypes { get; protected set; }

        public Mines(Game game, TextureValue texture, Vector2 position) : base(game, texture, position)
        {
            ProductionAMinute = 1;
            productionTypes = new List<IResource>() { new Iron() };
            Cost = new Wallet();
            Cost.Deposit(new Iron(), 20);
            Cost.Deposit(new Wood(), 200);
            Cost.Deposit(new Money(), 100);
            energyCost = 5;
            name = "Mines";
            Position = position;
            Size = new Vector2(0, 0);
            TotalHealth = 200;
            CurrentHealth = 0;
        }
    }
}
