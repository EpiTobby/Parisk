using System.Collections.Generic;
using Parisk;

public class ControlPointContainer
{
    private Dictionary<Side, int> _points;
    
    private ControlPointContainer(Dictionary<Side, int> points)
    {
        _points = points;
    }

    public static ControlPointContainer InitializeRandom()
    {
        Dictionary<Side,int> dictionary = new Dictionary<Side, int>();
        dictionary.Add(Side.Communards, 0); // FIXME (random)
        dictionary.Add(Side.Versaillais, 0); // FIXME (random)
        return new ControlPointContainer(dictionary);
    }
}
