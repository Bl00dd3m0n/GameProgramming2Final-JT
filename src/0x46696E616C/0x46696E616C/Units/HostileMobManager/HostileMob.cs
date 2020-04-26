using _0x46696E616C.AstarAlgorithm;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.TechManager.Stats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldManager;
using WorldManager.TileHandlerLibrary;

namespace _0x46696E616C.Units.HostileMobManager
{
    class HostileMob : BasicUnit
    {
        IEntity Target;
        WorldHandler world;
        float checkPosTimer;
        float InteractTimer;
        public HostileMob(string name, Vector2 size, float totalHealth, float currentHealth, Vector2 position, BaseUnitState state, TextureValue texture, Color color, TextureValue icon, WorldHandler world, float range) : base(name, size, totalHealth, currentHealth, position, state, texture, color, icon, world, range)
        {
            waypoints = new List<Vector2>();
            speed = 50;
            this.world = world;
            stats.Add(new AttackPower("Attack", 100));
            tags.Add("CanAttack");
        }
        public override void Update(GameTime gameTime)
        {
            UpdateMove(gameTime);
            InteractTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (InteractTimer/1000 >= 1)
            {
                Interact();
                InteractTimer = 0;
            }
            base.Update();
        }
        public void Interact()
        {
            if (Target != null)
            {
                if (Vector2.Distance(Target.Position.ToPoint().ToVector2() - new Vector2(1, 0), Position.ToPoint().ToVector2()) < stats[typeof(Range)].Value && UnitState == BaseUnitState.attack)
                {
                    attack.Attack(Target, this, stats[typeof(AttackPower)].Value);
                    if (((ModifiableTile)Target).State == tileState.dead)
                    {
                        Target = null;
                        FindTarget();
                    }
                }
            }
        }
        public override void Damage(float value)
        {
            base.Damage(value);
        }
        /// <summary>
        /// Moves by waypoint
        /// </summary>
        ///<see cref="Probably implement some sort of A* and flocking ai either here or in the Command Component"/>>
        protected override void UpdateMove(GameTime gameTime)
        {
            base.UpdateMove(gameTime);
            checkPosTimer += gameTime.ElapsedGameTime.Milliseconds;
            if(waypoints.Count() > 0 && Vector2.Distance(Position,Target.Position) < range)
            {
                waypoints.Clear();
            }
            if(checkPosTimer/1000 >= 1)
            {
                if(Target != null && Target.Position.ToPoint() - new Point(1,0) != TargetPosition.ToPoint())
                {
                    //Move(Target.Position);
                } else if(Target == null)
                {
                    FindTarget();
                }
                checkPosTimer = 0;
            }
        }
        /// <summary>
        /// if the unit has hit a waypoint go to the next one
        /// </summary>
        protected override void WayPointFollower()
        {
            base.WayPointFollower();
        }
        /// <summary>
        /// Generates waypoints using A*
        /// </summary>
        /// <param name="Position"></param>
        public override void Move(Vector2 Position)
        {
            base.Move(Position);
        }
        protected void Attack(IEntity entity)
        {
            Target = entity;
            UnitState = BaseUnitState.attack;
            Move(entity.Position);
        }
        public void FindTarget()
        {
            IEntity entity = world.FindNearest(this.TeamAssociation - 1, this.Position);
            if (entity != null)
            {
                if (entity is ModifiableTile)
                {
                    Attack(entity);
                }
            }
        }
    }
}
