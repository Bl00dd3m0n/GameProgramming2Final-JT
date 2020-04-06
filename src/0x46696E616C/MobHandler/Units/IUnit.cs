﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.MobHandler.Units
{
    public enum BaseUnitState { flee, attack, build, harvest }
    public interface IUnit : IEntity
    {
        BaseUnitState State { get; }

        void QueueBuild();
    }
}