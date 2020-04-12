using _0x46696E616C.AstarAlgorithm;
using _0x46696E616C.Buildings;
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
using TechHandler;
using WorldManager;
using WorldManager.Mobs.HarvestableUnits;
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
        A_Star astar;
        List<Building> toBuild;
        Wallet UnitWallet;
        bool arrived;
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
            UnitWallet = new Wallet();
        }

        public void Build(IEntity target)
        {
            Target = target;
            Move(target.Position);
            State = BaseUnitState.build;
            arrived = false;
        }

        public void Move(Vector2 Position)
        {
            //waypoints = astar.FindPath(this.position, Position, world);
            TargetPosition = Position;
            arrived = false;
        }
        public void Attack(IEntity target)
        {
            this.Target = target;
            this.TargetPosition = Target.Position;
            arrived = false;
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
            if (Position.X < TargetPosition.X - 0.5f)
                Direction += xOne;
            if (Position.X > TargetPosition.X + 0.5f)
                Direction -= xOne;
            if (Position.Y < TargetPosition.Y - 0.5f)
                Direction += yOne;
            if (Position.Y > TargetPosition.Y + 0.5f)
                Direction -= yOne;
            Position += Direction * 5 * gameTime.ElapsedGameTime.Milliseconds / 1000;
            this.UpdatePosition(Position);
            
            if (Direction == zero && Target != null && !arrived)
            {
                if (Target is Building)
                {
                    ((Building)Target).GarrisonedUnits.Add(this);
                    ((Building)Target).Deposit(this.UnitWallet.Withdraw());
                    arrived = true;

                }
                else if (Target is IHarvestable)
                {
                    UnitWallet.Deposit(((HarvestableUnit)Target).Harvest(this.HarvestPower));
                }
                else
                {
                    Target.Damage(this.AttackPower);
                }

            }
            //WayPointFollower();
        }

        private void WayPointFollower()
        {
            if (Direction == zero && waypoints.Count > 0)
            {
                TargetPosition = waypoints[waypoints.Count - 1];
                waypoints.Remove(waypoints[waypoints.Count - 1]);
            }
        }

        public void Garrison(IEntity target)
        {
            this.Target = target;
            this.TargetPosition = Target.Position;
            arrived = false;
        }

        public void Harvest(IEntity target)
        {
            this.Target = target;
            this.TargetPosition = Target.Position;
            arrived = false;
        }

        public override BasicUnit NewInstace(float currentHealth, Vector2 position)
        {
            return new UnitComponent(this.Game, this.name, this.Size, this.TotalHealth, currentHealth, position, BaseUnitState.Idle, this.block.texture, this.world, this.Icon);
        }
    }
}
