using System;
using System.Globalization;
using Parisk.Event;
using UnityEngine;
using UnityEngine.UI;

public class EventPanelControler : MonoBehaviour
{
    [SerializeField] private new Text name;
    [SerializeField] private Text description;
    [SerializeField] private Text date;
    private DateTime _date = new DateTime(1871, 3, 18);

    private IEvent _currentEvent;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void DisplayEvent(IEvent iEvent)
    {
        gameObject.SetActive(true);
        name.text = iEvent.Name();
        description.text = iEvent.Description();
        var currentDate = _date.AddDays(GameController.Get().GetTurn() - 1);
        date.text = currentDate.ToString("D", new CultureInfo("fr-CA", true));

        _currentEvent = iEvent;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        _currentEvent.Execute();
        _currentEvent = null;
    }
}
