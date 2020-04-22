using _0x46696E616C.AstarAlgorithm;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.MobHandler.Units;
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
        List<Vector2> waypoints;
        IEntity Target;
        WorldHandler world;
        public HostileMob(string name, Vector2 size, float totalHealth, float currentHealth, Vector2 position, BaseUnitState state, TextureValue texture, Color color, TextureValue icon, WorldHandler world) : base(name, size, totalHealth, currentHealth, position, state, texture, color, icon, world)
        {
            waypoints = new List<Vector2>();
            speed = 50;
            this.world = world;
            AttackPower = 100;
        }
        public void Update(GameTime gameTime)
        {
            UpdateMove(gameTime);
            Interact();
            if (Keyboard.GetState().IsKeyDown(Keys.U))
            {
                FindTarget();
            }
            base.Update();
        }
        public void Interact()
        {
            if (Target != null)
            {
                if (TargetPosition.ToPoint() == Position.ToPoint() && UnitState == BaseUnitState.attack)
                {
                    Target.Damage(this.AttackPower);
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
