using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0x46696E616C.TechManager.Stats;
using Microsoft.Xna.Framework;
using UIProject;

namespace _0x46696E616C.UIComponents.Stats
{
    class StatComponent : Component
    {
        public Component component { get; private set; }
        public Stat stat { get; private set; }
        public StatComponent(Component component, Stat stat)
        {
            this.component = component;
            this.stat = stat;
        }
    }
}
