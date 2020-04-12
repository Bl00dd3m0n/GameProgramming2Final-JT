using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;
using _0x46696E616C.MobHandler;
using System.Collections.Generic;

namespace _0x46696E616C.Buildings
{
    public class SteelFactory : Building, IProductionCenter
    {


        public List<int> ProductionAMinute { get; private set; }

        public List<IResource> productionTypes { get; private set; }

        public SteelFactory(Game game, TextureValue texture, Vector2 position, TextureValue icon) : base(game, texture, position, icon)
        {
            productionTypes = new List<IResource>() { new Steel() };
            ProductionAMinute= new List<int>() { 1 };
            Cost = new Wallet();
            name = "Steel Factory";
            Position = position;
            Size = new Vector2(3, 3);
            TotalHealth = 0;
            CurrentHealth = 0;
        }

        public override Building NewInstace(Game game, TextureValue tex, Vector2 position, TextureValue Icon)
        {
            return new SteelFactory(game, tex, position, Icon);
        }
    }
}
