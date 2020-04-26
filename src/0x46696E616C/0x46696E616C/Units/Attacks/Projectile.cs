using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.Util.Collision;
using Microsoft.Xna.Framework;
using NationBuilder.DataHandlerLibrary;
using NationBuilder.TileHandlerLibrary;
using System;
using WorldManager.TileHandlerLibrary;

namespace _0x46696E616C.Units.Attacks
{
    internal class Projectile : ICollider
    {
        protected Vector2 direction;
        protected float speed;
        public Vector2 Position { get; protected set; }
        public TextureValue Texture { get; protected set; }
        protected Vector2 size;
        public float Scale { get; protected set; }
        public float Damage { get; protected set; }
        public BasicUnit Shooter { get; protected set; }
        public float Direction { get; protected set; }
        private float lifeTime;
        private float timer;
        public float LifeTime { get { return lifeTime / 1000; } }

        public Vector2 Size { get { return size; } }

        public Projectile(Vector2 EndPosition, float speed, Vector2 OriginPosition, TextureValue texture, float damage, BasicUnit shooter)
        {
            direction = new Vector2(EndPosition.X - OriginPosition.X, EndPosition.Y - OriginPosition.Y)+new Vector2(0.5f);
            direction.Normalize();
            this.speed = speed;
            this.Position = OriginPosition;
            Scale = 0.5f;
            size = new Vector2(Scale, Scale);
            this.Texture = texture;
            this.Shooter = shooter;
            this.Damage = damage;
            Direction = (float)Math.Asin(direction.Y / Math.Sqrt(Math.Pow(direction.X, 2) + (Math.Pow(direction.Y, 2))));//This should the the projectile direction
            lifeTime = Vector2.Distance(OriginPosition, EndPosition)/speed;
        }
        public void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer/1000 >= 1)
            {
                lifeTime -= timer / 1000;
                timer = 0;
            } 
            Position += direction * speed * gameTime.ElapsedGameTime.Milliseconds / 1000;
        }

        public void Collision(ICollider collider)
        {
            
        }

        internal void Hit()
        {
            lifeTime = 0;
        }
    }
}