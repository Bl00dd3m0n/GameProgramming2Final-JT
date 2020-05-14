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
        Portal,
        //Units
        Civilian,
        Ballista,
        Priest,

        None,
        Cursor,
        PowerSupply,
        SpawnPoint,

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

        //Hostile Mobs
        HeadlessHorseman,
        Mage,

        //Projectiles
        FireBall,
        Arrow,

        //Stats UI
        Chest,
        Damage,
        Range,
        HarvestPower,
        BuildPower,
        Health,
        Overlay,
    }
    [Serializable]
    public struct BlockData
    {
        public TextureValue texture;
    }
}
