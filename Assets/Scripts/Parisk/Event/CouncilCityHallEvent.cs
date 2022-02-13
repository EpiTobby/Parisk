using System;

namespace Parisk.Event
{
    public class CouncilCityHallEvent : IEvent
    {
        public string Name()
        {
            return "Établissement du Conseil de la Commune à l'hôtel de ville";
        }

        public string Description()
        {
            var amount = Convert.ToInt32(EventCost.InstallCouncilCityHall);
            return "+" + amount + " points au joueur controlant le 4e arrondissement.";
        }

        public int Turn()
        {
            return 11;
        }

        public void Execute()
        {
            GameController.Get().GetDistricts()[3].UpdateControlPointsOnEvent(Convert.ToInt32(EventCost.InstallCouncilCityHall), true);
        }
    }
}