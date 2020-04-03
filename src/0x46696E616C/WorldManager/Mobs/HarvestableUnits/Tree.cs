using _0x46696E616C;
using _0x46696E616C.MobHandler;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobHandler.Units
{
    class Tree : IHarvestable
    {
        IResource resource;
        public string name { get; private set; }

        public Vector2 Position { get; private set; }

        public Vector2 Size => throw new NotImplementedException();

        public float TotalHealth => throw new NotImplementedException();

        public float CurrentHealth => throw new NotImplementedException();

        Type IHarvestable.type => throw new NotImplementedException();

        public IHarvestable()
        {

        }


    }
}
