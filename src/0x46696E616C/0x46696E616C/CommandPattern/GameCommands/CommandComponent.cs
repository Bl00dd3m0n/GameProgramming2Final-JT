using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0x46696E616C.Buildings;
using _0x46696E616C.MobHandler;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using TechHandler;
using WorldManager;
using WorldManager.MapData;

namespace _0x46696E616C.CommandPattern
{
    class CommandComponent : GameComponent, ICommandComponent, IBuildingObserver
    {
        Wallet resources;
        internal List<IUnit> SelectedUnits;

        List<string> resourceStringList;
        private List<IUnit> units;

        internal void SetSpawnPoint(Vector2 position, Building building)
        {
            //building.SpawnPoint();
        }

        public List<IUnit> Units
        {
            get { return units.ToList(); }
            private set { units = value; }
        }
        List<Building> Buildings;
        List<Building> toBuild;

        Energy energy;
        float timer;

        public Building selectedBuild;
        private WorldHandler world;
        public CommandComponent(Game game, Wallet startingResources) : base(game)
        {
            energy = new Energy();
            Buildings = new List<Building>();
            toBuild = new List<Building>();
            resources = startingResources;

        }
        /// <summary>
        /// Test Constructor to easily get units
        /// </summary>
        /// <param name="game"></param>
        /// <param name="startingResources"></param>
        public CommandComponent(Game game, Wallet startingResources, List<IUnit> units, WorldHandler world) : base(game)
        {
            energy = new Energy();
            Buildings = new List<Building>();
            toBuild = new List<Building>();
            resources = startingResources;
            SelectedUnits = this.units = units;
            resourceStringList = new List<string>();
            this.world = world;

        }

        public void Select(List<IUnit> units)
        {
            SelectedUnits = units;
        }

        public void Attack(IEntity target)
        {
            foreach (IUnit unit in SelectedUnits)
            {
                if (unit is UnitComponent)
                {
                    if (target is IHarvestable)
                    {
                        ((UnitComponent)unit).Harvest(target);
                    }
                    else
                    {
                        ((UnitComponent)unit).Attack(target);
                    }

                }
            }
        }

        internal bool CheckCost(Building build)
        {
            return resources.CheckCost(build);
        }

        internal void SelectBuild(Building build)
        {
            this.selectedBuild = build;
        }

        public void Move(Vector2 Position)
        {
            foreach (IUnit unit in units)
            {
                if (unit is UnitComponent)
                {
                    ((UnitComponent)unit).Move(Position);
                }
            }
        }

        internal void Garrison(Building building)
        {
            foreach (IUnit unit in SelectedUnits)
            {
                if (unit is UnitComponent)
                {
                    ((UnitComponent)unit).Garrison(building);
                }
            }
        }


        internal void Build(WorldHandler wh, Vector2 Position)
        {
            Wallet wallet = resources.Withdraw(selectedBuild.Cost);
            if (selectedBuild.worldComponent == null) selectedBuild.Subscribe(this);
            if (wallet != null)
            {
                if (wh.Place(selectedBuild, Position))
                {
                    selectedBuild.UpdatePosition(Position);
                    selectedBuild.PlacedTile();

                    toBuild.Add(selectedBuild);
                    if (SelectedUnits != null)
                    {
                        foreach (IUnit unit in SelectedUnits)
                        {
                            if (unit is UnitComponent)
                            {
                                ((UnitComponent)unit).Build(selectedBuild);
                            }
                        }
                    }
                }
                else
                {
                    resources.Deposit(wallet);
                }
            }
            selectedBuild = selectedBuild.NewInstace(Game, selectedBuild.block.texture, selectedBuild.Position, selectedBuild.Icon);
            selectedBuild.Subscribe(this);
        }

