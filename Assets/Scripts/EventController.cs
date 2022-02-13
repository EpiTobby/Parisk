using Parisk.Event;

public class EventController
{
    private IEvent[] _events;

    public EventController()
    {
        _events = new IEvent[]
        {
            new CouncilCityHallEvent(),
            new FirstFiresEvent(),
            new RestrainPressFreedomEvent(),
            new SecondFiresEvent(),
            new TakeDownStatueEvent(),
        };
    }

    public void HandleEvents(int turn)
    {
        foreach (var iEvent in _events)
        {
            if (iEvent.Turn() == turn)
            {
                GameController.Get().GetEventPanelController().DisplayEvent(iEvent);
            }
        }
    }
}