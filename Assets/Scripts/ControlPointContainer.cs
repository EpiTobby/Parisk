using System;
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

    public int GetPointsFor(Side side)
    {
        return _points[side];
    }

    public int GetAbsenteeism()
    {
        return 100 - GetPointsFor(Side.Communards) - GetPointsFor(Side.Versaillais);
    }

    public void AddPointsTo(Side side, int amount, PointSource source = PointSource.Mixed)
    {
        amount = Math.Min(amount, 100 - _points[side]);
        _points[side] += amount;

        Side adversary = side.GetOpposite();
        switch (source)
        {
            case PointSource.Adversary:
                _points[adversary] = Math.Max(0, _points[adversary] - amount);
                break;
            case PointSource.Absenteeism:
            {
                if (GetAbsenteeism() < 0)
                    _points[adversary] += GetAbsenteeism();
                break;
            }
            case PointSource.Mixed:
            {
                _points[adversary] = Math.Max(0, _points[adversary] - amount / 2);
                if (GetAbsenteeism() < 0)
                    _points[adversary] += GetAbsenteeism();
                break;
            }
            default:
                throw new Exception();
        }
    }

    public void UpdatePointsOnDestroyBuildingEvent()
    {
        _points[Side.Versaillais] =
            Math.Max(_points[Side.Versaillais] - Convert.ToInt32(EventCost.DestroyBuildingOnEvent), 0);
    }
 
    public void RemovePointsTo(Side side, int amount)
    {
        _points[side] = Math.Max(0, _points[side] - amount);
    }
}
