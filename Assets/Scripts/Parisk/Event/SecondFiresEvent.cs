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
            return "Destruction du bâtiment « Hôtel de ville », faisant perdre " + amount + " points aux " +
                   "versaillais s'ils ne contrôlent pas le 4e arrondissement";;
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