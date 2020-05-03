using _0x46696E616C.Buildings;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.TechManager.Stats;
using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldManager;
using WorldManager.TileHandlerLibrary;

namespace _0x46696E616C.Units.AllyUnit
{
    public abstract class OffensiveUnits : BasicUnit
    {
        float timer;

        public OffensiveUnits(string name, Vector2 size, float totalHealth, float currentHealth, Vector2 position, BaseUnitState state, TextureValue texture, Color color, TextureValue icon, WorldHandler world, float range, Stats teamstats) : base(name, size, totalHealth, currentHealth, position, state, texture, color, icon, world, range, teamstats)
        {
            stats.Add(new MeleeDamage("Attack Power", 10));
        }

        public override void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;
            base.Update(gameTime);
            if (timer / 1000 >= 1)
            {
                UnitInteraction();
            }
            if (waypoints.Count() > 0 && Vector2.Distance(Position, TargetPosition + DistanceFromPosition) < stats[typeof(Range)].Value && UnitState == BaseUnitState.attack)
            {
                waypoints.Clear();
            }
            if (timer / 1000 >= 1)
            {
                if (Target != null && Target.Position.ToPoint() != TargetPosition.ToPoint()) // Keeps tracking the units
                {
                    Move(Target.Position);
                }
                timer = 0;
            }
        }

        /// <summary>
        /// Interact with each tile based on what type of tile it is if the unit is within range of their target.
        /// </summary>
        private void UnitInteraction()
        {
            if (Vector2.Distance(TargetPosition, Position) < 0.5f)
            {
                IEntity entity = world.GetUnits(TeamAssociation + 1).Find(l => Vector2.Distance(l.Position, Position) < stats[typeof(Range)].Value * 2 && l is ModifiableTile && ((ModifiableTile)l).TeamAssociation != this.TeamAssociation);
                if (entity == null)//Targets units first
                {
                    IEntity[] entities = world.GetTiles(TeamAssociation + 1);
                    if (entities != null)
                    {
                        //entity = entities.FirstOrDefault(l => Vector2.Distance(l.Position, Position) < stats[typeof(Range)].Value * 2 && l is ModifiableTile && ((ModifiableTile)l).TeamAssociation != this.TeamAssociation);
                    }
                }
                if (entity != null)//HACK this won't always work
                {
                    Attack(entity);
                }
            }
            float dist = Vector2.Distance(TargetPosition + DistanceFromPosition, nextPoint);
            if (Direction == zero && Target != null && dist <= stats[typeof(Range)].Value)
            {
                if (((ModifiableTile)Target).TeamAssociation != this.TeamAssociation)
                {
                    attack.Attack(Target, this, this.stats[typeof(MeleeDamage)].Value);
                    if (((ModifiableTile)Target).State == tileState.dead) Target = null;
                }
            }
        }
        /// <summary>
        /// Generates waypoints using A*
        /// </summary>
        /// <param name="Position"></param>
        public override void Move(Vector2 Position)
        {
            if (world.GetUnit(Position) == null && world.GetTile(Position) == null)
            {
                UnitState = BaseUnitState.Idle;
                Target = null;
            }
            base.Move(Position);
        }
        /// <summary>
        /// Attack the targetted unit
        /// </summary>
        /// <param name="target"></param>
        public void Attack(IEntity target)
        {
            ResetUnit();
            this.Target = target;
            Move(Target.Position);
            UnitState = BaseUnitState.attack;
        }

        /// <summary>
        /// Garrison in the targetted building
        /// </summary>
        /// <param name="target"></param>
        public void Garrison(IEntity target)
        {
            ResetUnit();
            this.Target = target;
            Move(Target.Position);
        }

        /// <summary>
        /// Ungarisons unit...more functionality might be added
        /// </summary>
        private void ResetUnit()
        {
            if (this.Target is Building)
            {
                if (((Building)this.Target).GarrisonedUnits.Contains(this))
                {
                    ((Building)this.Target).GarrisonedUnits.Remove(this);
                }
            }
        }
        /// <summary>
        /// Returns a new unit based on the position
        /// </summary>
        /// <param name="currentHealth"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public override BasicUnit NewInstace(float currentHealth, Vector2 position)
        {
            throw new NotImplementedException();
        }

    }
}
