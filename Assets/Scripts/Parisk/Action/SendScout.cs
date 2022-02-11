using System;
using DefaultNamespace;

namespace Parisk.Action
{
    public class SendScout : IAction
    {
        public string Name()
        {
            return "Envoyer un �claireur";
        }

        public string Description()
        {
            return "Permet de connaitre le nombre de points de contr�le d'un arrondissement. Envoyer un �claireur donne " + Convert.ToInt32(ActionCost.SendScout) + " points de contr�le � l'adversaire.";
        }

        public bool CanExecute(Player side, District district)
        {
            var currentPoints = district.getPointController().GetPointsFor(side.Side);
            var requiredPoints = Convert.ToInt32(ActionCost.PressureOnElected);
            return currentPoints >= requiredPoints;
        }

        public void Execute(Player side, District district)
        {
            var amount = Convert.ToInt32(ActionCost.SendScout);
            district.getPointController().RemovePointsTo(side.Side, amount);
            // TODO: add points to target district
            // target.getPointController().AddPointsTo(side.Side.GetOpposite(), amount, PointSource.Absenteeism);
        }
    }
}
