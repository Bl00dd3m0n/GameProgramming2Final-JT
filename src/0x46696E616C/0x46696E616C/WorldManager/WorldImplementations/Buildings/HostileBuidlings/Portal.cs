using _0x46696E616C.Buildings;
using _0x46696E616C.MobHandler;
using _0x46696E616C.TechManager.Stats;
using _0x46696E616C.Units.Attacks;
using _0x46696E616C.WorldManager.ConcreteImplementations.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MobHandler.HostileMobManager;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechHandler;
using WorldManager;
using WorldManager.MapData;

namespace _0x46696E616C.WorldManager.WorldImplementations.Buildings.HostileBuidlings
{
    class Portal : Building
    {
        WaveManager wave;
        public Portal(TextureValue texture, Vector2 position, TextureValue icon, WorldHandler world, ProjectileManager proj, Stats stats, WaveManager wave) : base(texture, position, icon, world, proj, stats)
        {
            energyCost = 0;
            name = "Portal";
            Position = position;
            Size = new Vector2(4, 4);
            stats.Add(new Health("Health", 5000));
            currentHealth = 100000;
            tags.Add("Portal");
            healthBar = new HealthBar(new Rectangle(new Point((int)position.X, (int)position.Y - 1), new Point((int)(Size.X * 16), (int)(Size.Y))));
            BuildingDescription = "The Portal summons waves";
            this.wave = wave;
        }

        public override Building NewInstace(TextureValue tex, Vector2 position, TextureValue Icon)
        {
            return new Portal(tex, position, Icon, world,proj,stats, wave);
        }

        public override void AddQueueable(IQueueable<TextureValue> item)
        {
            base.AddQueueable(item);
        }

        public override void Damage(float amount)
        {
            if (!wave.StartSpawn) wave.StartSpawn = true;
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
