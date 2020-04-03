using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationBuilder.TileHandlerLibrary
{
    [Serializable]
    public enum TextureValue
    {
        Grass,
        Tree,
        Water,
        Sand,
        Stone
    }
    [Serializable]
    public struct BlockData
    {
        public TextureValue texture;
    }
}
