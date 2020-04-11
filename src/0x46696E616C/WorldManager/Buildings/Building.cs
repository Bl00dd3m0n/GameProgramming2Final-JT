using System;
using NationBuilder.TileHandlerLibrary;
using WorldManager.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;
using MobHandler;
using _0x46696E616C.MobHandler;
using System.Collections.Generic;
using _0x46696E616C.CommandPattern.Commands;
using TechHandler;

namespace _0x46696E616C.Buildings
{
    public abstract class Building : ModifiableTile, IEntity, IQueueable<TextureValue>
    {
        /// <summary>
        /// This will probably get turned into a wallet class at some point however the name Cost will stay the same
        /// </summary>
        public Wallet Cost { get; protected set; }

        public int energyCost { get; protected set; }

        public List<IUnit> GarrisonedUnits { get; set; }
        public Queue<IQueueable<TextureValue>> trainingQueue { get; set; }
        public List<IQueueable <TextureValue>> QueueableThings { get; protected set; } 
        public TextureValue Icon { get; protected set; }
        public override Vector2 Position { get { return base.Position; } }
        Color teamColor; //Maybe implement this

        public Building(Game game, TextureValue texture, Vector2 position) : base(game, texture, position, Color.Blue)
        {
            Cost = new Wallet();
            name = "Building";
            Position = new Vector2(0, 0);
            Size = new Vector2(0, 0);
            TotalHealth = 0;
            CurrentHealth = 0;
            energyCost = 0;
            healthBar = new HealthBar(new Rectangle(this.Position.ToPoint() - new Point(0, (int)(this.Size.Y * 16 + 1)), Size.ToPoint()));
            GarrisonedUnits = new List<IUnit>();
            this.Icon = block.texture + 13;//if the texture values change this breaks it find a better way to do this
        }

        public void Construct()
        {
            foreach (IUnit unit in GarrisonedUnits)
            {
                if (unit is BasicUnit)
                {
                    CurrentHealth += ((BasicUnit)unit).BuildPower/60;
                }
                if (CurrentHealth > TotalHealth) CurrentHealth = TotalHealth;
            }
        }

        public override void Damage(float amount)
        {
            CurrentHealth -= amount;
        }

        public void Destroy()
        {
            
        }

        public override void Die()
        {
            throw new NotImplementedException();
        }
    }
}
