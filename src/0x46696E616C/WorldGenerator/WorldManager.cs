using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationBuilder.WorldHandlerLibrary
{
    class WorldManager : GameComponent
    {
        List<Region> regions;
        Vector2 Spawn;
        //Add mob handler
        //If multiplayer added players should be added here
        Player player;
        WorldGeneration worldGen;
        public WorldManager(Game game) : base(game)
        {
            worldGen = new WorldGeneration(game);
            //worldGen.
            regions = new List<Region>();
        }

        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }
    }
}
