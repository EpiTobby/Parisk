using System;
using System.Collections.Generic;

namespace DefaultNamespace
{
    public class Player
    {
        public int score;
        private String name;

        public Player(int score, String name)
        {
            this.score = score;
            this.name = name;
        }
    }
}