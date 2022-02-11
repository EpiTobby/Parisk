using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using JetBrains.Annotations;
using Parisk;
using Parisk.Action;
using TMPro;
using UnityEngine;

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
    private Player _versaillais = null;
    private Player _communard = null;
    
    [SerializeField]
    private GameObject resultPanel = null;
    
    private Player versaillais = null;
    private Player communard = null;
    private ControlPointContainer _controlPointContainer;
    [CanBeNull] public District SelectedDistrict { get; set; }
    [SerializeField] private DistrictSelectionPanelController _districtSelectionPanelController;

    private List<District> _districts;
    private List<List<int>> districtAdjLists = new List<List<int>>()
    {
        new List<int>{2,3,4,6,7,8},
        new List<int>{1,3,8,9,10},
        new List<int>{1,2,4,10,11,12},
        new List<int>{1,3,5,6,11,12},
        new List<int>{4,6,12,13,14},
        new List<int>{1,4,5,7,14,15},
        new List<int>{1,6,8,14,15,16},
        new List<int>{1,2,6,7,8,9,16,17},
        new List<int>{2,8,10,17,18},
        new List<int>{2,3,9,11,18,19,20},
        new List<int>{3,4,10,12,20},
        new List<int>{4,5,11,13,20},
        new List<int>{5,13,14},
        new List<int>{5,6,13,15},
        new List<int>{6,7,14,16},
        new List<int>{7,8,15,17},
        new List<int>{8,16,18},
        new List<int>{9,10,17,19},
        new List<int>{10,18,20},
        new List<int>{11,12,19},
    };
    
    private IAction[] _actions;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello world!");
        _versaillais = new Player(Side.Versaillais);
        _communard = new Player(Side.Communards);
        initDistrict();
        UpdateTextPlayerTurn();
        _actions = new IAction[0];
    }

    void initDistrict()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("District");
        _districts = objects.Select(obj => obj.GetComponent<District>()).ToList();
        _districts = _districts.OrderBy(district => district.getNumber()).ToList();
        _districts[14].SetOwner(_versaillais);
        _districts[15].SetOwner(_versaillais);
        _districts[17].SetOwner(_communard);
        _districts[18].SetOwner(_communard);
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
        return _districts.Where(district => player.Equals(district.GetOwner())).ToArray();
    }
    
    void applyInfluence()
    {
        foreach (District district in _districts)
        {
            if (district.GetOwner() == null)
                continue;
            foreach (int adj in districtAdjLists[district.getNumber() - 1])
            {
                Debug.Log(district.getNumber().ToString() + " influences " + adj);
                _districts[adj - 1].getPointController().AddPointsTo(district.GetOwner().Side,2);
            }
        }
    }

    void nextTurn()
    {
        Debug.Log("Turn " + _turn + " ended.");
        _turn++;
        if (_turn >= 73)
            endGame();
        else
        {
            turnNumber.text = "Turn " + _turn;
            applyInfluence();
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

    public void SelectDistrict(District district)
    {
        if (SelectedDistrict != null)
            SelectedDistrict.OnDeselect();
        SelectedDistrict = district;
        if (district != null)
        {
            district.OnSelect();
            _districtSelectionPanelController.Initialize(district);
        }
        else
            _districtSelectionPanelController.Hide();
    }

    public static GameController Get()
    {
        return GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
}
