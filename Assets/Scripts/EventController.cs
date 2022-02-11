using System;
using Parisk;
using UnityEngine;


public class EventController : MonoBehaviour
{
    private District[] _districts;
    
    public District[] Districts
    {
        get => _districts;
        set => _districts = value;
    }

    private void Start()
    {
        _districts = new District[20];
    }

    public void HandleEvents(int turn)
    {
        switch (turn)
        {
            case 11:
                // 28 mars
                EventCouncilCityHall();
                break;
            case 32:
                // 18 avril
                EventRestrainPressFreedom();
                break;
            case 60:
                // 16 mai
                EventTakeDownStatue();
                break;
            case 67:
                // 23 mai
                EventFirstFires();
                break;
            case 68:
                // 24 mai
                EventSecondFires();
                break;
        }
    }


    public void EventCouncilCityHall()
    {
        _districts[3].UpdateControlPointsOnEvent(Convert.ToInt32(EventCost.InstallCouncilCityHall), true);
    }

    public void EventRestrainPressFreedom()
    {
        foreach (District district in _districts)
        {
            if (district.owner == null)
                district.UpdateInertiaPointsOnEvent(Convert.ToInt32(EventCost.RestrainPressFreedom), false);
        }
    }

    public void EventTakeDownStatue()
    {
        _districts[0].DestroyBuildingOnEvent("Vendome");
    }

    public void EventFirstFires()
    {
        _districts[7].DestroyBuildingOnEvent("Rue Royale");
    }

    public void EventSecondFires()
    {
        _districts[3].DestroyBuildingOnEvent("Hotel de Ville"); 
    }
    
    
}