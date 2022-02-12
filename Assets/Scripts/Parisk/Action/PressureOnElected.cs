using System;
using DefaultNamespace;

namespace Parisk.Action
{
    public class PressureOnElected : IAction
    {
        public string Name()
        {
            return "Pression sur les �lus";
        }

        public string Description()
        {
            return "Faire pression sur les �lus et gagner " + Convert.ToInt32(ActionCost.PressureOnElected) + " points de contr�le.";
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