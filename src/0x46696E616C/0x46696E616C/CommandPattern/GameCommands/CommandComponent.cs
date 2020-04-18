using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0x46696E616C.Buildings;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.ConcreteImplementations;
using _0x46696E616C.ConcreteImplementations.Resources;
using _0x46696E616C.MobHandler;
using _0x46696E616C.MobHandler.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NationBuilder.DataHandlerLibrary;
using NationBuilder.TileHandlerLibrary;
using TechHandler;
using WorldManager;
using WorldManager.MapData;
using WorldManager.TileHandlerLibrary;

namespace _0x46696E616C.CommandPattern
{
    class CommandComponent : GameComponent, ICommandComponent, IBuildingObserver
    {
        Wallet resources;
        internal List<IUnit> SelectedUnits;

        List<string> resourceStringList;
        private List<IUnit> units;
        static int ID;
        public int Team { get; private set; }


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

        public Texture2D SpawnMarker;
        private int spawnMarkerBuilding;

        public CommandComponent(Game game, Wallet startingResources) : base(game)
        {
            energy = new Energy();
            Buildings = new List<Building>();
            toBuild = new List<Building>();
            resources = startingResources;
            ID++;
            this.Team = ID;
            spawnMarkerBuilding = -1;
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
            this.units = units.ToList();
            this.SelectedUnits = units.ToList();
            resourceStringList = new List<string>();
            ID++;
            this.Team = ID;
            this.world = world;

        }
        /// <summary>
        /// set a spawn point if the spawnmarker isn't null otherwise set the spawn marker
        /// </summary>
        /// <param name="position"></param>
        /// <param name="building"></param>
        internal void SetSpawnPoint(Vector2 position, Building building)
        {
            if (SpawnMarker != null)
            {
                Buildings[spawnMarkerBuilding].SetSpawn(position);
                SpawnMarker = null;
                spawnMarkerBuilding = -1;
            }
            else
            {
                SpawnMarker = ContentHandler.DrawnTexture(TextureValue.SpawnPoint);
                if (!Buildings.Contains(building))
                {
                    Buildings.Add(building);
                }
                spawnMarkerBuilding = Buildings.FindIndex(l => l == building);
            }
        }
        /// <summary>
        /// Selected units set
        /// </summary>
        /// <param name="units"></param>
        public void Select(IUnit units)
        {
            SelectedUnits.Clear();
            SelectedUnits.Add(units);
        }
        /// <summary>
        /// Selected units set
        /// </summary>
        /// <param name="units"></param>
        public void Select(List<IUnit> units)
        {
            SelectedUnits.Clear();
            SelectedUnits.AddRange(units);
        }
        /// <summary>
        /// Attacks a selected target or harvests it depending on the target type
        /// </summary>
        /// <param name="target"></param>
        public void Attack(IEntity target)
        {
            foreach (IUnit unit in SelectedUnits)
            {
                if (unit is Civilian)
                {
                    if (target is IHarvestable)
                    {
                        ((Civilian)unit).Harvest(target);
                    }
                    else
                    {
                        ((Civilian)unit).Attack(target);
                    }

                }
            }
        }
        /// <summary>
        /// Checks the cost of a building
        /// </summary>
        /// <param name="build"></param>
        /// <returns></returns>
        internal bool CheckCost(Building build)
        {
            return resources.CheckCost(build);
        }
        /// <summary>
        /// Selects a building to build
        /// </summary>
        /// <param name="build"></param>
        internal void SelectBuild(Building build)
        {
            SpawnMarker = null;
            this.selectedBuild = build.NewInstace(Game, build.block.texture, build.Position, build.Icon);
            selectedBuild.SetTeam(this.Team);
            selectedBuild.Subscribe(this);
        }
        /// <summary>
        /// moves all selected unit to a position
        /// </summary>
        /// <param name="Position"></param>
        public void Move(Vector2 Position)
        {
            foreach (IUnit unit in SelectedUnits)
            {
                if (unit is Civilian)
                {
                    ((Civilian)unit).Move(Position);
                }
            }
        }
        /// <summary>
        /// for every selected unit garrison them
        /// </summary>
        /// <param name="building"></param>
        internal void Garrison(Building building)
        {
            foreach (IUnit unit in SelectedUnits)
            {
                if (unit is Civilian)
                {
                    ((Civilian)unit).Garrison(building);
                }
            }
        }

