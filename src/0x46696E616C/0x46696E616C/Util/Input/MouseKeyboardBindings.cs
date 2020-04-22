using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.Util.Input
{
        
    public enum MouseInput { LeftClick, RightClick, ScrollUp, ScrollDown}
    public class MouseKeyboardBindings
    {
        public bool isMouseType { get; private set; }
        public MouseInput mouse { get; set; }
        public Keys keys { get; set; }
        public MouseKeyboardBindings()
        {

        }
        public MouseKeyboardBindings(Keys key)
        {
            this.keys = key;
        }
        public MouseKeyboardBindings(MouseInput mouse)
        {
            this.mouse = mouse;
            isMouseType = true;
        }
    }
}
