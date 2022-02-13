using System;

namespace Parisk.Event
{
    public class TakeDownStatueEvent : IEvent
    {
        public string Name()
        {
            return "Destruction de la colonne Vendome";
        }

        public string Description()
        {
            var amount = Convert.ToInt32(EventCost.DestroyBuildingOnEvent);
            return "Destruction du bâtiment « Colonne de Vendome », faisant gagner " + amount + " points au joueur" +
                   "controlant l'arrondissement du bâtiment";
        }

        public int Turn()
        {
            return 60;
        }

        public void Execute()
        {
            var districts = GameController.Get().GetDistricts();
            districts[0].DestroyBuildingOnEvent("Vendome");
        }
    }
}