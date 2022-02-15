using System;
using DefaultNamespace;

namespace Parisk.Action
{
    public class DeployTroops : IAction
    {
        private int _numberOfTroops = 0;
        private District _targetDistrict = null;

        public string Name()
        {
            return "Déployer des troupes";
        }

        public string Description()
        {
            return "Transfère des points de contrôle d’un arrondissement possédé vers un arrondissement neutre.";
        }

        public string Image()
        {
            return "troops";
        }

        public bool CanExecute(Player side, District selectedDistrict)
        {
            return selectedDistrict.GetOwner() == side && selectedDistrict.GetPointController().GetPointsFor(side.Side) > _numberOfTroops;
        }

        public void Execute(Player side, District selectedDistrict)
        {
            _targetDistrict.AddPointsTo(side.Side, _numberOfTroops);
            selectedDistrict.RemovePointsTo(side.Side, _numberOfTroops);
            Logger.LogExecute("Deploy Troops", selectedDistrict);
        }

        public bool SetupExecute(Player side, int amount, District targetedDistrict)
        {
            if (targetedDistrict == null)
                return false;
            
            _numberOfTroops = amount;
            _targetDistrict = targetedDistrict;
            return true;
        }
    }
}