﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ragnarok.feeder.everest;

namespace Ragnarok.feeder
{
    class FeederFactory
    {
        public static IFeeder getFeeder()
        {
            return new EverestFeeder();
        }
    }
}
