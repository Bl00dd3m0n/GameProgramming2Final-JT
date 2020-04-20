using _0x46696E616C.CommandPattern;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.Units.HostileMobManager;
using Microsoft.Xna.Framework;
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
        public WaveManager(Game game, WorldHandler world) : base(game)
        {
            this.world = world;
            Wave = 0;
            List<IUnit> tempUnits = new List<IUnit>();
            tempUnits.Add(new HeadlessHorseman(game, "Headless Horseman", new Vector2(1), 1000, 1000, new Vector2(318, 120), BaseUnitState.Idle, TextureValue.HeadlessHorseman, Color.Red, TextureValue.HeadlessHorseman, world));
            tempUnits.Add(new Mage(game, "Mage", new Vector2(1), 1000, 1000, new Vector2(318, 120), BaseUnitState.Idle, TextureValue.Mage, Color.Red, TextureValue.Mage, world));
            ((HostileMob)tempUnits[0]).SetTeam(CommandComponent.ID+1);
            ((HostileMob)tempUnits[1]).SetTeam(CommandComponent.ID+1);
            WaveDetails wave = new WaveDetails(tempUnits);
            StartWave(wave);
        }

        public override void Update(GameTime gameTime)
        {
            foreach(IUnit unit in units)
            {
                if (unit is BasicUnit)//Always should be but just in case
                {
                    ((BasicUnit)unit).Update(gameTime);
                }
            }
            base.Update(gameTime);
        }

        public void StartWave(WaveDetails wave)
        {
            Wave++;
            //foreach (Tile tile in world.GetTiles("Portal"))
            //{
                SummonWave(wave);
            //}
        }

        private void SummonWave(WaveDetails wave)
        {
            units = wave.GetUnits();
            world.AddMobs(units);
            foreach(IUnit unit in units)
            {
                ((HostileMob)unit).FindTarget();
            }
        }
    }
}
