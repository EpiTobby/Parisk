using System;
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
        _districts[3].UpdateControlPoints(5, true);
    }

    public void EventRestrainPressFreedom()
    {
        foreach (District district in _districts)
        {
            if (district.getOwner() == null)
                district.UpdateInertiaPoints(3, false);
        }
    }

    public void EventTakeDownStatue()
    {
        _districts[0].FireBuilding("Vendome");
    }

    public void EventFirstFires()
    {
        _districts[0].FireBuilding("Tuilerie");
        _districts[0].FireBuilding("Louvre");
        
        int[] numberDistricts = new int[3]{3, 10, 11};
        foreach(var n in numberDistricts)
        {
            _districts[n].FireBuilding("Bastille");
        }
    }

    public void EventSecondFires()
    {
        _districts[7].FireBuilding("Rue Royale");
        _districts[4].FireBuilding("Hotel de Ville"); 
    }
    
    
}