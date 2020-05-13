using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0x46696E616C.MobHandler;
using _0x46696E616C.MobHandler.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NationBuilder.TileHandlerLibrary;

namespace _0x46696E616C.UIComponents
{
    class ImageBoxHealth : ImageBox
    {
        IEntity entity;
        public ImageBoxHealth(Texture2D texture, Vector2 position, Point size, Color color, IEntity entity) : base(texture, position, size, color)
        {
            this.entity = entity;
        }
        public override string Description()
        {
            return $"{entity.CurrentHealth}/{entity.TotalHealth}";
        }
    }
}
