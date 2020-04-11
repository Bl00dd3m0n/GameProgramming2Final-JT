using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;

namespace _0x46696E616C.Buildings
{
    public class ServerFarm : Building
    {
        public ServerFarm(Game game, TextureValue texture, Vector2 position) : base(game, texture, position)
        {
            Cost = new Wallet();
            Cost.Deposit(new Steel(), 1000);
            Cost.Deposit(new Wood(), 200);
            Cost.Deposit(new Money(), 100);
            energyCost = 5;
            name = "Server Farm";
            Position = position;
            Size = new Vector2(3, 3);
            TotalHealth = 2000;
        }
    }
}
