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
        public static Texture2D Grass;
        public static Texture2D Sand;
        public static Texture2D Water;
        public static Texture2D Rock;
        //ResourceTextures
        public static Texture2D Tree;
        public static Texture2D Iron;
        //Building Textures
        public static Texture2D Center;
        public static Texture2D FireWall;
        public static Texture2D InternetCafe;
        public static Texture2D Lab;
        public static Texture2D MediaCenter;
        public static Texture2D Mines;
        public static Texture2D ServerFarm;
        public static Texture2D SolarPanel;
        //Unit Textures
        public static Texture2D Civilian;
        //Util Content
        public static Texture2D Cursor;
        public static SpriteFont Font;
        public static bool LoadContent(Game game)
        {
            try
            {
                //Background Textures
                Grass = SetTexture(TextureValue.Grass,game);
                Sand = SetTexture(TextureValue.Sand,game);
                Water = SetTexture(TextureValue.Water,game);
                Rock = SetTexture(TextureValue.Stone,game);
                //ResourceTextures
                Tree = SetTexture(TextureValue.Tree, game);
                Iron = SetTexture(TextureValue.IronVein, game);
                //Building Textures
                Center = SetTexture(TextureValue.Center, game);
                //FireWall = SetTexture(TextureValue.FireWall, game);
                //InternetCafe = SetTexture(TextureValue.InternetCafe, game);
                //Lab = SetTexture(TextureValue.Lab, game);
               // MediaCenter = SetTexture(TextureValue.MediaCenter, game);
               // Mines = SetTexture(TextureValue.Mines, game);
                //ServerFarm = SetTexture(TextureValue.ServerFarm, game);
                SolarPanel = SetTexture(TextureValue.SolarPanel, game);
                //Unit Textures
                Civilian = SetTexture(TextureValue.Civilian, game);
                //Util Content
                Cursor = SetTexture(TextureValue.Cursor, game);
                Font = game.Content.Load<SpriteFont>("Ariel");
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
                //BackgroundBlocks
                case TextureValue.Sand:
                    return Sand;
                case TextureValue.Water:
                    return Water;
                case TextureValue.Stone:
                    return Rock;
                //Resources
                case TextureValue.Tree:
                    return Tree;
                case TextureValue.IronVein:
                    return Iron;
                    //Buildings
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
                    return SolarPanel;
                case TextureValue.Civilian:
                    return Civilian;
                    //Util
                case TextureValue.Cursor:
                    return Cursor;
                default:
                    return Grass;
            }
        }
        private static Texture2D SetTexture(TextureValue texture, Game game)
        {
            return game.Content.Load<Texture2D>(texture.ToString());
        }
    }
}
