using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Microsoft.Xna.Framework;
using _0x46696E616C.MobHandler;
using _0x46696E616C.ConcreteImplementations.Resources;
using _0x46696E616C.ConcreteImplementations;
using _0x46696E616C.WorldManager.ConcreteImplementations.Resources;
using System.Collections.Generic;
using _0x46696E616C.WorldManager.WorldImplementations.Buildings;
using _0x46696E616C.TechManager.Stats;
using WorldManager;
using _0x46696E616C.Units.Attacks;

namespace _0x46696E616C.Buildings
{
    public class MediaCenter : Building, IResourceCharge
    {
        public List<int> ChargeAMinute { get; protected set; }

        public List<IResource> ChargeTypes { get; protected set; } 

        public MediaCenter(TextureValue texture, Vector2 position, TextureValue icon, WorldHandler world, ProjectileManager proj) : base(texture, position, icon, world, proj)
        {
            Cost = new Wallet();
            Cost.Deposit(new Steel(), 100);
            Cost.Deposit(new Wood(), 200);
            Cost.Deposit(new Money(), 100);
            ChargeAMinute = new List<int>() { 20 };
            ChargeTypes = new List<IResource>() { new Energy() };
            name = "Media Center";
            Position = position;
            Size = new Vector2(4, 4);
            stats.Add(new Health("Health", 2000));
            CurrentHealth = 0;
            healthBar = new HealthBar(new Rectangle(new Point((int)position.X, (int)position.Y - 1), new Point((int)(Size.X * 16), (int)(Size.Y))));
            BuildingDescription = "In theory suppose to generate content, kinda pointless at this moment";
        }
        public override Building NewInstace(TextureValue tex, Vector2 position, TextureValue Icon)
        {
            return new MediaCenter(tex, position, Icon, world ,proj);
        }
    }
}
