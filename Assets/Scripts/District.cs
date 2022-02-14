using System;
using System.Collections.Generic;
using System.Collections;
using DefaultNamespace;
using JetBrains.Annotations;
using Parisk;
using Parisk.Action;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Random = System.Random;

public class District : MonoBehaviour
{
    [SerializeField]
    private int number;
    [SerializeReference]
    private List<Building> buildings = null;
    private Player _owner = null;
    private readonly ControlPointContainer _pointContainer = ControlPointContainer.InitializeRandom();
    [SerializeField] private Collider _collider;
    private AnimationSelectionDirection _animationSelectionDirection;
    private int _inertiaPoints = 0;
    [SerializeField] private GameObject boardObject;
    [SerializeField] private GameObject scoutModal;
    [SerializeField] private TMP_Text versaillaisPoints = null;
    [SerializeField] private TMP_Text communardsPoints = null;
    [SerializeField] private RigElectionPanelController rigElectionPanelController;

    public Animator transition;

    public List<District> adj = new List<District>();

    private Election _nextElection;

    private UniqueActionDistrict[] _uniqueActionDistrict =
    {
        new DestroyBuilding(), 
        new ExecutePrisoners(), 
    };

    private bool _alreadyDoneUniqueActionDistrict = false;

    private void Awake()
    {
        MeshCollider meshCollider = GetComponentInChildren<MeshCollider>();
        meshCollider.gameObject.AddComponent<ColliderBridge>().Initialize(this);
    }

    // Start is called before the first frame update
    void Start()
    {
       
        
        _pointContainer.SetVersaillaisPoints(versaillaisPoints);
        _pointContainer.SetCommunardsPoints(communardsPoints);
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

    public void AddPointsTo(Side side, int amount, PointSource source = PointSource.Mixed)
    {
        _pointContainer.AddPointsTo(side, amount, source);
        transition.SetTrigger("start_points");
    }

    public void RemovePointsTo(Side side, int amount)
    {
        _pointContainer.RemovePointsTo(side, amount);
        transition.SetTrigger("start_points");
    }

    private void OnMouseOver()
    {
        Debug.Log("Mouse over District " + number);
    }

    public String GetBuildings()
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

    IEnumerator ChangeDistrictColorWithAnimation()
    {
        transition.SetTrigger("start_flip");

        yield return new WaitForSeconds(0.5f);
        ChangeDistrictColor();
    }

    /**
     * Do an election an set the new owner of this district
     */
    public ElectionsResult DoElections()
    {
        if (_nextElection == null)
            throw new Exception("No election in this district");

        ElectionsResult result;
        if (_nextElection.GetFakedSide().HasValue)
        {
            Side side = _nextElection.GetFakedSide().GetValueOrDefault();
            bool success = new Random().Next(0, 100) <= Convert.ToInt32(ActionCost.RigElectionSuccessRate);

            var type = _owner == null ? ElectionsResultType.Win :
                _owner.Side != side ? ElectionsResultType.Reversal : ElectionsResultType.Maintain; 
            if (success)
            {
                AddPointsTo(side, Convert.ToInt32(ActionCost.RigElectionSuccess));
                result = new ElectionsResult(side, type);
            }
            else
            {
                RemovePointsTo(side, Convert.ToInt32(ActionCost.RigElectionFailure));
                result = PredictElections();
            }
            rigElectionPanelController.DisplayModal(success);
        }
        else
        {
            result = PredictElections();
        }
        
        _owner = result.Side == null 
            ? null 
            : GameController.Get().GetPlayer(result.Side.Value);
        StartCoroutine(ChangeDistrictColorWithAnimation());
        
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
        StartCoroutine(ChangeDistrictColorWithAnimation());
    }

    public Player GetOwner()
    {
        return _owner;
    }

    public bool CanExecuteUniqueActionDistrict()
    {
        return _alreadyDoneUniqueActionDistrict == false;
    }

    public void ExecuteUniqueActionDistrict()
    {
        _alreadyDoneUniqueActionDistrict = true;
    }

    public UniqueActionDistrict GetUniqueActionDistrict()
    {
        return _uniqueActionDistrict[Convert.ToInt32(_owner.Side)];
    }

    public void UpdateControlPointsOnEvent(int amount, bool adding)
    {
        if (_owner == null)
            return;
        
        AddPointsTo(adding ? _owner.Side : _owner.Side.GetOpposite(), amount);
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

    public ControlPointContainer GetPointController()
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

    public void OpenScoutModal()
    {
        if (scoutModal != null)
        {
            var script = (ScoutModal)scoutModal.GetComponent(typeof(ScoutModal));
            script.OpenModal(this);
        }
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
        // Check if the left mouse button was clicked
        if (!EventSystem.current.IsPointerOverGameObject())
            _listener.OnMouseEnter();
    }

    private void OnMouseExit()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            _listener.OnMouseExit();
    }

    private void OnMouseUpAsButton()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            _listener.OnMouseUpAsButton();
    }
}

enum AnimationSelectionDirection
{
    None,
    Up,
    Down,
}