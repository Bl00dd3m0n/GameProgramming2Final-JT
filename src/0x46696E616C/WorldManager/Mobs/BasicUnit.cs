using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.Units;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldManager.TileHandlerLibrary;

namespace _0x46696E616C.CommandPattern.Commands
{
    public class BasicUnit : ModifiableTile, IUnit
    {
        enum UnitState { Build, Attack, Flee, Idle }

        public BaseUnitState State { get; protected set; }
        protected Vector2 Direction { get; set; }
        protected float speed { get; set; }
        //TODO List of commands needed to be implemented for the units

        public BasicUnit(Game game, string name, Vector2 size, float totalHealth, float currentHealth, Vector2 position, BaseUnitState state, NationBuilder.TileHandlerLibrary.TextureValue texture) : base(game, texture, position)
        {
            this.name = name;
            this.Size = size;
            this.TotalHealth = totalHealth;
            this.CurrentHealth = currentHealth;
            this.Position = position;
            this.State = state;
            Direction = new Vector2(0, 0);
            speed = 0;
        }
    }
}
