using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using JetBrains.Annotations;
using Parisk;
using Parisk.Action;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GameController : MonoBehaviour
{
    private int _turn = 1;
    
    [SerializeField]
    private Text playerTurnText = null;
    [SerializeField]
    private Image playerTurn = null;

    private Color VersaillaisColor = new Color(57f / 255f, 69f / 255f, 212f / 255f);
    private Color CommunardColor = new Color(215f / 255f, 38f / 255f, 38f / 255f);

    [SerializeField]
    private TextMeshProUGUI turnNumber = null;

    private EventController _eventController;

    [SerializeField]
    private Text textResult = null;
    private Player _versaillais = null;
    private Player _communard = null;
    private Player _active = null;
    
    [SerializeField]
    private GameObject resultPanel = null;

    [SerializeField] private new GameObject light;
    
    [CanBeNull] public District SelectedDistrict { get; set; }
    [SerializeField] private DistrictSelectionPanelController _districtSelectionPanelController;

    private List<District> _districts;
    
    private IAction[] _actions;

    [SerializeField]
    private ActionScrollView _actionScrollView = null;

    [SerializeField] private EventPanelControler eventPanelController;

    private readonly List<EventObserver> _observers = new List<EventObserver>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello world!");
        _versaillais = new Player(Side.Versaillais);
        _communard = new Player(Side.Communards);
        _active = _communard;
        InitDistrict();
        _eventController = new EventController();
        _actions = new IAction[]
        {
            new CreateNewspaper(),
            new Attack(),
            new SendScout(),
            new DeployTroops(),
            new SpeakerDebate(),
            new ElectionAction(),
            new PressureOnElected(),
            new RigElection(),
            _versaillais.UniqueActionGame,
            _communard.UniqueActionGame,
            new ExecutePrisoners(),
            new DestroyBuilding()
        };
        _actionScrollView.createButtons(_actions);
        playerTurnText.text = "COMMUNARD";
    }

    void InitOwnerDistrict()
    {
        List<int> communardDistricts = new List<int>(){18, 19, 20, 11, 12};
        List<int> versaillaisDistricts = new List<int>(){14, 15, 16, 17};
        
        int random = new Random().Next(communardDistricts.Count);
        int value = communardDistricts[random];
        _districts[value - 1].SetOwner(_communard);
        _districts[value - 1].GetPointController().SetInitialPoints(Side.Communards);
        communardDistricts.RemoveAt(random);

        int closest = communardDistricts.OrderBy(district => Math.Abs(value - district)).First();
        _districts[closest - 1].SetOwner(_communard);
        _districts[closest - 1].GetPointController().SetInitialPoints(Side.Communards);

        random = new Random().Next(versaillaisDistricts.Count);
        value = versaillaisDistricts[random];
        _districts[value - 1].SetOwner(_versaillais);
        _districts[value - 1].GetPointController().SetInitialPoints(Side.Versaillais);
        versaillaisDistricts.RemoveAt(random);
        
        closest = versaillaisDistricts.OrderBy(district => Math.Abs(value - district)).First();
        _districts[closest - 1].SetOwner(_versaillais);
        _districts[closest - 1].GetPointController().SetInitialPoints(Side.Versaillais);
    }

    void InitDistrict()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("District");
        _districts = objects.Select(obj => obj.GetComponent<District>()).ToList();
        _districts = _districts.OrderBy(district => district.GetNumber()).ToList();

        InitOwnerDistrict();
        
        List<List<int>> districtAdjLists = new List<List<int>>()
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
        foreach (District district in _districts)
        {
            foreach (int adj in districtAdjLists[district.GetNumber() - 1])
            {
                district.adj.Add(_districts[adj - 1]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            EndActivePlayerTurn();
        }
        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }
        if (Input.GetKeyDown("a"))
        {
            _observers.ForEach(observer => observer.OnAction());
        }
    }

    public District[] GetPlayerDistrict(Player player)
    {
        return _districts.Where(district => player.Equals(district.GetOwner())).ToArray();
    }

    void ApplyInfluence()
    {
        foreach (District district in _districts)
        {
            if (district.GetOwner() == null)
                continue;

            foreach (District adj in district.adj)
            {
                Debug.Log(district.GetNumber() + " influences " + adj.GetNumber());
                adj.AddPointsTo(district.GetOwner().Side,2);
            }
        }
    }

    public void EndActivePlayerTurn()
    {
        if (_active.Side == Side.Versaillais)
        {
            _active = _communard;
            playerTurnText.text = "COMMUNARD";
            playerTurn.color = CommunardColor;
            NextTurn();
        }
        else
        {
            _active = _versaillais;
            playerTurnText.text = "VERSAILLAIS";
            playerTurn.color = VersaillaisColor;
        }
        _active.ExecutedActions.Clear();
        ProcessOnGoingElections();
    }

    void NextTurn()
    {
        _turn++;
        StartCoroutine(AnimateLight());
        if (_turn >= 73)
            EndGame();
        else
        {
            turnNumber.text = "Tour " + _turn;
            ApplyInfluence();
            _eventController.HandleEvents(_turn);
        }
    }

    private IEnumerator AnimateLight()
    {
        var rigidBody = light.GetComponent<Rigidbody>();
        rigidBody.AddForce(0, 0, 2000f);
        Debug.Log(light.transform.position.z);
        while (light.transform.position.z < 50)
            yield return null;
        rigidBody.transform.Translate(0, 0, -100f, Space.World);
        rigidBody.AddForce(0, 0, 2000f);
        while (light.transform.position.z < -7)
            yield return null;
        rigidBody.velocity = new Vector3(0, 0, 0);
        rigidBody.transform.position = new Vector3(-5.4f, 17.76f, -7.13f);
    }

    private void ProcessOnGoingElections()
    {
        foreach (var district in _districts)
        {
            Election districtElection = district.GetNextElection();
            if ( districtElection!= null 
                && districtElection!.GetTurn() == _turn
                && districtElection.GetStartingElectionSide() == _active.Side)
            {
                district.DoElections();
            }
        }
    }

    Side? GetResult()
    {
        int scoreVersaillais = 0;
        int scoreCommunard = 0;
        foreach (District district in _districts)
        {
            scoreVersaillais += district.GetPointController().GetPointsFor(_versaillais.Side);
            scoreCommunard += district.GetPointController().GetPointsFor(_communard.Side);
        }
        if (scoreCommunard != scoreVersaillais)
        {
            return scoreVersaillais > scoreCommunard ? Side.Versaillais : Side.Communards;
        }
        return null;
    }

    void EndGame()
    {
        turnNumber.text = " ";
        playerTurnText.text = " ";
        resultPanel.SetActive(true);
        var winningSide = GetResult();
        if (!winningSide.HasValue)
        {
            textResult.text = "ÉGALITÉ";
        }
        else if (winningSide! == Side.Communards)
        {
            textResult.color = new Color(235f / 255f, 60f / 255f, 1f / 255f);
            textResult.text = "COMMUNARDS";
        }
        else
        {
            textResult.color = new Color(9f / 255f, 124f / 255f, 255f / 255f);
            textResult.text = "VERSAILLAIS";
        }
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

    public void UnselectDistrict()
    {
        SelectDistrict(null);
    }

    public int GetTurn()
    {
        return _turn;
    } 
    
    public Player GetActive()
    {
        return _active;
    }

    public void RegisterEventObserver(EventObserver eventObserver)
    {
        _observers.Add(eventObserver);
    }

    public void ExecuteAction(Player player, IAction action, District district)
    {
        action.Execute(player, district);
        player.ExecutedActions[district] = action;
        _observers.ForEach(observer => observer.OnAction());
        SelectDistrict(null);
    }

    public List<District> GetDistricts()
    {
        return _districts;
    }

    public EventPanelControler GetEventPanelController()
    {
        return eventPanelController;
    }

    public static GameController Get()
    {
        return GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
}