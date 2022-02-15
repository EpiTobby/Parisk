using System;

namespace Parisk.Event
{
    public class SecondFiresEvent : IEvent
    {
        public string Name()
        {
            return "Incendie de l'Hôtel de ville";
        }

        public string Description()
        {
            var amount = Convert.ToInt32(EventCost.DestroyBuildingOnEvent);
            return "Destruction du bâtiment « Hôtel de ville », faisant gagner " + amount + " points au joueur" +
                   "controlant l'arrondissement du bâtiment";
        }

        public int Turn()
        {
            return 68;
        }

        public void Execute()
        {
            var districts = GameController.Get().GetDistricts();
            districts[3].DestroyBuildingOnEvent("Hotel de Ville");
        }
    }
}