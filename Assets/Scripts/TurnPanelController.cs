using Parisk;
using UnityEngine;
using UnityEngine.UI;

public class TurnPanelController : MonoBehaviour, EventObserver
{
    [SerializeField] private Button nextTurnButton;
    private int _actionCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameController.Get().RegisterEventObserver(this);
        UpdateNextTurnButton();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnAction()
    {
        _actionCount++;
        UpdateNextTurnButton();
    }

    private void UpdateNextTurnButton()
    {
        var buttonText = nextTurnButton.GetComponentInChildren<Text>();
        if (_actionCount < 2)
        {
            buttonText.text = _actionCount + "/2 Actions";
            nextTurnButton.interactable = false;
        }
        else
        {
            buttonText.text = "Tour suivant";
            nextTurnButton.interactable = true;
        }
    }

    public void OnClickNextTurn()
    {
        _actionCount = 0;
        UpdateNextTurnButton();
        GameController.Get().endActivePlayerTurn();
    }
}