using System;
using System.Collections.Generic;
using DefaultNamespace;
using JetBrains.Annotations;
using Parisk;
using UnityEngine;

public class District : MonoBehaviour
{
    [SerializeField]
    private int number;
    [SerializeReference]
    private List<Building> buildings = null;
    private Player _owner = null;
    private ControlPointContainer _pointContainer = ControlPointContainer.InitializeRandom();
    [SerializeField] private Collider _collider;
    private AnimationSelectionDirection _animationSelectionDirection;
    private int _inertiaPoints = 0;
    [SerializeField] private GameObject boardObject;
    
    public List<District> adj = new List<District>();

    private Election _nextElection;

    private void Awake()
    {
        MeshCollider collider = GetComponentInChildren<MeshCollider>();
        collider.gameObject.AddComponent<ColliderBridge>().Initialize(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("District " + number);
    }

    // Update is called once per frame
    void Update()
    {
        switch (_animationSelectionDirection)
        {
            case AnimationSelectionDirection.None:
                break;
            case AnimationSelectionDirection.Up:
                if (gameObject.transform.position.y >= 0.3f)
                    _animationSelectionDirection = AnimationSelectionDirection.None;
                else
                    gameObject.transform.Translate(0, 0.08f, 0);
                break;
            case AnimationSelectionDirection.Down:
                if (gameObject.transform.position.y <= -0.02461721f)
                    _animationSelectionDirection = AnimationSelectionDirection.None;
                else
                    gameObject.transform.Translate(0, -0.08f, 0);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
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

    private void ChangeDistrictColor()
    {
        var materialComponent = boardObject.GetComponent<MeshRenderer>();
        materialComponent.GetComponent<Renderer>().material = _owner == null ? 
            Resources.Load("Materials/White", typeof(Material)) as Material
            : _owner.Side == Side.Versaillais
            ? Resources.Load("Materials/Blue", typeof(Material)) as Material
            : Resources.Load("Materials/Red", typeof(Material)) as Material;
    }

    /**
     * Do an election an set the new owner of this district
     */
    public ElectionsResult DoElections()
    {
        if (_nextElection == null)
            throw new Exception("No election in this district");
        var result = PredictElections();
        _owner = result.Side == null 
            ? null 
            : GameController.Get().GetPlayer(result.Side.Value);
        ChangeDistrictColor();
        
        Debug.Log(_owner.Side.ToString() + " win the election in district" + number);
        
        _nextElection = null;
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

    public void SetOwner(Player newOwner)
    {
        _owner = newOwner;
        if (_owner != null)
        {
            ChangeDistrictColor();
        }
    }

    public Player GetOwner()
    {
        return _owner;
    }

    public void UpdateControlPointsOnEvent(int amount, bool adding)
    {
        if (_owner == null)
            return;
        
        _pointContainer.AddPointsTo(adding ? _owner.Side : _owner.Side.GetOpposite(), amount);
    }

    public void UpdateInertiaPoints(int amount, bool adding)
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

    private void AnimateSelection(AnimationSelectionDirection direction)
    {
        _animationSelectionDirection = direction;
    }

    public void OnSelect()
    {
        AnimateSelection(AnimationSelectionDirection.Up);
    }

    public void OnDeselect()
    {
        AnimateSelection(AnimationSelectionDirection.Down);
    }

    public void OnMouseEnter()
    {
        AnimateSelection(AnimationSelectionDirection.Up);
    }

    public void OnMouseExit()
    {
        if (GameController.Get().SelectedDistrict != this)
            AnimateSelection(AnimationSelectionDirection.Down);
    }

    public void OnMouseUpAsButton()
    {
        GameController.Get().SelectDistrict(this);
    }

    public int GetNumber()
    {
        return number;
    }

    public void StartElections()
    {
        if (_nextElection != null)
            throw new Exception("Elections already in progress in district " + number);
        _nextElection = new Election(GameController.Get().GetTurn() + 1);
    }

    [CanBeNull]
    public Election GetNextElection()
    {
        return _nextElection;
    }
}

class ColliderBridge : MonoBehaviour
{
    District _listener;
    public void Initialize(District l)
    {
        _listener = l;
    }

    private void OnMouseEnter()
    {
        _listener.OnMouseEnter();
    }

    private void OnMouseExit()
    {
        _listener.OnMouseExit();
    }

    private void OnMouseUpAsButton()
    {
        _listener.OnMouseUpAsButton();
    }
}

enum AnimationSelectionDirection
{
    None,
    Up,
    Down,
}