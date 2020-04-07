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
        //Background Textures
        public static Texture2D grass;
        public static Texture2D sand;
        public static Texture2D water;
        public static Texture2D rock;
        //ResourceTextures
        public static Texture2D tree;
        //Building Textures
        public static Texture2D Center;
        public static Texture2D FireWall;
        public static Texture2D InternetCafe;
        public static Texture2D Lab;
        public static Texture2D MediaCenter;
        public static Texture2D Mines;
        public static Texture2D ServerFarm;
        public static Texture2D SolarPanel;
        //Util Content
        public static Texture2D cursor;
        public static SpriteFont font;
        public static bool LoadContent(Game game)
        {
            try
            {
                //Background Textures
                grass = SetTexture(TextureValue.Grass,game);
                sand = SetTexture(TextureValue.Sand,game);
                water = SetTexture(TextureValue.Water,game);
                rock = SetTexture(TextureValue.Stone,game);
                //ResourceTextures
                tree = SetTexture(TextureValue.Tree, game);
                //Building Textures
                //Center = SetTexture(TextureValue.Center, game);
                //FireWall = SetTexture(TextureValue.FireWall, game);
                //InternetCafe = SetTexture(TextureValue.InternetCafe, game);
                //Lab = SetTexture(TextureValue.Lab, game);
               // MediaCenter = SetTexture(TextureValue.MediaCenter, game);
               // Mines = SetTexture(TextureValue.Mines, game);
                //ServerFarm = SetTexture(TextureValue.ServerFarm, game);
                SolarPanel = SetTexture(TextureValue.SolarPanel, game);

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

                case TextureValue.Center:
                    return SolarPanel;
                case TextureValue.FireWall:
                    return SolarPanel;
                case TextureValue.InternetCafe:
                    return SolarPanel;
                case TextureValue.Lab:
                    return SolarPanel;
                case TextureValue.MediaCenter:
                    return SolarPanel;
                case TextureValue.Mines:
                    return SolarPanel;
                case TextureValue.ServerFarm:
                    return SolarPanel;
                case TextureValue.SolarPanel:
                    return solarPanel;
                case TextureValue.IronVein:
                    return iron;
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
