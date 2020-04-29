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
        public ImageBox(TextureValue texture, Vector2 position, Point size, Color color) : base(position, size, color)
        {
            this.picture = ContentHandler.DrawnTexture(texture);
        }
        public ImageBox(Texture2D texture, Vector2 position, Point size, Color color) : base(position, size, color)
        {
            this.picture = texture;
        }
    }
}
