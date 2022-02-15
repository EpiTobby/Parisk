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
            return "Destruction du bâtiment « Colonne de Vendome », faisant perdre " + amount + " points aux " +
                   "versaillais s'ils ne contrôlent pas le 1er arrondissement";
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