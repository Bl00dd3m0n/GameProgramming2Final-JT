using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using MyVector2 = NationBuilder.TileHandlerLibrary.Vector2;
using _0x46696E616C.WorldManager.Resources;

namespace _0x46696E616C.Buildings
{
    class SolarPanel : Building
    {
        public SolarPanel(TextureValue texture, MyVector2 position) : base(texture, position)
        {
            Cost = new Wallet<IResource>();
            name = "Solar Panel";
            Position = position.ToMonoGameVector2();
            Size = new Vector2(0, 0);
            TotalHealth = 0;
            CurrentHealth = 0;
        }
    }
}
