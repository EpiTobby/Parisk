using System;
using DefaultNamespace;

namespace Parisk.Action
{
    public class SendScout : IAction
    {
        private District _targetDistrict = null;

        public string Name()
        {
            return "Envoyer un éclaireur";
        }

        public string Description()
        {
            return "Permet de connaitre le nombre de points de controle d'un arrondissement. Envoyer un éclaireur donne " + Convert.ToInt32(ActionCost.SendScout) + " points de controle à l'adversaire.";
        }

        public string Image()
        {
            return "scout";
        }

        public bool CanExecute(Player side, District district)
        {
            var currentPoints = district.GetPointController().GetPointsFor(side.Side);
            var requiredPoints = Convert.ToInt32(ActionCost.SendScout);
            return currentPoints >= requiredPoints;
        }

        public void Execute(Player side, District district)
        {
            var amount = Convert.ToInt32(ActionCost.SendScout);

            district.RemovePointsTo(side.Side, amount);
            _targetDistrict.AddPointsTo(side.Side.GetOpposite(), amount, PointSource.Absenteeism);

            Logger.LogExecute("Send scout", district);

            district.OpenScoutModal();
        }

        public bool SetupExecute(District targetedDistrict)
        {
            if (targetedDistrict == null)
                return false;

            _targetDistrict = targetedDistrict;
            return true;
        }
    }
}
