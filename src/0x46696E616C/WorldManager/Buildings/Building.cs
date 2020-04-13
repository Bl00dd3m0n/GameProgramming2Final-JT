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
using WorldManager.MapData;
using WorldManager.Buildings;
using System.Collections;

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
        public List<IQueueable<TextureValue>> QueueableThings { get; protected set; }
        public TextureValue Icon { get; protected set; }
        public override Vector2 Position { get { return base.Position; } }
        Color teamColor; //Maybe implement this
        IQueueable<TextureValue> trainingObject;
        public IBuildingObserver worldComponent { get; protected set; }

        Vector2 spawnPoint { get; set; }

        public Building(Game game, TextureValue texture, Vector2 position, TextureValue Icon) : base(game, texture, position, Color.Blue)
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
            this.Icon = Icon;//if the texture values change this breaks it find a better way to do this
            QueueableThings = new List<IQueueable<TextureValue>>();
            trainingQueue = new Queue<IQueueable<TextureValue>>();
        }

        public void Subscribe(IBuildingObserver observer)
        {
            worldComponent = observer;
        }

        public IQueueable<TextureValue> Train()
        {
            if (trainingQueue.Count > 0)
            {
                trainingObject = trainingQueue.Peek();
                if (((IEntity)trainingObject).CurrentHealth >= ((IEntity)trainingObject).TotalHealth)
                {
                    if (trainingObject is BasicUnit)
                    {
                        ((BasicUnit)trainingObject).UpdatePosition(spawnPoint);
                        ((BasicUnit)trainingObject).PlacedTile();
                    }
                    return trainingQueue.Dequeue();
                }
                CurrentHealth += 1f;//this probably should be updated too
            }
            return null;
        }

        public void Construct()
        {
            foreach (IUnit unit in GarrisonedUnits)
            {
                if (unit is BasicUnit)
                {
                    CurrentHealth += ((BasicUnit)unit).BuildPower;
                }
                if (CurrentHealth > TotalHealth)
                    CurrentHealth = TotalHealth;
            }
        }

        public virtual void AddQueueable(IQueueable<TextureValue> item)
        {
            QueueableThings.Add(item);//workaround for not being able to have UnitComponent
        }

        public override void Damage(float amount)
        {
            CurrentHealth -= amount;
        }

        public void Destroy()
        {

        }
        public override void UpdatePosition(Vector2 position)
        {
            spawnPoint = position - (new Vector2(0, -1) * this.Size);
            base.UpdatePosition(position);
        }
        public override void Die()
        {
            throw new NotImplementedException();
        }
        public virtual Building NewInstace(Game game, TextureValue tex, Vector2 position, TextureValue Icon)
        {
            throw new NotImplementedException();
        }
        public virtual void Deposit(Wallet wallet)
        {

        }

        public virtual void Collect(Wallet resource)
        {

        }

        public void SetSpawn(Vector2 position)
        {
            this.spawnPoint = position;
        }
        public Vector2 GetSpawn()
        {
            return this.spawnPoint;
        }
    }
}
