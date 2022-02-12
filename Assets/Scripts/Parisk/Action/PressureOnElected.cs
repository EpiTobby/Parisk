using System;
using DefaultNamespace;

namespace Parisk.Action
{
    public class PressureOnElected : IAction
    {
        public string Name()
        {
            return "Pression sur les élus";
        }

        public string Description()
        {
            return "Faire pression sur les élus et gagner " + Convert.ToInt32(ActionCost.PressureOnElected) + " points de contrôle.";
        }

        public bool CanExecute(Player side, District district)
        {
            return true;
        }

        public string Image()
        {
            return "pressure.png";
        }

        public void Execute(Player side, District district)
        {
            var amount = Convert.ToInt32(ActionCost.PressureOnElected);
            district.getPointController().AddPointsTo(side.Side, amount);
            
            Logger.LogExecute("Pressure on elected", district);
        }
    }
}