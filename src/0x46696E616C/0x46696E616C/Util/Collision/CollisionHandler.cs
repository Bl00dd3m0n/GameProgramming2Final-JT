using _0x46696E616C.Buildings;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.Units.Attacks;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldManager;
using WorldManager.TileHandlerLibrary;

namespace _0x46696E616C.Util.Collision
{
    public class CollisionHandler : GameComponent
    {
        List<ICollider> colliders { get; set; }
        List<ICollider> deletedColliders;
        WorldHandler world;
        float timer;
        public CollisionHandler(Game game, WorldHandler world) : base(game)
        {
            this.world = world;
            colliders = new List<ICollider>();
        }
        public override void Update(GameTime gameTime)
        {
            deletedColliders = new List<ICollider>();
            timer += gameTime.ElapsedGameTime.Milliseconds;
            foreach (ICollider collider in colliders)
            {
                Colliding(collider);
                if(collider is Projectile)
                {
                    if (((Projectile)collider).LifeTime <= 0)
                        deletedColliders.Add(collider);
                } else if(collider is ModifiableTile)
                {
                    if (((ModifiableTile)collider).CurrentHealth < 0) deletedColliders.Add(collider);
                }
            }
            foreach (ICollider collider in deletedColliders)
            {
                colliders.Remove(collider);
            }
            deletedColliders.Clear();
            base.Update(gameTime);
        }
        public void Colliding(ICollider checkingCollider)
        {
            if (timer / 1000 >= 1f)
            {
                timer = 0;
                foreach (ICollider collider in colliders)
                {
                    if (collider != checkingCollider)
                    {
                        if (CheckCollision(checkingCollider, collider))
                        {
                            checkingCollider.Collision(collider);
                            deletedColliders.Add(collider);
                            if (collider is Projectile && checkingCollider is ModifiableTile)
                            {
                                if (((Projectile)collider).Shooter.TeamAssociation != ((ModifiableTile)checkingCollider).TeamAssociation)
                                {
                                    deletedColliders.Add(collider);
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool CheckCollision(ICollider checkingCollider, ICollider collider)
        {
            if(checkingCollider.Position.X <= collider.Position.X && checkingCollider.Position.X+checkingCollider.Size.X >= collider.Position.X && checkingCollider.Position.Y <= collider.Position.Y && checkingCollider.Position.Y +checkingCollider.Size.Y >= collider.Position.Y)
            {
                return true;
            }
            return false;
        }

        public void AddCollider(ICollider collider)
        {
            colliders.Add(collider);
        }
    }
}
