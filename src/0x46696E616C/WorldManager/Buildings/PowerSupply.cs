using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using _0x46696E616C.MobHandler;

namespace _0x46696E616C.Buildings
{
    public class PowerSupply : Building
    {
        public PowerSupply(Game game, TextureValue texture, Vector2 position, TextureValue icon) : base(game, texture, position, icon)
        {
            Cost = new Wallet();
            Cost.Deposit(new Steel(), 1000);
            Cost.Deposit(new Wood(), 200);
            Cost.Deposit(new Money(), 100);
            energyCost = 5;
            name = "Power Supply";
            Position = position;
            Size = new Vector2(2, 2);
            TotalHealth = 2000;
            healthBar = new HealthBar(new Rectangle(new Point((int)position.X, (int)position.Y - 1), new Point((int)(Size.X * 16), (int)(Size.Y))));
        }
        public override Building NewInstace(Game game, TextureValue tex, Vector2 position, TextureValue Icon)
        {
            return new PowerSupply(game, tex, position, Icon);
        }
    }
}
