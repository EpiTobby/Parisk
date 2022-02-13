using System;
using System.Globalization;
using Parisk;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnPanelController : MonoBehaviour, EventObserver
{
    [SerializeField] private Button nextTurnButton;
    [SerializeField] private TextMeshProUGUI dateText;
    private int _actionCount = 0;
    private DateTime _date = new DateTime(1871, 3, 18);

    // Start is called before the first frame update
    void Start()
    {
        GameController.Get().RegisterEventObserver(this);
        UpdateNextTurnButton();
        UpdateDate();
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

    private void UpdateDate()
    {
        var currentDate = _date.AddDays(GameController.Get().GetTurn() - 1);
        dateText.text = currentDate.ToString("D", new CultureInfo("fr-CA", true));
    }

    public void OnClickNextTurn()
    {
        _actionCount = 0;
        GameController.Get().EndActivePlayerTurn();
        UpdateNextTurnButton();
        UpdateDate();
    }
}