using System;
using System.Collections.Generic;
using _0x46696E616C.MobHandler.Units;

namespace MobHandler.HostileMobManager
{
    public class WaveDetails
    {
        List<IUnit> WaveUnits;
        public WaveDetails(List<IUnit> units)
        {
            WaveUnits = new List<IUnit>();
            WaveUnits.AddRange(units);
        }
        internal List<IUnit> GetUnits()
        {
            return WaveUnits;
        }
    }
}