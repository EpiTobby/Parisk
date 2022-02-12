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
        nextTurnButton.interactable = false;
        GameController.Get().RegisterEventObserver(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAction()
    {
        _actionCount++;
    }

    public void OnClickNextTurn()
    {
        GameController.Get().endActivePlayerTurn();
    }
}
