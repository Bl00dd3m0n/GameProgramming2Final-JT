using _0x46696E616C.MobHandler.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.MobHandler
{
    public class HealthBar
    {
        public Texture2D Health { get; private set; }
        public Rectangle Bounds { get; protected set; }
        public Vector2 Position { get { return Bounds.Location.ToVector2(); } set { Bounds = new Rectangle(value.ToPoint(), Bounds.Size); } }
        public float scale { get; set; }
        public HealthBar(Rectangle bounds)
        {
            this.Bounds = bounds;
        }
        /// <summary>
        /// makes a new health bar based on the tiles health
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="gd"></param>
        public void UpdateHealth(IEntity entity, GraphicsDevice gd)
        {
            if (entity.healthBar.Bounds.Width > 0 && entity.healthBar.Bounds.Height > 0)
            {
                Color[] healthBar = new Color[Bounds.Width * Bounds.Height];
                float percentHealth = entity.CurrentHealth / entity.TotalHealth;
                for (int y = 0; y < Bounds.Height; y++)
                {
                    for (int x = 0; x < Bounds.Width; x++)
                    {
                        if(x == 0 || y == 0 || x== Bounds.Width-1 || y == Bounds.Height-1) healthBar[x + y * Bounds.Width] = Color.Blue;
                        else if ((float)x / (float)Bounds.Width >= percentHealth) healthBar[x + y * Bounds.Width] = Color.Red;
                        else healthBar[x + y * Bounds.Width] = Color.SpringGreen;
                    }
                }
                if (Health == null)
                {
                    Health = new Texture2D(gd, Bounds.Width, Bounds.Height);
                }
                Health.SetData(healthBar, 0, Bounds.Width * Bounds.Height);
            }
        }
    }
}
