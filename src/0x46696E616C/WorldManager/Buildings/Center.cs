using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;

namespace _0x46696E616C.Buildings
{
    public class Center : Building
    {
        public Center(Game game, TextureValue texture, Vector2 position) : base(game, texture, position)
        {
            Cost = new Wallet();
            Cost.Deposit(new Steel(), 1000);
            Cost.Deposit(new Wood(), 200);
            Cost.Deposit(new Money(), 100);
            energyCost = 5;
            name = "Center";
            Position = position;
            Size = new Vector2(4, 4);
            TotalHealth = 5000;
            CurrentHealth = 0;
        }
    }
}
