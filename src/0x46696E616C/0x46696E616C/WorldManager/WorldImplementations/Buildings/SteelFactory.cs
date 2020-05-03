using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Microsoft.Xna.Framework;
using _0x46696E616C.MobHandler;
using System.Collections.Generic;
using _0x46696E616C.WorldManager.ConcreteImplementations.Resources;
using _0x46696E616C.ConcreteImplementations.Resources;
using _0x46696E616C.ConcreteImplementations;
using _0x46696E616C.WorldManager.WorldImplementations.Buildings;
using _0x46696E616C.TechManager.Stats;
using WorldManager;
using _0x46696E616C.Units.Attacks;

namespace _0x46696E616C.Buildings
{
    public class SteelFactory : Building, IProductionCenter, IResourceCharge
    {
        public List<int> ChargeAMinute { get; protected set; }

        public List<IResource> ChargeTypes { get; protected set; }



        public List<int> ProductionAMinute { get; private set; }

        public List<IResource> productionTypes { get; private set; }

        public SteelFactory(TextureValue texture, Vector2 position, TextureValue icon, WorldHandler world, ProjectileManager proj) : base(texture, position, icon, world, proj)
        {
            Cost = new Wallet();
            Cost.Deposit(new Iron(), 200);
            Cost.Deposit(new Wood(), 300);
            Cost.Deposit(new Money(), 0);
            productionTypes = new List<IResource>() { new Steel() };
            ProductionAMinute= new List<int>() { 60 };
            ChargeAMinute = new List<int>() { 5, 60 };
            ChargeTypes = new List<IResource>() { new Energy(), new Iron() };
            name = "Steel Factory";
            Position = position;
            Size = new Vector2(3, 3);
            stats.Add(new Health("Health", 200));
            CurrentHealth = 0;
            BuildingDescription = "Used for production of steel, also costs iron for production";
        }

        public override Building NewInstace(TextureValue tex, Vector2 position, TextureValue Icon)
        {
            return new SteelFactory(tex, position, Icon, world,proj);
        }
    }
}
