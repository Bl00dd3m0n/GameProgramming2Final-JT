using _0x46696E616C.AstarAlgorithm;
using _0x46696E616C.Buildings;
using _0x46696E616C.ConcreteImplementations;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.TechManager.Stats;
using _0x46696E616C.Units;
using _0x46696E616C.Units.Attacks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechHandler;
using WorldManager;
using WorldManager.TileHandlerLibrary;

namespace _0x46696E616C.CommandPattern.Commands
{
    public class BasicUnit : ModifiableTile, IUnit, ITechObserver, IQueueable<TextureValue>
    {
        public BaseUnitState UnitState { get; protected set; }
        protected Vector2 Direction
        {
            get;
            set;
        }
        protected float speed { get; set; }
        public List<IQueueable<TextureValue>> QueueableThings { get; protected set; }
        public Stack<Building> toBuild { get; protected set; }

        public TextureValue Icon { get; protected set; }

        public Wallet Cost { get; protected set; }

        public string Description { get; protected set; }
        protected AttackType attack;
        protected List<Vector2> waypoints;
        protected Vector2 TargetPosition;
        protected Vector2 nextPoint;
        protected Vector2 zero;
        protected Vector2 xOne;
        protected Vector2 yOne;
        A_Star aStar;
        WorldHandler world;
        protected float range;
        //TODO List of commands needed to be implemented for the units
        public BasicUnit(string name, Vector2 size, float totalHealth, float currentHealth, Vector2 position, BaseUnitState state, TextureValue texture, Color color, TextureValue icon, WorldHandler world, float range) : base(texture, position, color)
        {
            Cost = new Wallet();// TODO charge for units
            this.name = name;
            this.Size = size;
            stats.Add(new Health("Health", totalHealth));
            stats.Add(new Range("Range", 1.1f));
            this.CurrentHealth = currentHealth;
            this.Position = position;
            this.UnitState = state;
            Direction = new Vector2(0, 0);
            speed = 0;
            this.Icon = icon;
            toBuild = new Stack<Building>();
            this.world = world;
            aStar = new A_Star();
            waypoints = new List<Vector2>();
            zero = Vector2.Zero;
            xOne = new Vector2(1, 0);
            yOne = new Vector2(0, 1);
            speed = 50;
            UnitState = BaseUnitState.Idle;
            attack = new Melee(range);
            this.range = range;
        }

        public override void UpdatePosition(GraphicsDevice gd, Vector2 position)
        {
            base.UpdatePosition(gd,position);
        }

        public virtual BasicUnit NewInstace(float currentHealth, Vector2 position)
        {
            return new BasicUnit(this.name, this.Size, this.TotalHealth, currentHealth, position, BaseUnitState.Idle, this.block.texture, this.tileColor, this.Icon, world, this.range);
        }


        public void Update(ITech tech)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(GameTime gameTime)
        {
            UpdateMove(gameTime);
        }

        /// <summary>
        /// Moves by waypoint
        /// </summary>
        ///<see cref="Probably implement some sort of A* and flocking ai either here or in the Command Component"/>>
        protected virtual void UpdateMove(GameTime gameTime)
        {
            Direction = zero;
            float distance = Vector2.Distance(Position, nextPoint);
            if (Position.X <= nextPoint.X && distance > 0.5f)
            {
                Direction += xOne;
                this.Direction = Direction;
            }
            if (Position.X >= nextPoint.X && distance > 0.5f)
            {
                Direction -= xOne;
                this.Direction = Direction;
            }
            if (Position.Y <= nextPoint.Y && distance > 0.5f)
            {
                Direction += yOne;
                this.Direction = Direction;
            }
            if (Position.Y >= nextPoint.Y && distance > 0.5f)
            {
                Direction -= yOne;
                this.Direction = Direction;
            }
            Vector2 TempPosition = Position + (Direction * 5 * gameTime.ElapsedGameTime.Milliseconds / 1000);

            if (world.GetUnit(TempPosition) != null && world.GetUnit(TempPosition) != this)
            {
                Move(TargetPosition);
            }
            else if(Direction != zero)
            {
                Position = TempPosition;
            }
            WayPointFollower();
        }
        /// <summary>
        /// if the unit has hit a waypoint go to the next one
        /// </summary>
        protected virtual void WayPointFollower()
        {
            if (Direction == zero && waypoints.Count > 1)
            {
                waypoints.Remove(waypoints[0]);
                nextPoint = waypoints[0];
            }
        }
        /// <summary>
        /// Generates waypoints using A*
        /// </summary>
        /// <param name="Position"></param>
        public virtual void Move(Vector2 Position)
        {
            Position -= new Vector2(1, 0);
            waypoints.Clear();
            waypoints = aStar.FindPath(this.Position, Position, world, waypoints);
            if (waypoints.Count > 0)
            {
                nextPoint = waypoints[0];
            }
            TargetPosition = Position;
        }
    }
}
