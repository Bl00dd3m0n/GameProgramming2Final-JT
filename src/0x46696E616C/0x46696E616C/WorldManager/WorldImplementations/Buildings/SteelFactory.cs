using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Microsoft.Xna.Framework;
using _0x46696E616C.MobHandler;
using System.Collections.Generic;
using _0x46696E616C.WorldManager.ConcreteImplementations.Resources;
using _0x46696E616C.ConcreteImplementations.Resources;
using _0x46696E616C.ConcreteImplementations;

namespace _0x46696E616C.Buildings
{
    public class SteelFactory : Building, IProductionCenter
    {


        public List<int> ProductionAMinute { get; private set; }

        public List<IResource> productionTypes { get; private set; }

        public SteelFactory(Game game, TextureValue texture, Vector2 position, TextureValue icon) : base(game, texture, position, icon)
        {
            Cost = new Wallet();
            Cost.Deposit(new Iron(), 200);
            Cost.Deposit(new Wood(), 3000);
            Cost.Deposit(new Money(), 100);
            productionTypes = new List<IResource>() { new Steel() };
            ProductionAMinute= new List<int>() { 1 };
            Cost = new Wallet();
            name = "Steel Factory";
            Position = position;
            Size = new Vector2(3, 3);
            TotalHealth = 0;
            CurrentHealth = 0;
            BuildingDescription = "Used for production of steel, also costs iron for production";
        }

        public override Building NewInstace(Game game, TextureValue tex, Vector2 position, TextureValue Icon)
        {
            return new SteelFactory(game, tex, position, Icon);
        }
    }
}
