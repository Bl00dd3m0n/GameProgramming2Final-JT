using _0x46696E616C.Buildings;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.Units;
using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechHandler;
using WorldManager.TileHandlerLibrary;

namespace _0x46696E616C.CommandPattern.Commands
{
    public class BasicUnit : ModifiableTile, IUnit, ITechObserver, IQueueable<TextureValue>
    {
        enum UnitState { Build, Attack, Flee, Idle }

        public BaseUnitState State { get; protected set; }
        protected Vector2 Direction { get; set; }
        protected float speed { get; set; }
        public float BuildPower { get; protected set; }
        public float HarvestPower { get; protected set; }
        public float AttackPower { get; protected set; }
        public List<IQueueable<TextureValue>> QueueableThings { get; protected set; }
        public Stack<Building> toBuild { get; protected set; }

        public TextureValue Icon { get; protected set; }

        //TODO List of commands needed to be implemented for the units
        public BasicUnit(Game game, string name, Vector2 size, float totalHealth, float currentHealth, Vector2 position, BaseUnitState state, TextureValue texture, Color color, TextureValue icon) : base(game, texture, position, color)
        {
            this.name = name;
            this.Size = size;
            this.TotalHealth = totalHealth;
            this.CurrentHealth = currentHealth;
            this.Position = position;
            this.State = state;
            this.BuildPower = 10;
            this.HarvestPower = 1;
            this.AttackPower = 1;
            Direction = new Vector2(0, 0);
            speed = 0;
            this.Icon = icon;
            toBuild = new Stack<Building>();
        }

        public virtual void QueueBuild(IEntity building)
        {
            throw new NotImplementedException();
        }

        public override void UpdatePosition(Vector2 position)
        {
            base.UpdatePosition(position);
        }

        public virtual BasicUnit NewInstace(float currentHealth, Vector2 position)
        {
            return new BasicUnit(this.Game, this.name, this.Size,this.TotalHealth, currentHealth, position, BaseUnitState.Idle, this.block.texture, this.tileColor, this.Icon);
        }
        

        public void Update(ITech tech)
        {
            throw new NotImplementedException();
        }
    }
}
