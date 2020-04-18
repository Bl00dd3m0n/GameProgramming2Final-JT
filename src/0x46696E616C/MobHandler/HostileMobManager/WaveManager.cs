using _0x46696E616C.MobHandler.Units;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobHandler.HostileMobManager
{
    class WaveManager : GameComponent
    {
        public int Wave { get; private set; }
        public List<Vector2> SpawnPoints;
        private List<IUnit> units;
        public WaveManager(Game game) : base(game)
        {

        }
        public void StartWave(WaveDetails wave)
        {
            Wave++;
            foreach (Vector2 point in SpawnPoints)
            {
                SummonWave(wave);
            }
        }

        private void SummonWave(WaveDetails wave)
        {
            units = wave.GetUnits();

        }
    }
}
