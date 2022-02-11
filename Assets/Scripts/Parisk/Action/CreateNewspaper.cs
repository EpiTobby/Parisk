using System;
using DefaultNamespace;

namespace Parisk.Action
{
    public class CreateNewspaper : IAction
    {
        public string Name()
        {
            return "Créer un journal";
        }

        public string Description()
        {
            return "Créer un journal rapport +5 en inertie politique +2 points de controle par arrondissement adjacent controlé";
        }

        public bool CanExecute(Player side, District district)
        {
            return true;
        }

        public void Execute(Player side, District district)
        {
            int amountControl = Convert.ToInt32(ActionCost.CreateNewsPaperControl) * GameController.Get().getAdjListOfDistrict(district.GetNumber()).Count;
            
            district.getPointController().AddPointsTo(side.Side, amountControl);
            district.UpdateInertiaPoints(Convert.ToInt32(ActionCost.CreateNewsPaperInertia), true);
        }
    }
}