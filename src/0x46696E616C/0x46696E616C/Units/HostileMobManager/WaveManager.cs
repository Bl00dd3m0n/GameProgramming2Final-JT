﻿using _0x46696E616C.CommandPattern;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.MobHandler.Units;
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
        ProjectileManager projectileManager;
        private float waveSpawnTimer;
        private float waveSpawnTime;
        public float TimeTillSpawn { get { return waveSpawnTime - (waveSpawnTimer / 1000); } }
        public WaveManager(Game game, WorldHandler world, ProjectileManager projectileManager) : base(game)
        {
            this.world = world;
            Wave = 0;
            this.projectileManager = projectileManager;
            units = new List<IUnit>();
            waveSpawnTime = 5f;
        }

        public override void Update(GameTime gameTime)
        {
            waveSpawnTimer += gameTime.ElapsedGameTime.Milliseconds;
            foreach(IUnit unit in units)
            {
                ((BasicUnit)unit).Update(gameTime);
            }
            if(waveSpawnTimer/1000 >= waveSpawnTime /*&& units.Count < 5*/)//TODO the game seems to freeze when you get more than 5 units...need to implement a better move system but I'm too tired at the moment to fix it.
            {
                waveSpawnTimer = 0;
                BasicWaveStart();
            }
            base.Update(gameTime);
        }
        public void BasicWaveStart()
        {
            List<IUnit> tempUnits = new List<IUnit>();
            //tempUnits.Add(new HeadlessHorseman("Headless Horseman", new Vector2(1), 1000, 1000, new Vector2(318, 120), BaseUnitState.Idle, TextureValue.HeadlessHorseman, Color.Red, TextureValue.HeadlessHorseman, world, 1));
            tempUnits.Add(new Mage("Mage", new Vector2(1), 1000, 1000, new Vector2(318, 113), BaseUnitState.Idle, TextureValue.Mage, Color.Red, TextureValue.Mage, world, projectileManager, 10));

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
                SummonWave(wave);
            }
        }

        private void SummonWave(WaveDetails wave)
        {
            units.AddRange(wave.GetUnits());
            world.AddMobs(units);
            foreach(IUnit unit in units)
            {
                ((HostileMob)unit).FindTarget();
            }
        }
    }
}
