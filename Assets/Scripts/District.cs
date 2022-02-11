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
    [SerializeField] private Collider _collider;
    private AnimationSelectionDirection _animationSelectionDirection;

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