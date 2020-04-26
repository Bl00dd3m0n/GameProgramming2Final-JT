using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechHandler;

namespace _0x46696E616C.TechManager.Technologies
{
    public class Technology : IQueueable<TextureValue>
    {
        public TextureValue Icon { get; protected set; }

        public Vector2 Position { get; protected set; }

        public Technology(TextureValue icon, Vector2 position)
        {
            this.Icon = icon;
            this.Position = position;
        }
    }
}