        /// <summary>
        /// checks resources for the building and if the unit is places subscribe it to the map and create a new instance of the building to place another one.
        /// </summary>
        /// <param name="wh"></param>
        /// <param name="Position"></param>
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
                            if (unit is Civilian)
                            {
                                ((Civilian)unit).Build(selectedBuild);
                                ((Civilian)unit).QueueBuild(selectedBuild);
                            }
                        }
                    }
                }
                else
                {
                    resources.Deposit(wallet);
                }
            }
            SelectBuild(selectedBuild.NewInstace(Game, selectedBuild.block.texture, Position, selectedBuild.Icon));
        }
        //Returns the teams resources
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
        //Gets the "world" time formatted
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
                if (unit is Civilian)
                {
                    ((Civilian)unit).Update(gameTime);
                }
            }
            timer += gameTime.ElapsedGameTime.Milliseconds;
            //Every second modify this
            if (timer / 1000 >= 1)
            {
                ProduceResources();
                ChargeEnergy();
                Construction();
                Train();
                timer = 0;
            }
            CleanList();
            base.Update(gameTime);
        }
        /// <summary>
        /// updates health of buildings marked for construction
        /// </summary>
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
                            ((Civilian)toBuild[i].GarrisonedUnits[j]).Harvest(world.FindNearest("Wood", toBuild[i].GarrisonedUnits[j].Position));
                        }
                    }
                    else if (toBuild[i].HasTag("Iron Collector"))
                    {
                        for (int j = 0; j < toBuild[i].GarrisonedUnits.Count; j++)
                        {
                            ((Civilian)toBuild[i].GarrisonedUnits[j]).Harvest(world.FindNearest("Iron", toBuild[i].GarrisonedUnits[j].Position));
                        }
                    }
                    Buildings.Add(toBuild[i]);
                    toBuild.RemoveAt(i);
                }
            }

        }

        /// <summary>
        /// Cleans up this list when the unit dies in the world
        /// </summary>
        private void CleanList()
        {
            for (int i = 0; i < units.Count; i++)
            {
                if (((ModifiableTile)units[i]).State == tileState.dead)
                {
                    if (SelectedUnits.Contains(units[i]))
                    {
                        SelectedUnits.Remove(units[i]);
                    }
                    units.Remove(units[i]);
                    i--;
                }
            }
        }

        /// <summary>
        /// Starts units training in the building
        /// </summary>
        /// <param name="build"></param>
        /// <param name="unit"></param>
        public void Train(Building build, IUnit unit)
        {
            if (!Buildings.Contains(build))
                Buildings.Add(build);
            if (unit is Civilian)
            {
                unit = ((Civilian)unit).NewInstace(0, unit.Position);
            }
            build.trainingQueue.Enqueue((IQueueable<TextureValue>)unit);
        }
        /// <summary>
        /// Actual training update loop - returns the units that are at full health(AKA fully trained)
        /// </summary>
        private void Train()
        {
            for (int i = 0; i < Buildings.Count; i++)
            {
                IQueueable<TextureValue> item = Buildings[i].Train();
                if (item != null)
                {
                    units.Add((IUnit)item);
                    if (units[units.Count - 1] is Civilian)
                    {
                        ((Civilian)units[units.Count - 1]).Move(Buildings[i].GetSpawn());
                        world.AddMob(units[units.Count - 1]);
                    }
                }
            }
        }
        /// <summary>
        /// Pulls energy cost for buildings
        /// </summary>
        public void ChargeEnergy()
        {
            foreach (Building building in Buildings)
            {
                resources.Withdraw(energy, building.energyCost / 60f); // TODO implement an building shutoff if the player runs out of energy
            }
        }
        /// <summary>
        /// Runs through buildings and produces resources
        /// </summary>
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
        /// <summary>
        /// deposits resources into the players wallet
        /// </summary>
        /// <param name="wallet"></param>
        public void Deposit(Wallet wallet)
        {
            this.resources.Deposit(wallet);
        }
        /// <summary>
        /// has every unit
        /// </summary>
        /// <param name="building"></param>
        internal void Repair(Building building)
        {
            toBuild.Add(building);
            foreach (IUnit unit in SelectedUnits)
            {
                if (unit is Civilian)
                {
                    ((Civilian)unit).Build(building);
                }
            }
        }
    }
}
