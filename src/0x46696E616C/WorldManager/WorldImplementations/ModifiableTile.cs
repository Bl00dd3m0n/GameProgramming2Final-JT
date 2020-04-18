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
using WorldManager.MapData;

namespace WorldManager.TileHandlerLibrary
{
    public enum tileState { whole, damaged, dead }
    public abstract class ModifiableTile : Tile, IEntity
    {
        public tileState State { get; private set; }

        public string name { get; protected set; }

        public Vector2 Size { get; protected set; }

        public float TotalHealth { get; protected set; }

        protected float currentHealth;
        /// <summary>
        /// returns the current health and can only be set if the health is less than or equal to the total health
        /// </summary>
        public float CurrentHealth {
            get
            {
                return currentHealth;
            }
            set
            {
                if(CurrentHealth <= TotalHealth)//Doesn't let you build past full health
                {
                    currentHealth = value;
                }
                else if(currentHealth >= TotalHealth && !built)
                {
                    built = true;
                }
            }
        }

        public int TeamAssociation { get; protected set; }

        public HealthBar healthBar { get; protected set; }

        List<IMapObserver> MapWatcher;

        protected List<string> tags;

        public bool built { get; protected set; }

        public ModifiableTile(Game game, TextureValue texture, Vector2 position, Color color) : base(game, texture, position, color)
        {
            built = false;
            MapWatcher = new List<IMapObserver>();
            healthBar = new HealthBar(new Rectangle(this.Position.ToPoint()-new Point(0,(int)(this.Size.Y*16+1)), Size.ToPoint()));
            tags = new List<string>();
        }

        public virtual void SetTeam(int team)
        {
            TeamAssociation = team;
        }


        public virtual void Subscribe(IMapObserver map)
        {
            MapWatcher.Add(map);
        }

        public virtual void UnSubscribe(IMapObserver map)
        {
            MapWatcher.Add(map);
        }
        public virtual void Update()
        {

        }
        public virtual void Damage(float value)
        {
            CurrentHealth -= value;
            if(CurrentHealth <= 0)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            State = tileState.dead;
            foreach(IMapObserver map in MapWatcher)
            {
                map.Update(this);
            }
        }
        /// <summary>
        /// Don't update harvestable units
        /// </summary>
        /// <param name="position"></param>
        public override void UpdatePosition(Vector2 position)
        {
            if (this is IHarvestable)
            {
            }
            else
            {
                base.UpdatePosition(position);
                healthBar = new HealthBar(new Rectangle(new Point((int)position.X, (int)position.Y - 1), new Point((int)(Size.X * 16), (int)(5))));
                healthBar.UpdateHealth(this, Game.GraphicsDevice);
                healthBar.Position = position;
            }
        }
        //checks if the tile has the tag
        public bool HasTag(string v)
        {
            if (tags.Contains(v)) return true;
            return false;
        }
    }
}
