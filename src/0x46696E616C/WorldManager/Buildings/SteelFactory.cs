using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;

namespace _0x46696E616C.Buildings
{
    public class SteelFactory : Building
    {
        public SteelFactory(Game game, TextureValue texture, Vector2 position) : base(game, texture, position)
        {
            Cost = new Wallet();
            name = "Steel Factory";
            Position = position;
            Size = new Vector2(3, 3);
            TotalHealth = 0;
            CurrentHealth = 0;
        }
    }
}
