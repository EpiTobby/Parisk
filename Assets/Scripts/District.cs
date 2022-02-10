using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
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

    private void setOwner(Player newOwner)
    {
        owner = newOwner;
    }

    public Player getOwner()
    {
        return owner;
    }
}
