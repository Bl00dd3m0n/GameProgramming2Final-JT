﻿using _0x46696E616C.AstarAlgorithm;
using _0x46696E616C.Buildings;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.ConcreteImplementations;
using _0x46696E616C.ConcreteImplementations.Resources;
using _0x46696E616C.MobHandler;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.TechManager.Stats;
using _0x46696E616C.Units.Attacks;
using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechHandler;
using Util;
using WorldManager;
using WorldManager.Mobs.HarvestableUnits;
using WorldManager.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace _0x46696E616C.CommandPattern
{
    internal class Civilian : BasicUnit, ICommandComponent
    {
        Wallet unitWallet;
        bool arrived;
        IEntity returnTarget;
        float timer = 0;
        ProjectileManager proj;
        public Civilian(string name, Vector2 size, float totalHealth, float currentHealth, Vector2 position, BaseUnitState state, TextureValue texture, WorldHandler world, TextureValue Icon, float range, ProjectileManager proj, Stats teamStats) : base(name, size, totalHealth, currentHealth, position, state, texture, Color.Blue, Icon, world, range, teamStats)
        {
            this.proj = proj;
            stats.Add(new BuildPower("Build Power", 10));
            stats.Add(new MeleeDamage("Attack Power", 10));
            stats.Add(new HarvestPower("Harvest Power", 2));
            stats.Add(new InventorySpace("Inventory Space", 10));
            waypoints = new List<Vector2>();
            zero = Vector2.Zero;
            xOne = new Vector2(1, 0);
            yOne = new Vector2(0, 1);
            toBuild = new Stack<Building>();
            speed = 50;
            unitWallet = new UnitWallet(this);
            tags.Add("HasInventoryCap");
            tags.Add("CanAttack");
            tags.Add("HasHealth");
            nextPoint = TargetPosition = Position;
            Description = "A basic unit able to Harvest resources, and build things\nMinimal Damage";
        }

        public override BasicUnit AddQueueables()
        {
            QueueableThings = new List<IQueueable<TextureValue>>();
            QueueableThings.Add(new Center(TextureValue.Center, Vector2.Zero, TextureValue.CenterIcon, world, proj, teamStats));
            QueueableThings.Add(new FireWall(TextureValue.FireWall, Vector2.Zero, TextureValue.FireWallIcon, world, proj, teamStats));
            QueueableThings.Add(new InternetCafe(TextureValue.InternetCafe, Vector2.Zero, TextureValue.InternetCafeIcon, world, proj, teamStats));
            QueueableThings.Add(new Lab(TextureValue.Lab, Vector2.Zero, TextureValue.LabIcon, world, proj, teamStats));
            QueueableThings.Add(new MediaCenter(TextureValue.MediaCenter, Vector2.Zero, TextureValue.MediaCenterIcon, world, proj, teamStats));
            QueueableThings.Add(new Mines(TextureValue.Mines, Vector2.Zero, TextureValue.MinesIcon, world, proj, teamStats));
            QueueableThings.Add(new PowerSupply(TextureValue.PowerSupply, Vector2.Zero, TextureValue.PowerSupplyIcon, world, proj, teamStats));
            QueueableThings.Add(new ServerFarm(TextureValue.ServerFarm, Vector2.Zero, TextureValue.ServerFarmIcon, world, proj, teamStats));
            QueueableThings.Add(new SolarPanel(TextureValue.SolarPanel, Vector2.Zero, TextureValue.SolarPanelIcon, world, proj, teamStats));
            QueueableThings.Add(new SteelFactory(TextureValue.SteelFactory, Vector2.Zero, TextureValue.SteelFactoryIcon, world, proj, teamStats));
            return this;
        }

        public override void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;
            base.Update(gameTime);
            if (timer / 1000 >= 1)
            {
                UnitInteraction();
            }
            if (waypoints.Count() > 0 && Vector2.Distance(Position, TargetPosition + DistanceFromPosition) < stats[typeof(Range)].Value)
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
            float dist = Vector2.Distance(TargetPosition + DistanceFromPosition, nextPoint);
            if (Direction == zero && Target != null && !arrived && dist <= stats[typeof(Range)].Value)
            {
                if (Target is Building && ((ModifiableTile)Target).TeamAssociation == this.TeamAssociation)
                {
                    ((Building)Target).GarrisonedUnits.Add(this);
                    if (this.UnitState == BaseUnitState.harvest)
                    {
                        if (((Building)Target).HasTag("Wood Collector") && ((Building)Target).HasTag("Iron Collector"))
                        {
                            ((Building)Target).Collect(this.unitWallet.Withdraw());
                        }
                        else if (((Building)Target).HasTag("Iron Collector"))
                        {
                            ((Building)Target).Collect(this.unitWallet.Withdraw(new Iron()));
                        }
                    }
                    arrived = true;
                    if (returnTarget != null) Harvest(returnTarget);
                    else if (toBuild.Count > 0)
                        Build(toBuild.Pop());
                    returnTarget = null;//Set to null after setting target...keeps the unit from going random places the player wouldn't expect.

                }
                else if (Target is IHarvestable)
                {
                    Type type = ((HarvestableUnit)Target).type.GetType();
                    Wallet wal = ((HarvestableUnit)Target).Harvest(stats[typeof(HarvestPower)].Value);
                    if (((HarvestableUnit)Target).State == tileState.dead) //When the source dies find a new thing to harvest
                    {
                        Harvest(world.FindNearest(((HarvestableUnit)Target).type.GetType().Name.ToString(), this.Position));
                    }
                    bool returnResources = unitWallet.Deposit(wal);
                    if (returnResources)//If the unit wallet is full return it to the nearest building that collects the type of the harvestable resource
                    {
                        ((HarvestableUnit)Target).Return(wal);
                        returnTarget = Target;
                        try
                        {
                            Garrison(world.FindNearest(type.Name.ToString() + " Collector", this.Position));
                        }
                        catch (NullReferenceException) { }
                    }
                }
                //Attack the unit if it isn't part of their team
                else if(((ModifiableTile)Target).TeamAssociation != this.TeamAssociation)
                {
                    Target.Damage(this.stats[typeof(MeleeDamage)].Value);
                    if (((ModifiableTile)Target).State == tileState.dead) Target = null;
                }
                timer = 0;// Only reset the timer if the unit does something that way the player instantly acts when they get to the position
            }
        }

        /// <summary>
        /// Moves by waypoint
        /// </summary>
        ///<see cref="Probably implement some sort of A* and flocking ai either here or in the Command Component"/>>
        protected override void UpdateMove(GameTime gameTime)
        {
            base.UpdateMove(gameTime);
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
        public override void Move(Vector2 Position)
        {
            if (world.GetUnit(Position) == null && world.GetTile(Position) == null) Target = null;  
            base.Move(Position);
        }
        /// <summary>
        /// Targets a placed building to build
        /// </summary>
        /// <param name="target"></param>
        public void Build(IEntity target)
        {
            ResetUnit();
            Target = target;
            Move(target.Position);
            UnitState = BaseUnitState.build;
            arrived = false;
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
            arrived = false;
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
            arrived = false;
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
        /// Move to the harvestable unit
        /// </summary>
        /// <param name="target"></param>
        public void Harvest(IEntity target)
        {
            ResetUnit();
            this.Target = target;
            this.Move(target.Position);
            UnitState = BaseUnitState.harvest;
            arrived = false;
        }
        public void QueueBuild(IEntity building)
        {
            if(building is Building)
            {
                if(!toBuild.Contains(Target) && Target != null)
                {
                    toBuild.Push((Building)building);
                }
                else
                {
                    toBuild = MoveStackToTop.Move(toBuild, (Building)Target);
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
            return new Civilian(this.name, this.Size, this.TotalHealth, currentHealth, position, BaseUnitState.Idle, this.block.texture, this.world, this.Icon, this.stats[typeof(Range)].Value, proj, teamStats).AddQueueables();
        }
    }
}
