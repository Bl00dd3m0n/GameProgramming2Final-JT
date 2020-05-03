using _0x46696E616C.Buildings;
using _0x46696E616C.CommandPattern;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.TechManager.Stats;
using _0x46696E616C.Units.Attacks;
using _0x46696E616C.Units.HostileMobManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldManager;

namespace MobHandler.HostileMobManager
{
    class WaveManager : GameComponent
    {
        public int Wave { get; private set; }
        WorldHandler world;
        private List<IUnit> units;
        private List<IUnit> clearUnits;
        ProjectileManager projectileManager;
        private float waveSpawnTimer;
        private float waveSpawnTime;
        public float TimeTillSpawn { get { return waveSpawnTime - (waveSpawnTimer / 1000); } }
        public bool StartSpawn;
        public bool Won { get; private set; }
        internal Stats TeamStats { get; private set; }
        public WaveManager(Game game, WorldHandler world, ProjectileManager projectileManager) : base(game)
        {
            this.world = world;
            Wave = 0;
            this.projectileManager = projectileManager;
            units = new List<IUnit>();
            clearUnits = new List<IUnit>();
            waveSpawnTime = 30f;
            TeamStats = new Stats(new List<Stat>() { new Health("Health", 0), new MeleeDamage("Damage", 0), new Range("Range", 0), new HarvestPower("Harvest Power", 0), new BuildPower("Harvest Power", 0), new InventorySpace("InventorySpace", 0), });
        }

        private void CheckGameOver()
        {
            if (world.GetUnits(2).Where(l => l is HostileMob).Count() <= 0)
            {
                if (world.GetTiles("Portal").Count() <= 0)
                {
                    Won = true;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            waveSpawnTimer += gameTime.ElapsedGameTime.Milliseconds;
            CheckGameOver();
            if (waveSpawnTimer / 1000 >= 300 && !StartSpawn)//5 Minute spawn time
            {

                StartSpawn = true;
                waveSpawnTimer = 0;
            }
            if (StartSpawn && !Won)
            {
                foreach (IUnit unit in units)
                {
                    ((HostileMob)unit).Update(gameTime);
                    if (((HostileMob)unit).State == WorldManager.TileHandlerLibrary.tileState.dead) clearUnits.Add(unit);
                }
                foreach(IUnit unit in clearUnits)
                {
                    units.Remove(unit);
                }
                clearUnits.Clear();
                if (waveSpawnTimer / 1000 >= waveSpawnTime)
                {
                    waveSpawnTimer = 0;
                    BasicWaveStart();
                }
            }
            base.Update(gameTime);
        }
        public void BasicWaveStart()
        {
            List<IUnit> tempUnits = new List<IUnit>();
            tempUnits.Add(new HeadlessHorseman("Headless Horseman", new Vector2(1), (Wave * 100) + 100, (Wave * 100) + 100, new Vector2(318, 120), BaseUnitState.Idle, TextureValue.HeadlessHorseman, Color.Red, TextureValue.HeadlessHorseman, world, 1, TeamStats));
            tempUnits.Add(new Mage("Mage", new Vector2(1), (Wave * 50) + 100, (Wave * 100) + 100, new Vector2(318, 113), BaseUnitState.Idle, TextureValue.Mage, Color.Red, TextureValue.Mage, world, projectileManager, 10, TeamStats));

            foreach (IUnit unit in tempUnits)
            {
                ((HostileMob)unit).SetTeam(CommandComponent.ID + 1);
            }
            WaveDetails wave = new WaveDetails(tempUnits);
            StartWave(wave);
        }
        public void StartWave(WaveDetails wave)
        {
            Wave++;
            foreach (Tile tile in world.GetTiles("Portal"))
            {
                SummonWave(wave, (Building)tile);
            }
        }

        private void SummonWave(WaveDetails wave, Building building)
        {
            units.AddRange(wave.GetUnits(building));
            world.AddMobs(units);
        }
    }
}
