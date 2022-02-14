using System;
using System.Collections.Generic;
using Parisk;
using TMPro;

public class ControlPointContainer
{
    private Dictionary<Side, int> _points;
    private TMP_Text _versaillaisPoints = null;
    private TMP_Text _communardsPoints = null;
    
    private const int MinRandom = 10;
    private const int MaxRandom = 30;

    private const int InitialPoints = 70;
    
    private int _communardRandom;
    private int _versaillaisRandom;

    private ControlPointContainer(Dictionary<Side, int> points)
    {
        _points = points;
        _versaillaisRandom = _points[Side.Versaillais];
        _communardRandom = _points[Side.Communards];
        
    }

    public static ControlPointContainer InitializeRandom()
    {
        Dictionary<Side,int> dictionary = new Dictionary<Side, int>();
        
        dictionary.Add(Side.Communards, new Random().Next(MinRandom, MaxRandom));
        dictionary.Add(Side.Versaillais, new Random().Next(MinRandom, MaxRandom)); 
        return new ControlPointContainer(dictionary);
    }

    public void SetVersaillaisPoints(TMP_Text versaillaisPoints)
    {
        _versaillaisPoints = versaillaisPoints;
    }

    public void SetCommunardsPoints(TMP_Text communardsPoints)
    {
        _communardsPoints = communardsPoints;
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
        SetSidePointText(side, amount);

        Side adversary = side.GetOpposite();
        switch (source)
        {
            case PointSource.Adversary:
            {
                _points[adversary] = Math.Max(0, _points[adversary] - amount);
                SetSidePointText(side.GetOpposite(), -amount);
                break;
            }
            case PointSource.Absenteeism:
            {
                if (GetAbsenteeism() < 0)
                {
                    _points[adversary] += GetAbsenteeism();
                    SetSidePointText(side.GetOpposite(), GetAbsenteeism());
                }
                else
                {
                    SetSidePointText(side.GetOpposite(), 0);
                }
                break;
            }
            case PointSource.Mixed:
            {
                _points[adversary] = Math.Max(0, _points[adversary] - amount / 2);
                if (GetAbsenteeism() < 0)
                {
                    _points[adversary] += GetAbsenteeism();
                    SetSidePointText(side.GetOpposite(), -(amount / 2) + GetAbsenteeism());
                }
                else
                {
                    SetSidePointText(side.GetOpposite(), -(amount / 2));
                }
                break;
            }
            default:
                throw new Exception();
        }
    }

    private void SetSidePointText(Side side, int amount)
    {
        var text = amount < 0 ? amount.ToString() : "+" + amount.ToString();
        if (side == Side.Communards)
            _communardsPoints.text = text;
        else
            _versaillaisPoints.text = text;
    }

    public void RemovePointsTo(Side side, int amount)
    {
        _points[side] = Math.Max(0, _points[side] - amount);
        SetSidePointText(side, -amount);
        SetSidePointText(side.GetOpposite(), 0);
    }

    public void UpdatePointsOnDestroyBuildingEvent()
    {
        _points[Side.Versaillais] =
            Math.Max(_points[Side.Versaillais] - Convert.ToInt32(EventCost.DestroyBuildingOnEvent), 0);
    }
    
    public int GetCommunardRandomPoints()
    {
        return _communardRandom;
    }

    public int GetVersaillaisRandomPoints()
    {
        return _versaillaisRandom;
    }

    public void SetInitialPoints(Side side)
    {
        _points[side] = InitialPoints;
    }
}

