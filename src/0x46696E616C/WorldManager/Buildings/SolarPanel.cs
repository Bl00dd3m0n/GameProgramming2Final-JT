﻿using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;

namespace _0x46696E616C.Buildings
{
    class SolarPanel : Building
    {
        public SolarPanel(Game game, TextureValue texture, Vector2 position) : base(game, texture, position)
        {
            Cost = new Wallet<IResource>();
            name = "Solar Panel";
            Position = position;
            Size = new Vector2(0, 0);
            TotalHealth = 0;
            CurrentHealth = 0;
        }
    }
}
