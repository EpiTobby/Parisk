using System;

namespace Parisk
{
    public enum Side
    {
        Communards,
        Versaillais,
    }

    public static class SideMethods
    {
        public static string GetName(this Side side)
        {
            return side switch
            {
                Side.Communards => "Communard",
                Side.Versaillais => "Versaillais",
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            };
        }

        public static Side GetOpposite(this Side side)
        {
            return side == Side.Communards ? Side.Versaillais : Side.Communards;
        }
    }
}