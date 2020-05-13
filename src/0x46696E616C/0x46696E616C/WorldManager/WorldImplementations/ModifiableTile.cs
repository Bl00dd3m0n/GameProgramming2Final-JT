using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using System.Collections.Generic;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.MobHandler;
using WorldManager.MapData;
using Microsoft.Xna.Framework.Graphics;
using _0x46696E616C.TechManager.Stats;
using _0x46696E616C.Util.Collision;
using _0x46696E616C.Units.Attacks;

namespace WorldManager.TileHandlerLibrary
{
    public enum tileState { whole, damaged, dead }
    public abstract class ModifiableTile : Tile, IEntity, ICollider
    {
        public tileState State { get; private set; }

        public string name { get; protected set; }

        public Vector2 Size { get; protected set; }

        public float TotalHealth { get { return stats[typeof(Health)].Value; } }

        protected float currentHealth;

        public Stats stats { get; protected set; }

        /// <summary>
        /// returns the current health and can only be set if the health is less than or equal to the total health
        /// </summary>
        public float CurrentHealth
        {
            get
            {
                return currentHealth;
            }
            set
            {
                if (CurrentHealth < TotalHealth || value < CurrentHealth)//Doesn't let you build past full health
                {
                    currentHealth = value;
                    if (currentHealth >= TotalHealth && !built)
                    {
                        built = true;
                    }
                }

            }
        }

        public int TeamAssociation { get; protected set; }

        public HealthBar healthBar { get; protected set; }

        List<IMapObserver> MapWatcher;

        protected List<string> tags;

        public bool built { get; protected set; }
        public Stats TeamStats { get; private set; }

        public ModifiableTile(TextureValue texture, Vector2 position, Stats teamStats, Color color) : base(texture, position, color)
        {
            stats = new Stats();
            this.TeamStats = teamStats;
            built = false;
            MapWatcher = new List<IMapObserver>();
            healthBar = new HealthBar(new Rectangle(this.Position.ToPoint() - new Point(0, (int)(this.Size.Y * 16 + 1)), Size.ToPoint()));
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
            if (this is ReferenceTile)
            {
                ((ReferenceTile)this).tile.Damage(value);
            }
            else
            {
                CurrentHealth -= value;
                if (CurrentHealth <= 0)
                {
                    Die();
                }
            }
        }

        public virtual void Die()
        {
            State = tileState.dead;
            foreach (IMapObserver map in MapWatcher)
            {
                map.Update(this);
                if (map is ReferenceTile)
                {
                    ((ReferenceTile)map).Die();
                }
            }
            MapWatcher.RemoveAll(l => l is ReferenceTile && ((ReferenceTile)l).State == tileState.dead);
        }
        /// <summary>
        /// Don't update harvestable units
        /// </summary>
        /// <param name="position"></param>
        public override void UpdatePosition(GraphicsDevice gd, Vector2 position)
        {
            if (this is IHarvestable)
            {
            }
            else
            {
                if (!placed)
                {
                    base.UpdatePosition(gd, position);
                    if (healthBar.Health == null)
                    {
                        healthBar = new HealthBar(new Rectangle(new Point((int)position.X, (int)position.Y - 1), new Point((int)(Size.X * 16), (int)(5))));

                    }
                    healthBar.Position = position;
                }

            }
            if(healthBar != null)
            {
                healthBar.UpdateHealth(this, gd);
            }
        }
        public override Tile PlacedTile(GraphicsDevice gd)
        {
            if (healthBar.Health == null)
            {
                healthBar = new HealthBar(new Rectangle(new Point((int)Position.X, (int)Position.Y - 1), new Point((int)(Size.X * 16), (int)(5))));
            }
            healthBar.UpdateHealth(this, gd);
            healthBar.Position = Position;
            return base.PlacedTile(gd);
        }
        //checks if the tile has the tag
        public bool HasTag(string v)
        {
            if (tags.Contains(v)) return true;
            return false;
        }

        public void Collision(ICollider tile)
        {
            if (tile is Projectile)
            {
                if (((Projectile)tile).Shooter.TeamAssociation != this.TeamAssociation)
                {
                    Damage(((Projectile)tile).Damage);
                    ((Projectile)tile).Hit();
                }
            }
        }
    }
}
