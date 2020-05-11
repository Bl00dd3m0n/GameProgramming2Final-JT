using System;
using NationBuilder.TileHandlerLibrary;
using WorldManager.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using _0x46696E616C.MobHandler.Units;
using Microsoft.Xna.Framework;
using MobHandler;
using _0x46696E616C.MobHandler;
using System.Collections.Generic;
using _0x46696E616C.CommandPattern.Commands;
using TechHandler;
using WorldManager.MapData;
using WorldManager.Buildings;
using System.Collections;
using _0x46696E616C.ConcreteImplementations;
using _0x46696E616C.WorldManager.ConcreteImplementations.Resources;
using Microsoft.Xna.Framework.Graphics;
using _0x46696E616C.TechManager.Stats;
using WorldManager;
using System.Linq;
using _0x46696E616C.Units.Attacks;
using _0x46696E616C.TechManager.Technologies;

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

        protected List<IQueueable<TextureValue>> queueableThings { get; set; }
        public List<IQueueable<TextureValue>> QueueableThings { get { return queueableThings.ToList(); } }
        public TextureValue Icon { get; protected set; }

        public override Vector2 Position { get { return base.Position; } }

        Color teamColor; //TODO Maybe implement this

        IQueueable<TextureValue> trainingObject;

        public IBuildingObserver worldComponent { get; protected set; }

        public List<ITechObserver> techObservers;

        Vector2 spawnPoint { get; set; }

        protected string BuildingDescription { get; set; }

        protected WorldHandler world;

        protected ProjectileManager proj;

        protected float trainTimer;

        protected Stats teamStats;

        public Building(TextureValue texture, Vector2 position, TextureValue Icon, WorldHandler world, ProjectileManager proj, Stats teamStats) : base(texture, position, Color.Blue)
        {
            Cost = new Wallet();
            name = "Building";
            Position = new Vector2(0, 0);
            Size = new Vector2(0, 0);
            stats.Add(new Health("Health", 100000));
            currentHealth = 0;
            healthBar = new HealthBar(new Rectangle(this.Position.ToPoint() - new Point(0, (int)(this.Size.Y * 16 + 1)), Size.ToPoint()));
            GarrisonedUnits = new List<IUnit>();
            this.Icon = Icon;//if the texture values change this breaks it find a better way to do this
            queueableThings = new List<IQueueable<TextureValue>>();
            trainingQueue = new Queue<IQueueable<TextureValue>>();
            BuildingDescription = "";
            techObservers = new List<ITechObserver>();
            this.world = world;
            this.proj = proj;
            this.teamStats = teamStats;
        }

        public void Subscribe(IBuildingObserver observer)
        {
            worldComponent = observer;
        }

        public void Subscribe(ITechObserver observer)
        {
            techObservers.Add(observer);
        }
        //this method is called every second
        public IQueueable<TextureValue> Train(GraphicsDevice gd)
        {
            if (trainingQueue.Count > 0)
            {
                trainingObject = trainingQueue.Peek();
                if (trainingObject is IEntity)
                {
                    if (((IEntity)trainingObject).CurrentHealth >= ((IEntity)trainingObject).TotalHealth)
                    {
                        if (trainingObject is BasicUnit)
                        {
                            ((BasicUnit)trainingObject).UpdatePosition(gd, spawnPoint);
                            ((BasicUnit)trainingObject).PlacedTile(gd);
                            ((BasicUnit)trainingObject).SetTeam(this.TeamAssociation);
                        }
                        return trainingQueue.Dequeue();
                    }
                    ((BasicUnit)trainingObject).CurrentHealth += 10f;//this probably should be updated too
                }
                else if (trainingObject is ITech)
                {
                    if (trainTimer >= ((Technology)trainingObject).LearnTime)
                    {
                        trainTimer = 0;
                        Learn((ITech)trainingObject);
                        trainingQueue.Dequeue();
                        return null;
                    }
                    trainTimer++;//Since it's supposed to be called every one second it's fair to add 1 every time
                }

            }
            return null;
        }

        public void Construct()
        {
            foreach (IUnit unit in GarrisonedUnits)
            {
                if (unit.UnitState == BaseUnitState.build)
                {
                    if (unit is BasicUnit)
                    {
                        CurrentHealth += ((BasicUnit)unit).stats[typeof(BuildPower)].Value + ((BasicUnit)unit).teamStats[typeof(BuildPower)].Value;
                    }
                    if (CurrentHealth > TotalHealth)
                        CurrentHealth = TotalHealth;
                }
            }
        }

        public virtual void AddQueueable(IQueueable<TextureValue> item) // For Tech
        {
            queueableThings.Add(item);//workaround for not being able to have UnitComponent
        }


        public void Learn(ITech tech)
        {
            foreach (ITechObserver observer in techObservers)
            {
                observer.Update(tech);
            }
        }

        public override void Damage(float amount)
        {
            base.Damage(amount);
        }

        public override void UpdatePosition(GraphicsDevice gd, Vector2 position)
        {
            if (!placed)
            {
                spawnPoint = position - (new Vector2(0, -1) * this.Size);
            }
            base.UpdatePosition(gd, position);
        }
        public override void Die()
        {
            base.Die();
        }
        public virtual Building NewInstace(TextureValue tex, Vector2 position, TextureValue Icon)
        {
            throw new NotImplementedException();//This never should be hit
        }

        public virtual void Deposit(Wallet wallet)
        {

        }

        public virtual void Collect(Wallet resource)
        {

        }

        public virtual Building AddQueueables()
        {
            return this;
        }

        public void SetSpawn(Vector2 position)
        {
            this.spawnPoint = position;
        }
        public Vector2 GetSpawn()
        {
            return this.spawnPoint;
        }

        internal object Description()
        {
            string description = string.Empty;
            description += $"{name}\n";
            description += "Cost:\n";
            foreach (string resource in Cost.ResourceString())
            {
                description += $"{resource}\n";
            }
            description += this.BuildingDescription;
            return description;
        }
    }
}
