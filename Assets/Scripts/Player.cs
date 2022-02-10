using System;
using System.Collections.Generic;
using Parisk;

namespace DefaultNamespace
{
    public class Player
    {
        private Side Side { get; }

        public Player(Side side)
        {
            Side = side;
        }
    }
}