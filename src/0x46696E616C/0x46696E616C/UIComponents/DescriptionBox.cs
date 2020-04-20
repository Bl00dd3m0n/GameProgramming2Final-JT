using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject;

namespace _0x46696E616C.UIComponents
{
    class DescriptionBox : Component
    {
        public string description { get; set; }

        public DescriptionBox(Point Size) : base(new Vector2(0), Size, Color.Navy)
        {
            drawComponent = false;
        }
    }
}
