using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Microsoft.Xna.Framework;
using _0x46696E616C.MobHandler;
using _0x46696E616C.ConcreteImplementations.Resources;
using _0x46696E616C.ConcreteImplementations;

namespace _0x46696E616C.Buildings
{
    public class FireWall : Building
    {
        public FireWall(Game game, TextureValue texture, Vector2 position, TextureValue icon) : base(game, texture, position, icon)
        {
            Cost = new Wallet();
            Cost.Deposit(new Steel(), 200);
            Cost.Deposit(new Wood(), 500);
            Cost.Deposit(new Money(), 100);
            energyCost = 20;
            name = "Firewall";
            Position = position;
            Size = new Vector2(1, 1);
            TotalHealth = 2000;
            CurrentHealth = 0;
            healthBar = new HealthBar(new Rectangle(new Point((int)position.X, (int)position.Y - 1), new Point((int)(Size.X * 16), (int)(Size.Y))));
            BuildingDescription = "A wall used to keep enemies away from your buildings";
        }
        public override Building NewInstace(Game game, TextureValue tex, Vector2 position, TextureValue Icon)
        {
            return new FireWall(game, tex, position, Icon);
        }
    }
}
