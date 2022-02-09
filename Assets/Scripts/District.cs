using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class District : MonoBehaviour
{
    [SerializeField]
    private int _number = 0;
    
    [SerializeReference]
    private List<Building> _buildings = null;
    
    private Player _owner = null;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("District " + _number);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        Debug.Log("Mouse over District " + _number);
    }

    public String getBuildings()
    {
        String res = "";
        foreach(Building building in _buildings)
        {
            res += building.getName() + '\n';
        }
        return res;
    }

    private void setOwner(Player newOwner)
    {
        _owner = newOwner;
    }

    public Player getOwner()
    {
        return _owner;
    }

    public void UpdateControlPoints(int amount, bool adding)
    {
        
    }

    public void UpdateInertiaPoints(int amount, bool adding)
    {
        
    }

    public void FireBuilding(String buildingName)
    {
        // update control points of the district
        _buildings.RemoveAll(building => building.getName() == buildingName);
    }
}