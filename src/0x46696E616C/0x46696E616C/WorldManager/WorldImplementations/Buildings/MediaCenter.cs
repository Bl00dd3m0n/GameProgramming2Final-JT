using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Microsoft.Xna.Framework;
using _0x46696E616C.MobHandler;
using _0x46696E616C.ConcreteImplementations.Resources;
using _0x46696E616C.ConcreteImplementations;

namespace _0x46696E616C.Buildings
{
    public class MediaCenter : Building
    {
        public MediaCenter(TextureValue texture, Vector2 position, TextureValue icon) : base(texture, position, icon)
        {
            Cost = new Wallet();
            Cost.Deposit(new Steel(), 1000);
            Cost.Deposit(new Wood(), 20000);
            Cost.Deposit(new Money(), 1000);
            energyCost = 20;
            name = "Media Center";
            Position = position;
            Size = new Vector2(4, 4);
            TotalHealth = 2000;
            CurrentHealth = 0;
            healthBar = new HealthBar(new Rectangle(new Point((int)position.X, (int)position.Y - 1), new Point((int)(Size.X * 16), (int)(Size.Y))));
            BuildingDescription = "In theory suppose to generate content, kinda pointless at this moment";
        }
        public override Building NewInstace(TextureValue tex, Vector2 position, TextureValue Icon)
        {
            return new MediaCenter(tex, position, Icon);
        }
    }
}
