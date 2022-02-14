using System;
using System.Collections;
using System.Collections.Generic;
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
            List<District> districts = GameController.Get().GetDistricts();
            foreach (District d in districts)
            {
                if (d.GetPointController().GetPointsFor(side.Side) >= 5)
                    return true;
            }

            return false;
        }

        public void Execute(Player side, District district)
        {
            var amount = Convert.ToInt32(ActionCost.SendScout);

            district.RemovePointsTo(side.Side, amount);
            _targetDistrict.AddPointsTo(side.Side.GetOpposite(), amount, PointSource.Absenteeism);

            Logger.LogExecute("Send scout ", _targetDistrict);

            _targetDistrict.OpenScoutModal();
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
