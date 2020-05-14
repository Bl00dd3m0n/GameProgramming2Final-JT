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
        float checkPosTimer;
        float InteractTimer;
        private Color color;

        public HostileMob(string name, Vector2 size, float totalHealth, float currentHealth, Vector2 position, BaseUnitState state, TextureValue texture, Color color, TextureValue icon, float range, Stats teamStats) : base(name, size, totalHealth, currentHealth, position, state, texture, color, icon, range, teamStats)
        {
            waypoints = new List<Vector2>();
            speed = 50;
            stats.Add(new MeleeDamage("Attack", 3));
            tags.Add("CanAttack");
        }

        public override void Update(GameTime gameTime, WorldHandler world)
        {
            UpdateMove(gameTime, world);
            InteractTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (InteractTimer / 1000 >= 1)
            {
                Interact(world);
                InteractTimer = 0;
            }
            base.Update();
        }
        public void Interact(WorldHandler world)
        {
            if (Target != null)
            {
                if (Vector2.Distance(Position, Target.Position + DistanceFromPosition) < stats[typeof(Range)].Value+ teamStats[typeof(Range)].Value && UnitState == BaseUnitState.attack)
                {
                    attack.Attack(Target, this, stats[typeof(MeleeDamage)].Value+teamStats[typeof(MeleeDamage)].Value);
                    if (((ModifiableTile)Target).State == tileState.dead)
                    {
                        Target = null;
                        FindTarget(world);
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
        protected override void UpdateMove(GameTime gameTime, WorldHandler world)
        {
            base.UpdateMove(gameTime, world);
            checkPosTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (waypoints.Count() > 0 && Vector2.Distance(Position, TargetPosition + DistanceFromPosition) < stats[typeof(Range)].Value + teamStats[typeof(Range)].Value && UnitState == BaseUnitState.attack)
            {
                waypoints.Clear();
            }
            if (checkPosTimer / 1000 >= 1f)
            {
                if (Target == null || (Target!= null && TargetPosition != Target.Position) && (Vector2.Distance(TargetPosition, Position) < 10))//A* for far distances causes extreme strain on the game limiting target updating distance to allow the game to scale a bit more.
                {
                    FindTarget(world);
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
        public override void Move(Vector2 Position, WorldHandler world)
        {
            if (world.GetUnit(Position) == null && world.GetTile(Position) == null)
            {
                UnitState = BaseUnitState.Idle;
                Target = null;
            }
            base.Move(Position, world);
        }
        protected void Attack(IEntity entity, WorldHandler world)
        {
            Target = entity;
            UnitState = BaseUnitState.attack;
            Move(entity.Position, world);
        }
        public void FindTarget(WorldHandler world)
        {
            IEntity entity = world.FindNearest(this.TeamAssociation - 1, this.Position);
            if (entity != null)
            {
                if (entity is ModifiableTile)
                {
                    Attack(entity, world);
                }
            }
        }
    }
}
