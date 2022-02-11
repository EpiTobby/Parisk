namespace Parisk
{
    public class ElectionsResult
    {
        public Side? Side { get; }
        public ElectionsResultType Type { get; }

        public ElectionsResult(Side? side, ElectionsResultType type)
        {
            Side = side;
            Type = type;
        }
    }

    public enum ElectionsResultType
    {
        Draw,
        Reversal,
        Maintain,
        Win,
    }
}