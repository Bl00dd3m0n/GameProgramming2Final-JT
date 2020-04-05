using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace _0x46696E616C.Buildings
{
    public class SolarPanel : Building, IProductionCenter
    {
        public int ProductionAMinute { get; protected set; }

        public List<IResource> productionTypes { get; protected set; }

        public SolarPanel(Game game, TextureValue texture, Vector2 position) : base(game, texture, position)
        {
            ProductionAMinute = 60;
            productionTypes = new List<IResource>() { new Energy() };

            Cost = new Wallet();
            Cost.Deposit(new Steel(), 200);
            Cost.Deposit(new Money(), 100);
            energyCost = 5;//Negative value for production of energy
            name = "Solar Panel";
            Position = position;
            Size = new Vector2(2, 4);
            TotalHealth = 0;
            CurrentHealth = 0;
        }
    }
}
