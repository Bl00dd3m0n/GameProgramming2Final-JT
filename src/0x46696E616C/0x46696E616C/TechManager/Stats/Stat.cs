using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.TechManager.Stats
{
    public abstract class Stat
    {
        string name;
        float value;
        TextureValue statIcon;
        public float Value { get { return value; } set { this.value = value; } }
        public string Name { get { return name; } }
        public TextureValue Texture { get { return statIcon; } set { statIcon = value; } }
        public Stat(string name, float value)
        {
            this.name = name;
            this.value = value;
        }
        public void Upgrade(float amount)
        {
            value += amount;
        }
        public static Stat operator +(Stat thisStat, Stat otherStat) { thisStat.value += otherStat.value; return thisStat; }
        public static float operator +(Stat thisStat, float otherStat) { return thisStat.value + otherStat; }
    }
}
