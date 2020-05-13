using _0x46696E616C.WorldManager.MapData;
using Microsoft.Xna.Framework;
using WorldManager;

namespace Tutorial
{
    internal class TutorialWorld : WorldHandler
    {
        public TutorialWorld(Game game, string WorldName) : base(game, WorldName)
        {
            map = new TutorialMap(game, new Vector2(60,60), 0);
        }
    }
}