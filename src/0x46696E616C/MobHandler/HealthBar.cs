using _0x46696E616C.MobHandler.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobHandler
{
    class HealthBar
    {
        Texture2D Health;
        Rectangle Bounds;
        IUnit unit;
        public void UpdateHealth(int totalHealth, int currentHealth)
        {
            Color[] healthBar = new Color[Bounds.Width * Bounds.Height];
            float percentHealth = currentHealth / totalHealth;
            for (int y = 0; y < Bounds.Height; y++)
                for (int x = 0; x < Bounds.Width; x++)
                {
                    if (x / Bounds.Width >= percentHealth) healthBar[x + y * Bounds.Width] = Color.Green;
                    else healthBar[x + y * Bounds.Width] = Color.Red;
            }
            Health.SetData(healthBar, 0, Bounds.Width*Bounds.Height);
        }
    }
}
