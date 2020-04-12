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
        //ResourceTextures
        Tree,
        IronVein,
        //Background Textures
        Grass,
        Water,
        Sand,
        Stone,
        //Building Textures
        Center,
        FireWall,
        InternetCafe,
        Lab,
        MediaCenter,
        Mines,
        ServerFarm,
        SolarPanel,
        SteelFactory,

        //Units
        Civilian,

        None,
        Cursor,
        PowerSupply,

        //Icons
        CenterIcon,
        FireWallIcon,
        InternetCafeIcon,
        LabIcon,
        MediaCenterIcon,
        MinesIcon,
        ServerFarmIcon,
        SolarPanelIcon,
        SteelFactoryIcon,
        PowerSupplyIcon,
    }
    [Serializable]
    public struct BlockData
    {
        public TextureValue texture;
    }
}
