﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechHandler
{
    public interface ITechObserver
    {
        void Update(ITech tech);
    }
}
