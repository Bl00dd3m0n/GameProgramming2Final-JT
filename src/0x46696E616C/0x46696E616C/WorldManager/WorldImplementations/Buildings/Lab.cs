using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Microsoft.Xna.Framework;
using _0x46696E616C.MobHandler;
using _0x46696E616C.ConcreteImplementations.Resources;
using _0x46696E616C.ConcreteImplementations;

namespace _0x46696E616C.Buildings
{
    public class Lab : Building
    {
        public Lab(TextureValue texture, Vector2 position, TextureValue icon) : base(texture, position, icon)
        {
            Cost = new Wallet();
            Cost.Deposit(new Steel(), 10000);
            Cost.Deposit(new Wood(), 20000);
            Cost.Deposit(new Money(), 10000);
            energyCost = 100;
            Cost = new Wallet();
            name = "Lab";
            Position = position;
            Size = new Vector2(2, 2);
            TotalHealth = 1000;
            CurrentHealth = 0;
            healthBar = new HealthBar(new Rectangle(new Point((int)position.X, (int)position.Y - 1), new Point((int)(Size.X * 16), (int)(Size.Y))));
            BuildingDescription = "Used to learn technology to improve production.";
        }
        public override Building NewInstace(TextureValue tex, Vector2 position, TextureValue Icon)
        {
            return new Lab(tex, position, Icon);
        }
    }
}
