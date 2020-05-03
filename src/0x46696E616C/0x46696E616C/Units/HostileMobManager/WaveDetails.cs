using System;
using System.Collections.Generic;
using _0x46696E616C.Buildings;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.Units.HostileMobManager;
using Microsoft.Xna.Framework;

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
        internal List<IUnit> GetUnits(Building building)
        {
            int lSide = -(WaveUnits.Count / 2);
            for (int i = 0; i < WaveUnits.Count;i++)
            {
                if (WaveUnits[i] is BasicUnit)
                {
                    WaveUnits[i] = ((BasicUnit)WaveUnits[i]).NewInstace(WaveUnits[i].CurrentHealth, building.GetSpawn() + new Vector2(lSide, 0));
                    lSide++;
                }
            }
            return WaveUnits;
        }
    }
}