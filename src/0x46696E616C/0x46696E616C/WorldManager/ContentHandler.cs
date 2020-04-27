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
        private static Dictionary<TextureValue, Texture2D> textures;
        public static SpriteFont Font;
        internal static void Clear()
        {
            textures.Clear();
            Font = null;
        }
        public static bool LoadContent(Game game)
        {
            textures = new Dictionary<TextureValue, Texture2D>();
            try
            {
                //ResourceTextures
                textures.Add(TextureValue.Tree, SetTexture(TextureValue.Tree, game));
                textures.Add(TextureValue.IronVein, SetTexture(TextureValue.IronVein, game));
                //Background Textures
                textures.Add(TextureValue.Grass, SetTexture(TextureValue.Grass, game));
                textures.Add(TextureValue.Sand, SetTexture(TextureValue.Sand, game));
                textures.Add(TextureValue.Water, SetTexture(TextureValue.Water, game));
                textures.Add(TextureValue.Stone, SetTexture(TextureValue.Stone, game));
                //Building Textures
                textures.Add(TextureValue.Center, SetTexture(TextureValue.Center, game));
                textures.Add(TextureValue.FireWall, SetTexture(TextureValue.FireWall, game));
                textures.Add(TextureValue.InternetCafe, SetTexture(TextureValue.InternetCafe, game));
                textures.Add(TextureValue.Lab, SetTexture(TextureValue.Lab, game));
                textures.Add(TextureValue.MediaCenter, SetTexture(TextureValue.MediaCenter, game));
                textures.Add(TextureValue.Mines, SetTexture(TextureValue.Mines, game));
                textures.Add(TextureValue.PowerSupply, SetTexture(TextureValue.PowerSupply, game));
                textures.Add(TextureValue.ServerFarm, SetTexture(TextureValue.ServerFarm, game));
                textures.Add(TextureValue.SolarPanel, SetTexture(TextureValue.SolarPanel, game));
                textures.Add(TextureValue.SteelFactory, SetTexture(TextureValue.SteelFactory, game));
                textures.Add(TextureValue.Portal, SetTexture(TextureValue.Portal, game));
                //Unit Textures
                textures.Add(TextureValue.Civilian, SetTexture(TextureValue.Civilian, game));
                textures.Add(TextureValue.HeadlessHorseman, SetTexture(TextureValue.HeadlessHorseman, game));
                textures.Add(TextureValue.Mage, SetTexture(TextureValue.Mage, game));
                //Projectiles
                textures.Add(TextureValue.FireBall, textures[TextureValue.FireWall]);
                //Icon Textures
                textures.Add(TextureValue.CenterIcon, SetTexture(TextureValue.CenterIcon, game));
                textures.Add(TextureValue.FireWallIcon, SetTexture(TextureValue.FireWallIcon, game));
                textures.Add(TextureValue.InternetCafeIcon, SetTexture(TextureValue.InternetCafeIcon, game));
                textures.Add(TextureValue.LabIcon, SetTexture(TextureValue.LabIcon, game));
                textures.Add(TextureValue.MediaCenterIcon, SetTexture(TextureValue.MediaCenterIcon, game));
                textures.Add(TextureValue.MinesIcon, SetTexture(TextureValue.MinesIcon, game));
                textures.Add(TextureValue.PowerSupplyIcon, SetTexture(TextureValue.PowerSupplyIcon, game));
                textures.Add(TextureValue.ServerFarmIcon, SetTexture(TextureValue.ServerFarmIcon, game));
                textures.Add(TextureValue.SolarPanelIcon, SetTexture(TextureValue.SolarPanelIcon, game));
                textures.Add(TextureValue.SteelFactoryIcon, SetTexture(TextureValue.SteelFactoryIcon, game));
                //Stats UI 

                //Util Content
                textures.Add(TextureValue.Cursor, SetTexture(TextureValue.Cursor, game));
                textures.Add(TextureValue.SpawnPoint, SetTexture(TextureValue.SpawnPoint, game));
                Font = game.Content.Load<SpriteFont>("Ariel");

                return true;
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.GetType().Name;
                return false;
            }
        }
        public static Texture2D DrawnTexture(TextureValue value)
        {
            if (textures[value] != null) return textures[value];
            else return textures[TextureValue.Grass];
        }
        private static Texture2D SetTexture(TextureValue texture, Game game)
        {
            return game.Content.Load<Texture2D>(texture.ToString());
        }
    }
}
