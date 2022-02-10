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
    private Player owner = null;
    private ControlPointContainer pointContainer = ControlPointContainer.InitializeRandom();

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
        owner = result.Side == null 
            ? null 
            : GameController.Get().GetPlayer(result.Side.Value);
        return result;
    }

    /**
     * Compute the result of an election
     */
    private ElectionsResult PredictElections()
    {
        if (owner != null)
        {
            if (pointContainer.GetPointsFor(owner.Side) > pointContainer.GetPointsFor(owner.Side.GetOpposite()))
                return new ElectionsResult(owner.Side, ElectionsResultType.Maintain);
        }
        Side winningSide;
        if (pointContainer.GetPointsFor(Side.Communards) > 50)
            winningSide = Side.Communards;
        else if (pointContainer.GetPointsFor(Side.Versaillais) > 50)
            winningSide = Side.Versaillais;
        else
            return new ElectionsResult(null, ElectionsResultType.Draw);

        return new ElectionsResult(winningSide, owner != null ? ElectionsResultType.Reversal : ElectionsResultType.Win);
    }

    private void setOwner(Player newOwner)
    {
        owner = newOwner;
    }

    public Player getOwner()
    {
        return owner;
    }

    public ControlPointContainer getPointController()
    {
        return pointContainer;
    }
}
