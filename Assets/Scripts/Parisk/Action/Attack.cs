using System;
using DefaultNamespace;

namespace Parisk.Action
{
    public class Attack : IAction
    {
        public string Name()
        {
            return "Attaquer";
        }

        public string Description()
        {
            return "Attaquer l'adversaire et lui prendre entre " + Convert.ToInt32(ActionCost.AttackMin) + " à " + Convert.ToInt32(ActionCost.AttackMax) + " points de contrôle. Cette action a 20% de chance de rater.";
        }

        public bool CanExecute(Player side, District district)
        {
            return true;
        }

        public void Execute(Player side, District district)
        {
            var amount = new Random().Next(Convert.ToInt32(ActionCost.AttackMin), Convert.ToInt32(ActionCost.AttackMax));
            district.getPointController().AddPointsTo(side.Side, amount, PointSource.Adversary);
        }
    }
}