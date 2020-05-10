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
using _0x46696E616C.TechManager.Stats;
using _0x46696E616C.TechManager.Technologies;
using _0x46696E616C.Units.AllyUnit;
using _0x46696E616C.WorldManager.WorldImplementations.Buildings;
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
    class CommandComponent : GameComponent, ICommandComponent, IBuildingObserver, ITechObserver
    {
        Wallet resources;
        internal List<IUnit> SelectedUnits;

        List<string> resourceStringList;
        public static int ID { get; set; }
        public int Team { get; private set; }

        List<Building> toBuild;

        float timer;
        float clock;

        public Building SelectedBuild;
        private WorldHandler world;

        public Texture2D SpawnMarker;
        private Building spawnMarkerBuilding;
        public bool IsGameOver { get; private set; }
        internal Stats TeamStats { get; private set; }

        /// <summary>
        /// Test Constructor to easily get units
        /// </summary>
        /// <param name="game"></param>
        /// <param name="startingResources"></param>
        public CommandComponent(Game game, WorldHandler world) : this(game, new Wallet(), world)
        {
        }
        /// <summary>
        /// Test Constructor to easily get units
        /// </summary>
        /// <param name="game"></param>
        /// <param name="startingResources"></param>
        public CommandComponent(Game game, Wallet startingResources, WorldHandler world) : base(game)
        {
            toBuild = new List<Building>();
            resources = startingResources;
            resourceStringList = new List<string>();
            ID++;
            this.Team = ID;
            this.world = world;
            IsGameOver = false;
            SelectedUnits = new List<IUnit>();
            TeamStats = new Stats(new List<Stat>() { new Health("Health", 0), new MeleeDamage("Damage", 0), new Range("Range", 0), new HarvestPower("Harvest Power", 0), new BuildPower("Harvest Power", 0), new InventorySpace("InventorySpace", 0), });
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
                spawnMarkerBuilding.SetSpawn(position);
                SpawnMarker = null;
                spawnMarkerBuilding = null;
            }
            else
            {
                SpawnMarker = ContentHandler.DrawnTexture(TextureValue.SpawnPoint);

                ModifiableTile tile = world.GetTile(position);
                if (tile is ReferenceTile)
                {
                    tile = ((ReferenceTile)tile).tile;
                }
                spawnMarkerBuilding = (Building)tile;
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
                if (unit is BasicUnit && ((BasicUnit)unit).TeamAssociation == this.Team)
                {
                    if (unit is Civilian && target is IHarvestable)
                    {
                        ((Civilian)unit).Harvest(target);
                    }
                    else
                    {
                        if (unit is Civilian)
                        {
                            ((Civilian)unit).Attack(target);
                        }
                        else if (unit is OffensiveUnits)
                        {
                            ((OffensiveUnits)unit).Attack(target);
                        }
                    }

                }
            }
        }

        internal void EndGame()
        {
            IsGameOver = true;
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
            this.SelectedBuild = build.NewInstace(build.block.texture, build.Position, build.Icon);
            SelectedBuild.SetTeam(this.Team);
            SelectedBuild.Subscribe((IBuildingObserver)this);
        }
        /// <summary>
        /// moves all selected unit to a position
        /// </summary>
        /// <param name="Position"></param>
        public void Move(Vector2 Position)
        {
            foreach (IUnit unit in SelectedUnits)
            {
                if (unit is BasicUnit && ((BasicUnit)unit).TeamAssociation == this.Team)
                {
                    ((BasicUnit)unit).Move(Position);
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
                if (unit is BasicUnit && ((BasicUnit)unit).TeamAssociation == this.Team)
                {
                    if (unit is Civilian)
                    {
                        ((Civilian)unit).Garrison(building);
                    }
                    else if (unit is OffensiveUnits)
                    {
                        ((OffensiveUnits)unit).Garrison(building);
                    }
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
            Wallet wallet = resources.Withdraw(SelectedBuild.Cost);
            if (SelectedBuild.worldComponent == null) SelectedBuild.Subscribe((IBuildingObserver)this);
            if (wallet != null)
            {
                if (wh.Place(SelectedBuild, Position))
                {
                    SelectedBuild.UpdatePosition(Game.GraphicsDevice, Position);
                    SelectedBuild.PlacedTile();



                    toBuild.Add(SelectedBuild);
                    if (SelectedUnits != null)
                    {
                        foreach (IUnit unit in SelectedUnits)
                        {
                            if (unit is Civilian)
                            {
                                ((Civilian)unit).Build(SelectedBuild);
                                ((Civilian)unit).QueueBuild(SelectedBuild);
                            }
                        }
                    }
                }
                else
                {
                    resources.Deposit(wallet);
                }
            }
            SelectBuild(SelectedBuild.NewInstace(SelectedBuild.block.texture, Position, SelectedBuild.Icon));
        }
        //Returns the teams resources
        public List<string> Resources()
        {
            return resources.ResourceString();
        }
        //Gets the "world" time formatted
        public string Time()
        {
            int hours = 0;
            int minutes = 0;
            int seconds = (int)(clock / 1000);

            minutes = seconds / 60;
            seconds %= 60;
            if (minutes > 60)
            {
                hours = minutes / 60;
                minutes %= 60;
            }
            if (hours > 0)
                return $"{hours}:{minutes}:{seconds}";
            return $"0:{minutes}:{seconds}";
        }

        public override void Update(GameTime gameTime)
        {
            foreach (IUnit unit in world.GetUnits(Team))
            {
                if (unit is BasicUnit)
                {
                    ((BasicUnit)unit).Update(gameTime);
                }
            }
            timer += gameTime.ElapsedGameTime.Milliseconds;
            clock += gameTime.ElapsedGameTime.Milliseconds;
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
            if (world.GetTiles(Team).Where(l => l is Building).Count() <= 0 && world.GetUnits(Team).Count <= 0)
            {
                //IsGameOver = true;
            }
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
                    toBuild[i].Subscribe((ITechObserver)this);
                    toBuild.RemoveAt(i);
                }
            }

        }

        /// <summary>
        /// Cleans up this list when the unit dies in the world
        /// </summary>
        private void CleanList()
        {
            for (int i = 0; i < SelectedUnits.Count; i++)
            {
                if (SelectedUnits[i] == null)
                {
                    SelectedUnits.Remove(SelectedUnits[i]);
                }
            }
        }

        /// <summary>
        /// Starts units training in the building
        /// </summary>
        /// <param name="build"></param>
        /// <param name="unit"></param>
        public void Train(Building building, IUnit unit)
        {
            Wallet wallet = resources.Withdraw(unit.Cost);
            if (wallet != null)
            {
                if (unit is BasicUnit)
                {
                    unit = ((BasicUnit)unit).NewInstace(0, unit.Position);
                }
                building.trainingQueue.Enqueue((IQueueable<TextureValue>)unit);
            }
        }
        public void Learn(Building building, ITech tech)
        {
            Wallet wallet = resources.Withdraw(((Technology)tech).Cost);
            if (wallet != null)
            {
                building.trainingQueue.Enqueue((IQueueable<TextureValue>)tech);
            }
        }
        /// <summary>
        /// Actual training update loop - returns the units that are at full health(AKA fully trained)
        /// </summary>
        private void Train()
        {
            if (resources.Count(new Energy()) > 0)//Shut off training if out of power
            {
                Building[] buildings = world.GetTiles(Team).Where(l => l is Building).Cast<Building>().ToArray();
                for (int i = 0; i < buildings.Length; i++)
                {
                    IQueueable<TextureValue> item = buildings[i].Train(Game.GraphicsDevice);
                    if (item != null)
                    {

                        if (item is BasicUnit)
                        {
                            ((BasicUnit)item).Move(buildings[i].GetSpawn());
                            world.AddMob((BasicUnit)item);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Pulls energy cost for buildings
        /// </summary>
        public void ChargeEnergy()
        {
            Building[] buildings = world.GetTiles(Team).Where(l => l is Building).Cast<Building>().ToArray();
            foreach (Building building in buildings)
            {
                if (building is IResourceCharge && building.built)
                {
                    for (int i = 0; i < ((IResourceCharge)building).ChargeTypes.Count; i++)
                    {
                        resources.Withdraw(((IResourceCharge)building).ChargeTypes[i], ((IResourceCharge)building).ChargeAMinute[i] / 60f);
                    }
                }
            }
        }

        /// <summary>
        /// Runs through buildings and produces resources
        /// </summary>
        public void ProduceResources()
        {
            Building[] buildings = world.GetTiles(Team).Where(l => l is Building).Cast<Building>().ToArray();
            foreach (Building building in buildings)
            {
                if (building is IProductionCenter && building.built)
                {
                    for (int i = 0; i < ((IProductionCenter)building).productionTypes.Count; i++)
                    {
                        if (resources.Count(new Energy()) > 0 || ((IProductionCenter)building).productionTypes[i] is Energy)//Production shuts off without energy
                        {
                            resources.Deposit(((IProductionCenter)building).productionTypes[i], ((IProductionCenter)building).ProductionAMinute[i] / 60f);
                        }
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
            if (!toBuild.Contains(building))
            {
                toBuild.Add(building);
            }
            foreach (IUnit unit in SelectedUnits)
            {
                if (unit is Civilian)
                {
                    ((Civilian)unit).Build(building);
                }
            }
        }

        public void Update(ITech tech)
        {
            if (tech is StatTech)
            {
                TeamStats[((StatTech)tech).Upgrade.GetType()] += ((StatTech)tech).Upgrade;
            }
        }
    }
}
