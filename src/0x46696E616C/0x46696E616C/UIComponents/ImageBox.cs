using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NationBuilder.DataHandlerLibrary;
using NationBuilder.TileHandlerLibrary;
using UIProject;

namespace _0x46696E616C.UIComponents
{
    class ImageBox : Component
    {
        TextureValue texture { get; set; }
        public ImageBox(TextureValue texture, Vector2 position, Point size, Color color) : base(position, size, color)
        {
            this.picture = ContentHandler.DrawnTexture(texture);
            this.texture = texture;
        }
        public ImageBox(Texture2D texture, Vector2 position, Point size, Color color) : base(position, size, color)
        {
            this.picture = texture;
        }
        public override string Description()
        {
            switch(texture)
            {
                case TextureValue.Damage:
                    return "Attack Power";
                case TextureValue.BuildPower:
                    return "Build Power";
                case TextureValue.HarvestPower:
                    return "Harvest Power";
                case TextureValue.Range:
                    return "Range";
                case TextureValue.Chest:
                    return "Carry Capacity";
                default:
                    return "";
            }
        }
    }
}
