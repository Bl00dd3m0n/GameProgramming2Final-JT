using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveManager
{
    public class MyColor
    {
        Microsoft.Xna.Framework.Color color;
        private int v;

        public int R
        {
            get { return color.R; }
            set { color = new Microsoft.Xna.Framework.Color(value, color.G, color.B, color.A); }
        }
        public int G
        {
            get { return color.G; }
            set { color = new Microsoft.Xna.Framework.Color(color.R, value, color.B, color.A); }
        }
        public int B
        {
            get { return color.B; }
            set { color = new Microsoft.Xna.Framework.Color(color.R, color.G, value, color.A); }
        }
        public int A
        {
            get { return color.A; }
            set { color = new Microsoft.Xna.Framework.Color(color.R, color.G, color.B, value); }
        }
        public Microsoft.Xna.Framework.Color ToMonoGameColor()
        {
            return color;
        }
        public MyColor() : this(0,0,0,0)
        {

        }
        public MyColor(int R, int G, int B) : this(R,G,B,255)
        {

        }

        public MyColor(int r, int g, int b, int a)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }

        public static implicit operator Microsoft.Xna.Framework.Color(MyColor color)
        {
            return new Microsoft.Xna.Framework.Color(color.R, color.G, color.B, color.A);
        }

        public static implicit operator MyColor(Microsoft.Xna.Framework.Color color)
        {
            return new MyColor(color.R, color.G, color.B, color.A);
        }
    }
}
