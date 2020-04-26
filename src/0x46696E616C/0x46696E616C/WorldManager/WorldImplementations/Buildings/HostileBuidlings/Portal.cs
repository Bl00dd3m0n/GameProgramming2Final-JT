using _0x46696E616C.Buildings;
using _0x46696E616C.MobHandler;
using _0x46696E616C.TechManager.Stats;
using _0x46696E616C.WorldManager.ConcreteImplementations.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechHandler;
using WorldManager.MapData;

namespace _0x46696E616C.WorldManager.WorldImplementations.Buildings.HostileBuidlings
{
    class Portal : Building
    {
        public Portal( TextureValue texture, Vector2 position, TextureValue Icon) : base(texture, position, Icon)
        {
            energyCost = 0;
            name = "Portal";
            Position = position;
            Size = new Vector2(4, 4);
            stats.Add(new Health("Health", 5000));
            CurrentHealth = 5000;
            tags.Add("Portal");
            healthBar = new HealthBar(new Rectangle(new Point((int)position.X, (int)position.Y - 1), new Point((int)(Size.X * 16), (int)(Size.Y))));
            BuildingDescription = "The Portal summons waves";
        }

        public override Building NewInstace( TextureValue tex, Vector2 position, TextureValue Icon)
        {
            return new Portal(tex, position, Icon);
        }

        public override void AddQueueable(IQueueable<TextureValue> item)
        {
            base.AddQueueable(item);
        }

        public override void Damage(float amount)
        {
            base.Damage(amount);
            //TODO implement destruction a way to destroy it....requires more than just flat damage
        }

        public override void Die()
        {
            base.Die();
        }

        public override void SetTeam(int team)
        {
            base.SetTeam(team);
        }

        public override void UpdatePosition(GraphicsDevice gd, Vector2 position)
        {
            base.UpdatePosition(gd, position);
        }
    }
}
