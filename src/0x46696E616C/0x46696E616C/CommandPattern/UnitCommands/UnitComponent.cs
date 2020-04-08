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
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace _0x46696E616C.CommandPattern
{
    internal class UnitComponent : BasicUnit, ICommandComponent
    {
        IEntity Target;
        Vector2 TargetPosition;

        public UnitComponent(Game game, string name, Vector2 size, float totalHealth, float currentHealth, Vector2 position, BaseUnitState state, TextureValue texture) : base(game, name, size, totalHealth, currentHealth, position, state, texture)
        {
            speed = 50;
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
            if (TargetPosition != position/(Tile.Zoom * 16))
            {
                Vector2 ActualTargetPos = TargetPosition / (Tile.Zoom * 16);
                Vector2 ActualPos = position / (Tile.Zoom * 16);
                float Distance = (float)Math.Sqrt(Math.Pow((ActualPos.X-ActualTargetPos.X),2)+ Math.Pow((ActualPos.Y - ActualTargetPos.Y), 2));

                Direction = new Vector2(Distance, 1);
                position += Direction * 5 * gameTime.ElapsedGameTime.Milliseconds/1000;
            }
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
