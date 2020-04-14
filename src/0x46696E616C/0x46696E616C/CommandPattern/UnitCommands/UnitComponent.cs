using _0x46696E616C.AstarAlgorithm;
using _0x46696E616C.Buildings;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.MobHandler;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechHandler;
using WorldManager;
using WorldManager.Buildings;
using WorldManager.Mobs.HarvestableUnits;
using WorldManager.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace _0x46696E616C.CommandPattern
{
    internal class UnitComponent : BasicUnit, ICommandComponent
    {
        IEntity Target;
        Vector2 TargetPosition;
        Vector2 NextPoint;
        Vector2 zero;
        Vector2 xOne;
        Vector2 yOne;
        List<Vector2> waypoints;
        WorldHandler world;
        A_Star astar;
        List<Building> toBuild;
        Wallet UnitWallet;
        bool arrived;
        IEntity returnTarget;
        float timer = 0;
        public UnitComponent(Game game, string name, Vector2 size, float totalHealth, float currentHealth, Vector2 position, BaseUnitState state, TextureValue texture, WorldHandler world, TextureValue Icon) : base(game, name, size, totalHealth, currentHealth, position, state, texture, Color.Blue, Icon)
        {
            QueueableThings = new List<IQueueable<TextureValue>>();
            QueueableThings.Add(new Center(game, TextureValue.Center, Vector2.Zero, TextureValue.CenterIcon));
            QueueableThings.Add(new FireWall(game, TextureValue.FireWall, Vector2.Zero, TextureValue.FireWallIcon));
            QueueableThings.Add(new InternetCafe(game, TextureValue.InternetCafe, Vector2.Zero, TextureValue.InternetCafeIcon));
            QueueableThings.Add(new Lab(game, TextureValue.Lab, Vector2.Zero, TextureValue.LabIcon));
            QueueableThings.Add(new MediaCenter(game, TextureValue.MediaCenter, Vector2.Zero, TextureValue.MediaCenterIcon));
            QueueableThings.Add(new Mines(game, TextureValue.Mines, Vector2.Zero, TextureValue.MinesIcon));
            QueueableThings.Add(new PowerSupply(game, TextureValue.PowerSupply, Vector2.Zero, TextureValue.PowerSupplyIcon));
            QueueableThings.Add(new ServerFarm(game, TextureValue.ServerFarm, Vector2.Zero, TextureValue.ServerFarmIcon));
            QueueableThings.Add(new SolarPanel(game, TextureValue.SolarPanel, Vector2.Zero, TextureValue.SolarPanelIcon));
            QueueableThings.Add(new SteelFactory(game, TextureValue.SteelFactory, Vector2.Zero, TextureValue.SteelFactoryIcon));

            astar = new A_Star();
            waypoints = new List<Vector2>();
            zero = Vector2.Zero;
            xOne = new Vector2(1, 0);
            yOne = new Vector2(0, 1);
            speed = 50;
            this.world = world;
            toBuild = new List<Building>();
            UnitWallet = new UnitWallet(10);
            NextPoint = TargetPosition = Position;
            
        }

        public override void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;
            UpdateMove(gameTime);
            if (timer / 1000 >= 1)
            {
                UnitInteraction();
                
            }
            base.Update(gameTime);
        }

        private void UnitInteraction()
        {
            if (Direction == zero && Target != null && !arrived && TargetPosition.ToPoint() == NextPoint.ToPoint())
            {
                if (Target is Building)
                {
                    ((Building)Target).GarrisonedUnits.Add(this);

                    if (((Building)Target).HasTag("Wood Collector") && ((Building)Target).HasTag("Iron Collector"))
                    {
                        ((Building)Target).Collect(this.UnitWallet.Withdraw());
                    }
                    else if (((Building)Target).HasTag("Iron Collector"))
                    {
                        ((Building)Target).Collect(this.UnitWallet.Withdraw(new Iron()));
                    }
                    arrived = true;
                    if (returnTarget != null) Harvest(returnTarget);
                    returnTarget = null;//Set to null after setting target...keeps the unit from going random places the player wouldn't expect.

                }
                else if (Target is IHarvestable)
                {
                    Type type = ((HarvestableUnit)Target).type.GetType();
                    Wallet wal = ((HarvestableUnit)Target).Harvest(this.HarvestPower);
                    if (((HarvestableUnit)Target).State == tileState.dead) //When the source dies find a new thing to harvest
                    {
                        Harvest(world.FindNearest(((HarvestableUnit)Target).type.GetType().Name.ToString(), this.Position));
                    }
                    bool returnResources = UnitWallet.Deposit(wal);
                    if (returnResources)
                    {
                        ((HarvestableUnit)Target).Return(wal);
                        returnTarget = Target;
                        Garrison(world.FindNearest(type.Name.ToString() + " Collector", this.Position));
                    }
                }
                else
                {
                    Target.Damage(this.AttackPower);
                    if (((ModifiableTile)Target).State == tileState.dead) Target = null;
                }
                timer = 0;// Only reset the timer if the unit does something that way the player instantly acts when they get to the position
            }
        }

        /// <summary>
        /// Moves by waypoint
        /// </summary>
        ///<see cref="Probably implement some sort of A* and flocking ai either here or in the Command Component"/>>
        private void UpdateMove(GameTime gameTime)
        {

            Direction = zero;
            if (Position.X < NextPoint.X - 0.5f)
                Direction += xOne;
            if (Position.X > NextPoint.X + 0.5f)
                Direction -= xOne;
            if (Position.Y < NextPoint.Y - 0.5f)
                Direction += yOne;
            if (Position.Y > NextPoint.Y + 0.5f)
                Direction -= yOne;
            Position += Direction * 5 * gameTime.ElapsedGameTime.Milliseconds / 1000;
            //this.UpdatePosition(Position);

            WayPointFollower();
        }

        private void WayPointFollower()
        {
            if (Direction == zero && waypoints.Count > 1)
            {
                waypoints.Remove(waypoints[0]);
                NextPoint = waypoints[0];
            }
        }

        public void Build(IEntity target)
        {
            ResetUnit();
            Target = target;
            Move(target.Position);
            State = BaseUnitState.build;
            arrived = false;
        }
        /// <summary>
        /// Generates waypoints using A*
        /// </summary>
        /// <param name="Position"></param>
        public void Move(Vector2 Position)
        {
            Position -= new Vector2(1, 0);
            ResetUnit();
            waypoints.Clear();
            waypoints = astar.FindPath(this.Position, Position, world);
            if (waypoints.Count > 0)
            {
                NextPoint = waypoints[0];
            }
            TargetPosition = Position;
            arrived = false;
        }
        public void Attack(IEntity target)
        {
            ResetUnit();
            this.Target = target;
            Move(Target.Position);
            arrived = false;
        }

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
        public void Harvest(IEntity target)
        {
            ResetUnit();
            this.Target = target;
            this.Move(target.Position);
            arrived = false;
        }

        public override BasicUnit NewInstace(float currentHealth, Vector2 position)
        {
            return new UnitComponent(this.Game, this.name, this.Size, this.TotalHealth, currentHealth, position, BaseUnitState.Idle, this.block.texture, this.world, this.Icon);
        }
    }
}
