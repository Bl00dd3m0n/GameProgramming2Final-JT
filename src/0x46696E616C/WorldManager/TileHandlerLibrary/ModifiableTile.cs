using _0x46696E616C;
using _0x46696E616C.Units;
using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;
//using MyVector2 = NationBuilder.TileHandlerLibrary.Vector2;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.MobHandler;

namespace WorldManager.TileHandlerLibrary
{
    public abstract class ModifiableTile : Tile, IEntity
    {
        public string name { get; protected set; }

        public Vector2 Position { get { return position; } protected set { position = value; } }

        public Vector2 Size { get; protected set; }

        public float TotalHealth { get; protected set; }

        public float CurrentHealth { get; protected set; }

        public HealthBar healthBar { get; protected set; }

        public ModifiableTile(Game game, TextureValue texture, Vector2 position) : base(game, texture, position)
        {
            healthBar = new HealthBar(new Rectangle(this.position.ToPoint()-new Point(0,(int)(this.Size.Y*16+1)), Size.ToPoint()));
        }

        public virtual void Damage(float value)
        {
            throw new NotImplementedException();
        }

        public virtual void Die()
        {
            throw new NotImplementedException();
        }

        public void QueueBuild()
        {
            throw new NotImplementedException();
        }
    }
}