        public List<string> Resources()
        {
            resourceStringList.Clear();
            resourceStringList.Add($"Wood:{resources.Count(new Wood())}");
            resourceStringList.Add($"Energy:{resources.Count(new Energy())}");
            resourceStringList.Add($"Iron:{resources.Count(new Iron())}");
            resourceStringList.Add($"Likes:{resources.Count(new Likes())}");
            resourceStringList.Add($"Money:{resources.Count(new Money())}");
            resourceStringList.Add($"Steel:{resources.Count(new Steel())}");
            return resourceStringList;
        }

        public string Time()
        {
            int hours = 0;
            int minutes = 0;
            int seconds = (int)(timer / 1000);

            seconds %= 60;
            minutes = seconds / 60;
            if (minutes > 60)
            {
                minutes %= 60;
                hours /= 60;
            }
            if (hours > 0)
                return $"{hours}:{minutes}:{seconds}";
            return $"0:{minutes}:{seconds}";
        }

        public override void Update(GameTime gameTime)
        {
            foreach (IUnit unit in units)
            {
                if (unit is UnitComponent)
                {
                    ((UnitComponent)unit).Update(gameTime);
                }
            }
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer / 1000 >= 1)
            {
                ProduceResources();
                ChargeEnergy();
                Construction();
                Train();
            }
            base.Update(gameTime);
        }

        private void Construction()
        {
            for (int i = 0; i < toBuild.Count; i++)
            {
                toBuild[i].healthBar.UpdateHealth(toBuild[i], Game.GraphicsDevice);
                toBuild[i].Construct(); //TODO implement worker proficiency at repairing/building here(More workers/better tech should speed this process up)
                if (toBuild[i].CurrentHealth >= toBuild[i].TotalHealth)
                {
                    if (toBuild[i].HasTag("Wood Collector"))
                    {
                        for (int j = 0; j < toBuild[i].GarrisonedUnits.Count; j++)
                        {
                            ((UnitComponent)toBuild[i].GarrisonedUnits[j]).Harvest(world.FindNearest("Wood", toBuild[i].GarrisonedUnits[j].Position));
                        }
                    }
                    else if(toBuild[i].HasTag("Iron Collector"))
                    {
                        for(int j = 0; j < toBuild[i].GarrisonedUnits.Count; j++)
                        {
                            ((UnitComponent)toBuild[i].GarrisonedUnits[j]).Harvest(world.FindNearest("Iron", toBuild[i].GarrisonedUnits[j].Position));
                        }
                    }
                    Buildings.Add(toBuild[i]);
                    toBuild.RemoveAt(i);
                }
            }

        }
        public void Train(Building build, IUnit unit)
        {
            if (!Buildings.Contains(build))
                Buildings.Add(build);
            build.trainingQueue.Enqueue((IQueueable<TextureValue>)unit);
        }
        private void Train()
        {
            for (int i = 0; i < Buildings.Count; i++)
            {
                IQueueable<TextureValue> item = Buildings[i].Train();
                if(item != null)
                {
                    units.Add((IUnit)item);
                    if(units[units.Count - 1] is UnitComponent)
                    {
                        ((UnitComponent)units[units.Count - 1]).Move(Buildings[i].GetSpawn());
                    }
                }
            }
        }

        public void ChargeEnergy()
        {
            foreach (Building building in Buildings)
            {
                resources.Withdraw(energy, building.energyCost / 60f); // TODO implement an building shutoff if the player runs out of energy
            }
        }

        public void ProduceResources()
        {
            foreach (Building building in Buildings)
            {
                if (building is IProductionCenter)
                {
                    for (int i = 0; i < ((IProductionCenter)building).productionTypes.Count; i++)
                    {
                        resources.Deposit(((IProductionCenter)building).productionTypes[i], ((IProductionCenter)building).ProductionAMinute[i] / 60f);
                    }
                }
            }
        }

        public void Deposit(Wallet wallet)
        {
            this.resources.Deposit(wallet);
        }

        internal void Repair(Building building)
        {
            toBuild.Add(building);
            foreach (IUnit unit in units)
            {
                if (unit is UnitComponent)
                {
                    ((UnitComponent)unit).Build(building);
                }
            }
        }
    }
}
