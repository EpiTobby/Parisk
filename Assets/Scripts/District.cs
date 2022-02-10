using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Parisk;
using UnityEngine;

public class District : MonoBehaviour
{
    [SerializeField]
    private int number = 0;
    
    [SerializeReference]
    private List<Building> buildings = null;
    
    private Player _owner = null;

    private int _inertiaPoints = 0;
    
    private ControlPointContainer _pointContainer = ControlPointContainer.InitializeRandom();
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("District " + number);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        Debug.Log("Mouse over District " + number);
    }

    public String getBuildings()
    {
        String res = "";
        foreach(Building building in buildings)
        {
            res += building.getName() + '\n';
        }
        return res;
    }

    /**
     * Do an election an set the new owner of this district
     */
    public ElectionsResult DoElections()
    {
        var result = PredictElections();
        _owner = result.Side == null 
            ? null 
            : GameController.Get().GetPlayer(result.Side.Value);
        return result;
    }

    /**
     * Compute the result of an election
     */
    private ElectionsResult PredictElections()
    {
        if (_owner != null)
        {
            if (_pointContainer.GetPointsFor(_owner.Side) > _pointContainer.GetPointsFor(_owner.Side.GetOpposite()))
                return new ElectionsResult(_owner.Side, ElectionsResultType.Maintain);
        }
        Side winningSide;
        if (_pointContainer.GetPointsFor(Side.Communards) > 50)
            winningSide = Side.Communards;
        else if (_pointContainer.GetPointsFor(Side.Versaillais) > 50)
            winningSide = Side.Versaillais;
        else
            return new ElectionsResult(null, ElectionsResultType.Draw);

        return new ElectionsResult(winningSide, _owner != null ? ElectionsResultType.Reversal : ElectionsResultType.Win);
    }

    private void setOwner(Player newOwner)
    {
        _owner = newOwner;
    }

    public Player getOwner()
    {
        return _owner;
    }


    public void UpdateControlPointsOnEvent(int amount, bool adding)
    {
        if (_owner == null)
            return;
        
        _pointContainer.AddPointsTo(adding ? _owner.Side : _owner.Side.GetOpposite(), amount);
    }

    public void UpdateInertiaPointsOnEvent(int amount, bool adding)
    {
        if (_owner == null)
            return;

        _inertiaPoints = adding ? _inertiaPoints + amount : Math.Max(_inertiaPoints - amount, 0);
    }

    public void DestroyBuildingOnEvent(String buildingName)
    {
        if (_owner == null || _owner.Side != Side.Communards)
            return;
        
        _pointContainer.UpdatePointsOnDestroyBuildingEvent();

        buildings.RemoveAll(building => building.getName() == buildingName);
    }

    public ControlPointContainer getPointController()
    {
        return _pointContainer;
    }
}