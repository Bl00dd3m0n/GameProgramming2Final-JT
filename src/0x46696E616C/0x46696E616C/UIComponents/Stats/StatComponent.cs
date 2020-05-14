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
        public Type statType { get; private set; }
        public float value;
        public StatComponent(Component component, Type stat, float value)
        {
            this.component = component;
            this.statType = stat;
            this.value = value;
        }
    }
}
