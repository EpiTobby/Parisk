using System;
using System.Collections.Generic;
using Parisk;

namespace DefaultNamespace
{
    public class Player
    {
        public Side Side { get; }

        public Player(Side side)
        {
            Side = side;
        }
    }
}