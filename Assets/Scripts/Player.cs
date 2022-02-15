using System;
using System.Collections.Generic;
using Parisk;
using Parisk.Action;

namespace DefaultNamespace
{
    public class Player
    {
        public Side Side { get; }
        public UniqueActionGame UniqueActionGame { get; }
        public Dictionary<District, IAction> ExecutedActions { get; } = new Dictionary<District, IAction>();

        public Player(Side side)
        {
            Side = side;
            if (side == Side.Versaillais)
                UniqueActionGame = new GermanPact();
            else
                UniqueActionGame = new NationalGuardReinstatement();
        }
    }
}