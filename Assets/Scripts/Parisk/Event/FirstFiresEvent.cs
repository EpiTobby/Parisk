using System;

namespace Parisk.Event
{
    public class FirstFiresEvent : IEvent
    {
        public string Name()
        {
            return "Premiers incendis";
        }

        public string Description()
        {
            var amount = Convert.ToInt32(EventCost.DestroyBuildingOnEvent);
            return "Destruction du bâtiment « Rue Royale », faisant gagner " + amount + " points au joueur" +
                   "controlant l'arrondissement du bâtiment";
        }

        public int Turn()
        {
            return 67;
        }

        public void Execute()
        {
            var districts = GameController.Get().GetDistricts();
            districts[7].DestroyBuildingOnEvent("Rue Royale");
        }
    }
}