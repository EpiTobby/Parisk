using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Parisk;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    private int _turn = 1;
    
    [SerializeField]
    private TextMeshProUGUI playerTurnText = null;
    
    [SerializeField]
    private TextMeshProUGUI turnNumber = null;

    [SerializeReference] private EventController eventController = null;

    [SerializeField]
    private TextMeshProUGUI textResult = null;

    private District[] _districts;
    private Player _versaillais = null;
    private Player _communard = null;
    
    [SerializeField]
    private GameObject resultPanel = null;
    
    private District[] districts;
    private Player versaillais = null;
    private Player communard = null;
    private ControlPointContainer _controlPointContainer;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello world!");
        _versaillais = new Player(Side.Versaillais);
        _communard = new Player(Side.Communards);
        _controlPointContainer = ControlPointContainer.InitializeRandom();
        GameObject[] objects = GameObject.FindGameObjectsWithTag("District");
        districts = objects.Select(obj => obj.GetComponent<District>()).ToArray();
        UpdateTextPlayerTurn();
    }

    void UpdateTextPlayerTurn()
    {
        if (_turn % 2 == 1)
            playerTurnText.text = "Communard's Turn";
        else
            playerTurnText.text = "Versaillais's Turn";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            nextTurn();
        }
    }

    District[] getPlayerdistrict(Player player)
    {
        return _districts.Where(district => player.Equals(district.getOwner())).ToArray();
    }
    void nextTurn()
    {
        Debug.Log("Turn " + _turn + "ended.");
        _turn++;
        if (_turn == 73)
            endGame();
        else
        {
            turnNumber.text = "Turn " + _turn;
            UpdateTextPlayerTurn();
            eventController.HandleEvents(_turn);
        }
    }

    String getResult()
    {
        int scoreVersaillais = 0;
        int scoreCommunard = 0;
        foreach (District district in _districts)
        {
            scoreVersaillais += district.getPointController().GetPointsFor(_versaillais.Side);
            scoreCommunard += district.getPointController().GetPointsFor(_communard.Side);
        }
        if (scoreCommunard != scoreVersaillais)
        {
            return scoreVersaillais > scoreCommunard ? "VERSAILLAIS WINS" : "COMMUNARD WINS";
        }
        return "IT'S A DRAW!";
    }

    void endGame()
    {
        resultPanel.SetActive(true);
        textResult.text = getResult();
    }

    public Player GetPlayer(Side side)
    {
        return side switch
        {
            Side.Communards => _communard,
            Side.Versaillais => _versaillais,
            _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
        };
    }

    public static GameController Get()
    {
        return GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
}
