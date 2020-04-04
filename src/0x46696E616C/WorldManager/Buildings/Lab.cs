using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;

namespace _0x46696E616C.Buildings
{
    class Lab : Building
    {
        public Lab(Game game, TextureValue texture, Vector2 position) : base(game, texture, position)
        { 
            Cost = new Wallet<IResource>();
            name = "Lab";
            Position = position;
            Size = new Vector2(0, 0);
            TotalHealth = 0;
            CurrentHealth = 0;
        }
    }
}
