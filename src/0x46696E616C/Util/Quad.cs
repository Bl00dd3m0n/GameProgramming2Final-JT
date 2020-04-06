using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoVector2 = Microsoft.Xna.Framework.Vector2;

namespace Util
{
    public class Quad
    {
        public VertexPositionTexture[] QuadVert { get; private set; }
        public Quad(int x, int y)
        {
            QuadVert = new VertexPositionTexture[6];
            QuadVert[0] = new VertexPositionTexture(new Vector3(x + 1f, y + 1f, 0), new MonoVector2(0f, 0f));
            QuadVert[1] = new VertexPositionTexture(new Vector3(x + 1f, y - 1f, 0), new MonoVector2(0f, 1f));
            QuadVert[2] = new VertexPositionTexture(new Vector3(x - 1f, y + 1f, 0), new MonoVector2(1f, 0f));
            QuadVert[3] = QuadVert[1];
            QuadVert[4] = new VertexPositionTexture(new Vector3(x - 1f, y - 1f, 0), new MonoVector2(1f, 1f));
            QuadVert[5] = QuadVert[2];
        }
    }
}
