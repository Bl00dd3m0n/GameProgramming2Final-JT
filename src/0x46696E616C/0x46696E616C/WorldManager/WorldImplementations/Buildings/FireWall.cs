using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Microsoft.Xna.Framework;
using _0x46696E616C.MobHandler;
using _0x46696E616C.ConcreteImplementations.Resources;
using _0x46696E616C.ConcreteImplementations;
using _0x46696E616C.WorldManager.WorldImplementations.Buildings;
using _0x46696E616C.WorldManager.ConcreteImplementations.Resources;
using System.Collections.Generic;
using _0x46696E616C.TechManager.Stats;

namespace _0x46696E616C.Buildings
{
    public class FireWall : Building, IResourceCharge
    {
        public List<int> ChargeAMinute { get; protected set; }

        public List<IResource> ChargeTypes { get; protected set; }

        public FireWall(TextureValue texture, Vector2 position, TextureValue icon) : base(texture, position, icon)
        {
            Cost = new Wallet();
            Cost.Deposit(new Steel(), 50);
            Cost.Deposit(new Wood(), 10);
            Cost.Deposit(new Money(), 100);
            ChargeAMinute = new List<int>() { 20 };
            ChargeTypes = new List<IResource>() { new Energy() };
            name = "Firewall";
            Position = position;
            Size = new Vector2(1, 1);
            stats.Add(new Health("Health", 2000));
            CurrentHealth = 0;
            healthBar = new HealthBar(new Rectangle(new Point((int)position.X, (int)position.Y - 1), new Point((int)(Size.X * 16), (int)(Size.Y))));
            BuildingDescription = "A wall used to keep enemies away from your buildings";
        }
        public override Building NewInstace( TextureValue tex, Vector2 position, TextureValue Icon)
        {
            return new FireWall( tex, position, Icon);
        }
    }
}
