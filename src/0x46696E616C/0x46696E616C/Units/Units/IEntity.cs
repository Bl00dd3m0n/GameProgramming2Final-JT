using _0x46696E616C.TechManager.Stats;
using _0x46696E616C.Units;
using Microsoft.Xna.Framework;
using MobHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.MobHandler.Units
{
    public interface IEntity : IDamagable
    {
        string name { get; }
        Vector2 Position { get; }
        Vector2 Size { get; }
        HealthBar healthBar { get; }
        Stats stats { get; }
    }
}
