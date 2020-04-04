using _0x46696E616C.CommandPattern.Commands;
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

        public UnitComponent(Game game, string name, Vector2 size, float totalHealth, float currentHealth, Vector2 position, BaseUnitState state, TextureValue texture) : base(game, name, size, totalHealth, currentHealth, position, state, texture)
        {
            speed = 50;
        }

        public void Build(IEntity target)
        {
            
        }

        public void Move(Vector2 Position)
        {
            
        }
        public void Attack(IEntity target)
        {
            
        }
        public override void Update(GameTime gameTime)
        {
            UpdateMove();
            base.Update(gameTime);
        }

        private void UpdateMove()
        {
            position += Direction * 2;
            throw new NotImplementedException();
        }

        public void Garrison(IEntity Target)
        {
            throw new NotImplementedException();
        }


    }
}
