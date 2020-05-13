using _0x46696E616C.Buildings;
using _0x46696E616C.CommandPattern;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.Util.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NationBuilder.DataHandlerLibrary;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using WorldManager;
using WorldManager.TileHandlerLibrary;

namespace _0x46696E616C.Units.Attacks
{
    public class ProjectileManager : DrawableGameComponent
    {
        List<Projectile> projectiles;
        List<Projectile> HitProjectiles;
        Camera cam;
        CollisionHandler collision;
        public ProjectileManager(Game game, Camera cam, CollisionHandler collision) : base(game)
        {
            projectiles = new List<Projectile>();
            HitProjectiles = new List<Projectile>();
            this.collision = collision;
            this.cam = cam;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (Projectile projectile in projectiles)
            {
                spriteBatch.Draw(ContentHandler.DrawnTexture(projectile.Texture), projectile.Position * Tile.Zoom * 16 - cam.Position * Tile.Zoom * 16, null, Color.White, projectile.Direction, new Vector2(), projectile.Scale, SpriteEffects.None, 0);
            }
            spriteBatch.End();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update(gameTime);
                if (projectiles[i].LifeTime <= 0)
                {
                    projectiles.Remove(projectiles[i]);
                    i--;
                }
            }
            HitProjectiles.Clear();
            base.Update(gameTime);
        }

        internal void Clear()
        {
            projectiles.Clear();
        }

        public void AddProjectile(Projectile projectile)
        {
            projectiles.Add(projectile);
            collision.AddCollider(projectile);
        }
    }
}
