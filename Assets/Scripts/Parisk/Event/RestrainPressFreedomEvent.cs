using System;
using System.Linq;

namespace Parisk.Event
{
    public class RestrainPressFreedomEvent : IEvent
    {
        public string Name()
        {
            return "Restriction des libertés de la presse";
        }

        public string Description()
        {
            return "Créer un journal passe de +5 à +2 intertie politique dans tous les arrondissements non possédés " +
                   "par un joueur";
        }

        public int Turn()
        {
            return 32;
        }

        public void Execute()
        {
            var districts = GameController.Get().GetDistricts();
            foreach (var district in districts.Where(district => district.GetOwner() == null))
            {
                district.UpdateInertiaPoints(Convert.ToInt32(EventCost.RestrainPressFreedom), false);
            }
        }
    }
}