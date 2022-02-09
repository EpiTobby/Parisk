using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int turn = 1;
    private Text playerTurnText;
    private Text TurnNumber;
    private District[] districts;
    private Player versaillais = null;
    private Player communard = null;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello world!");
        versaillais = new Player(0, "Versaillais");
        communard = new Player(0, "Communard");
        GameObject[] objects = GameObject.FindGameObjectsWithTag("District");
        districts = objects.Select(obj => obj.GetComponent<District>()).ToArray();
        UpdateTextPlayerTurn();
    }

    void UpdateTextPlayerTurn()
    {
        if (turn % 2 == 1)
            playerTurnText.text = "Communard's Turn";
        else
            playerTurnText.text = "Versaillais's Turn";
    }

    // Update is called once per frame
    void Update()
    {

    }

    District[] getPlayerdistrict(Player player)
    {
        return districts.Where(district => player.Equals(district.getOwner())).ToArray();
    }

    void nextTurn()
    {
        Debug.Log("Turn " + turn + "ended.");
        turn++;
        TurnNumber.text = "" + turn;
        UpdateTextPlayerTurn();
    } 
}
