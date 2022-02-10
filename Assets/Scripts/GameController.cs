using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Parisk;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int turn = 1;
    [SerializeField]
    private TextMeshProUGUI playerTurnText = null;
    
    [SerializeField]
    private TextMeshProUGUI TurnNumber = null;
    
    [SerializeField]
    private TextMeshProUGUI textResult = null;
    
    private District[] districts;
    private Player versaillais = null;
    private Player communard = null;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello world!");
        versaillais = new Player(Side.Versaillais);
        communard = new Player(Side.Communards);
        //
        /*GameObject[] objects = GameObject.FindGameObjectsWithTag("District");
        districts = objects.Select(obj => obj.GetComponent<District>()).ToArray();*/
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
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("N key pressed");
            nextTurn();
        }
    }

    District[] getPlayerdistrict(Player player)
    {
        return districts.Where(district => player.Equals(district.getOwner())).ToArray();
    }
    void nextTurn()
    {
        Debug.Log("Turn " + turn + "ended.");
        turn++;
        if (turn == 72)
            endGame();
        else
        {
            TurnNumber.text = "Turn " + turn;
            UpdateTextPlayerTurn();
        }
    }

    String getResult()
    {
        int scoreVersaillais = 0;
        int scoreCommunard = 0;
        foreach (District district in districts)
        {
            scoreVersaillais += district.getPointController().GetPointsFor(versaillais.Side);
            scoreCommunard += district.getPointController().GetPointsFor(communard.Side);
        }
        if (scoreCommunard != scoreVersaillais)
        {
            return scoreVersaillais > scoreCommunard ? "VERSAILLAIS WINS" : "COMMUNARD WINS";
        }
        return "IT'S A DRAW!";
    }

    void endGame()
    {
        GameObject resultPanel = GameObject.Find("ResultPanel");
        resultPanel.SetActive(true);
        textResult.text = getResult();
    }

    public Player GetPlayer(Side side)
    {
        return side switch
        {
            Side.Communards => communard,
            Side.Versaillais => versaillais,
            _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
        };
    }

    public static GameController Get()
    {
        return GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
}
