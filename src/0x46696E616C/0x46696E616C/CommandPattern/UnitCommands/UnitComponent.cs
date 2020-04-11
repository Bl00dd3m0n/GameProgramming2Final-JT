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
        public UnitComponent(Game game, string name, Vector2 size, float totalHealth, float currentHealth, Vector2 position, BaseUnitState state, TextureValue texture, WorldHandler world) : base(game, name, size, totalHealth, currentHealth, position, state, texture, Color.Blue)
        {
            QueueableThings = new List<IQueueable<TextureValue>>();
            QueueableThings.Add(new Center(game, TextureValue.Center, Vector2.Zero));
            QueueableThings.Add(new FireWall(game, TextureValue.FireWall, Vector2.Zero));
            QueueableThings.Add(new InternetCafe(game, TextureValue.InternetCafe, Vector2.Zero));
            QueueableThings.Add(new Lab(game, TextureValue.Lab, Vector2.Zero));
            QueueableThings.Add(new MediaCenter(game, TextureValue.MediaCenter, Vector2.Zero));
            QueueableThings.Add(new Mines(game, TextureValue.Mines, Vector2.Zero));
            QueueableThings.Add(new PowerSupply(game, TextureValue.PowerSupply, Vector2.Zero));
            QueueableThings.Add(new Mines(game, TextureValue.Mines, Vector2.Zero));
            QueueableThings.Add(new ServerFarm(game, TextureValue.ServerFarm, Vector2.Zero));
            QueueableThings.Add(new SolarPanel(game, TextureValue.SolarPanel, Vector2.Zero));
            QueueableThings.Add(new SteelFactory(game, TextureValue.SteelFactory, Vector2.Zero));

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
        }

        public void Move(Vector2 Position)
        {
            //waypoints = astar.FindPath(this.position, Position, world);
            TargetPosition = Position;
        }
        public void Attack(IEntity target)
        {
            this.Target = target;
            this.TargetPosition = Target.Position;
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
            if (Direction == zero && Target != null)
            {
                if (Target is Building)
                {
                    ((Building)Target).GarrisonedUnits.Add(this);
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
        }

        public void Move(IEntity Target)
        {
            throw new NotImplementedException();
        }
        public void Harvest(IEntity target)
        {
            this.Target = target;
            this.TargetPosition = Target.Position;
        }

    }
}
