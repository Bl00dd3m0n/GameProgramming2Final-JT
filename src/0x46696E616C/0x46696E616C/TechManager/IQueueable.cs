using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechHandler
{
    public interface IQueueable<T>
    {
        T Icon { get; }
        Vector2 Position { get; }
    }
}
