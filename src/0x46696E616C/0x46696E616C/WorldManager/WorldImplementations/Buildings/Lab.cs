using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Microsoft.Xna.Framework;
using _0x46696E616C.MobHandler;
using _0x46696E616C.ConcreteImplementations.Resources;
using _0x46696E616C.ConcreteImplementations;
using _0x46696E616C.WorldManager.WorldImplementations.Buildings;
using System.Collections.Generic;
using _0x46696E616C.WorldManager.ConcreteImplementations.Resources;
using TechHandler;
using WorldManager;
using _0x46696E616C.TechManager.Stats;
using _0x46696E616C.TechManager.Technologies;

namespace _0x46696E616C.Buildings
{
    public class Lab : Building, IResourceCharge
    {
        public List<int> ChargeAMinute { get; protected set; }

        public List<IResource> ChargeTypes { get; protected set; } 

        public Lab(TextureValue texture, Vector2 position, TextureValue icon) : base(texture, position, icon)
        {
            QueueableThings = new List<IQueueable<TextureValue>>();
            QueueableThings.Add(new DamageUpgrade(new AttackPower("Power", 10), TextureValue.Damage, new Vector2()));
            Cost = new Wallet();
            Cost.Deposit(new Steel(), 1000);
            Cost.Deposit(new Wood(), 2000);
            Cost.Deposit(new Money(), 1000);
            ChargeAMinute = new List<int>() { 100 };
            ChargeTypes = new List<IResource>() { new Energy() };
            name = "Lab";
            Position = position;
            Size = new Vector2(2, 2);
            stats.Add(new Health("Health", 1000));
            CurrentHealth = 0;
            healthBar = new HealthBar(new Rectangle(new Point((int)position.X, (int)position.Y - 1), new Point((int)(Size.X * 16), (int)(Size.Y))));
            BuildingDescription = "Used to learn technology to improve production.";
        }
        public override Building NewInstace(TextureValue tex, Vector2 position, TextureValue Icon)
        {
            return new Lab(tex, position, Icon);
        }

        public void Learn(ITech tech)
        {
            foreach(ITechObserver observer in techObservers)
            {
                observer.Update(tech);
            }
        }
    }
}
