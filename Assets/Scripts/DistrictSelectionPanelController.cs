using System;
using DefaultNamespace;
using Parisk;
using UnityEngine;
using UnityEngine.UI;

public class DistrictSelectionPanelController : MonoBehaviour
{
    [SerializeField] private Text _titleText;
    [SerializeField] private ActionScrollView actionScrollView;
    [SerializeField] private GameObject alreadyPlayedText;
    [SerializeField] private Text districtSidePoints = null;
    
    private Color VersaillaisColor = new Color(57f / 255f, 69f / 255f, 212f / 255f);
    private Color CommunardColor = new Color(215f / 255f, 38f / 255f, 38f / 255f);
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }
    }

    public void Initialize(District district)
    {
        _titleText.text = district.GetNumber() + "e arrondissement";
        gameObject.SetActive(true);
        Player activePlayer = GameController.Get().GetActive();
        if (!activePlayer.ExecutedActions.ContainsKey(district) && activePlayer.ExecutedActions.Count < 2)
        {
            actionScrollView.DisplayButtons(GameController.Get().GetActive(), district);
            actionScrollView.gameObject.SetActive(true);
            alreadyPlayedText.SetActive(false);
            districtSidePoints.color = activePlayer.Side == Side.Communards ? CommunardColor : VersaillaisColor;
            
            ControlPointContainer controlPointContainer = district.GetPointController();
            
            if (district.GetOwner() == activePlayer)
                districtSidePoints.text = "Points : " + controlPointContainer.GetPointsFor(activePlayer.Side);
            else
                districtSidePoints.text = "Points: " + Math.Max(0,
                    controlPointContainer.GetPointsFor(activePlayer.Side) -
                    (activePlayer.Side == Side.Communards
                        ? controlPointContainer.GetCommunardRandomPoints()
                        : controlPointContainer.GetVersaillaisRandomPoints())) + " + ?";
            
        }
        else
        {
            actionScrollView.gameObject.SetActive(false);
            alreadyPlayedText.SetActive(true);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}