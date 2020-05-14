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
    public class ServerFarm : Building, IResourceCharge
    {
        public List<int> ChargeAMinute { get; protected set; }

        public List<IResource> ChargeTypes { get; protected set; }

        public ServerFarm(TextureValue texture, Vector2 position, TextureValue icon, ProjectileManager proj, Stats teamStats) : base(texture, position, icon, proj, teamStats)
        {
            Cost = new Wallet();
            Cost.Deposit(new Steel(), 100);
            Cost.Deposit(new Wood(), 200);
            Cost.Deposit(new Money(), 100);
            ChargeAMinute = new List<int>(5);
            ChargeTypes = new List<IResource>() { new Energy() };
            name = "Server Farm";
            Position = position;
            Size = new Vector2(3, 3);
            stats.Add(new Health("Health", 2000));
            healthBar = new HealthBar(new Rectangle(new Point((int)position.X, (int)position.Y - 1), new Point((int)(Size.X * 16), (int)(Size.Y))));
            BuildingDescription = "Used if a unit cap is implemented, at the moment this is also useless.";
        }

        public override Building NewInstace(TextureValue tex, Vector2 position, TextureValue Icon)
        {
            return new ServerFarm(tex, position, Icon, proj, teamStats);
        }
    }
}
