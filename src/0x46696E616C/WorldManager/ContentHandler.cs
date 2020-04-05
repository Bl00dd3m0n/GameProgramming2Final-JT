using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationBuilder.DataHandlerLibrary
{
    
    public static class ContentHandler
    {
        public static Texture2D grass;
        public static Texture2D sand;
        public static Texture2D water;
        public static Texture2D rock;
        public static Texture2D tree;
        public static Texture2D solarPanel;
        public static Texture2D cursor;
        public static SpriteFont font;
        public static bool LoadContent(Game game)
        {
            try
            {
                grass = SetTexture(TextureValue.Grass,game);
                sand = SetTexture(TextureValue.Sand,game);
                water = SetTexture(TextureValue.Water,game);
                rock = SetTexture(TextureValue.Stone,game);
                tree = SetTexture(TextureValue.Tree,game);
                solarPanel = SetTexture(TextureValue.SolarPanel, game);
                cursor = SetTexture(TextureValue.Cursor, game);
                font = game.Content.Load<SpriteFont>("Ariel");
                return true;
            } catch(Exception ex)
            {
                string ErrorMessage = ex.GetType().Name;
                return false;
            }
        }
        public static Texture2D DrawnTexture(TextureValue value)
        {
            switch(value)
            {
                case TextureValue.Sand:
                    return sand;
                case TextureValue.Water:
                    return water;
                case TextureValue.Stone:
                    return rock;
                case TextureValue.Tree:
                    return tree;
                case TextureValue.SolarPanel:
                    return solarPanel;
                case TextureValue.Cursor:
                    return cursor;
                default:
                    return grass;
            }
        }
        private static Texture2D SetTexture(TextureValue texture,Game game)
        {
            return game.Content.Load<Texture2D>(texture.ToString());
        }
    }
}
