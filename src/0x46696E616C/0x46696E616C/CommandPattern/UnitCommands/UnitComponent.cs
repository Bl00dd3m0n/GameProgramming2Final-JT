using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.MobHandler;
using _0x46696E616C.MobHandler.Units;
using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldManager;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace _0x46696E616C.CommandPattern
{
    internal class UnitComponent : BasicUnit, ICommandComponent
    {
        IEntity Target;
        Vector2 TargetPosition;
        Vector2 zero;
        Vector2 xOne;
        Vector2 yOne;
        List<Vector2> waypoints;
        WorldHandler world;
        public UnitComponent(Game game, string name, Vector2 size, float totalHealth, float currentHealth, Vector2 position, BaseUnitState state, TextureValue texture, WorldHandler world) : base(game, name, size, totalHealth, currentHealth, position, state, texture)
        {
            zero = Vector2.Zero;
            xOne = new Vector2(1, 0);
            yOne = new Vector2(0, 1);
            speed = 50;
            this.world = world;
        }

        public void Build(IEntity target)
        {
            State = BaseUnitState.build;
        }

        public void Move(Vector2 Position)
        {
            TargetPosition = Position;
        }
        public void Attack(IEntity target)
        {
            if (target is IHarvestable) State = BaseUnitState.harvest;
            else State = BaseUnitState.attack;
        }
        public override void Update(GameTime gameTime)
        {
            UpdateMove(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// Probably implement some sort of A* and flocking ai either here or 
        /// </summary>
        ///<see cref="Probably implement some sort of A* and flocking ai either here or in the Command Component"/>>
        private void UpdateMove(GameTime gameTime)
        {
            Direction = zero;
            if (position.X < TargetPosition.X-0.5f)
                Direction += xOne;
            if (position.X > TargetPosition.X+0.5f)
                Direction -= xOne;
            if (position.Y < TargetPosition.Y-0.5f)
                Direction += yOne;
            if (position.Y > TargetPosition.Y+0.5f)
                Direction -= yOne;
                position += Direction * 5 * gameTime.ElapsedGameTime.Milliseconds/1000;
        }

        public void Garrison(IEntity Target)
        {
            throw new NotImplementedException();
        }

        public void Move(IEntity Target)
        {
            throw new NotImplementedException();
        }
        public void Harvest()
        {
            throw new NotImplementedException();
        }
    }
}
