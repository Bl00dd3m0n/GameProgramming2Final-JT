

using _0x46696E616C.MobHandler.Units;
using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using WorldManager;

namespace _0x46696E616C.Units.HostileMobManager
{
    class Mage : HostileMob
    {
        public Mage(Game game, string name, Vector2 size, float totalHealth, float currentHealth, Vector2 position, BaseUnitState state, TextureValue texture, Color color, TextureValue icon, WorldHandler world) : base(game, name, size, totalHealth, currentHealth, position, state, texture, color, icon, world)
        {
        }
    }
}
