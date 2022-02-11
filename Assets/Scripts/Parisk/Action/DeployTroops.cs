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
            return "Déplacer des troupes : transfère des points de controle d’un arrondissement possédé vers un arrondissement non ennemi (neutre ou ami)." + 
                   " Les points perdus dans l’arrondissement de départ sont complétés à moitié par l’ennemi et à moitié par l’absenteisme.";
        }

        public bool CanExecute(Player side, District selectedDistrict)
        {
            return _targetDistrict != null 
                   && selectedDistrict.GetOwner() == side 
                   && selectedDistrict.getPointController().GetPointsFor(side.Side) > _numberOfTroops
                   && (_targetDistrict.GetOwner() == null || _targetDistrict.GetOwner().Side == side.Side);
        }

        public void Execute(Player side, District selectedDistrict)
        {
            _targetDistrict.getPointController().AddPointsTo(side.Side, _numberOfTroops);
            selectedDistrict.getPointController().RemovePointsTo(side.Side, _numberOfTroops);
        }

        public void SetupExecute(int amount, District targetedDistrict)
        {
            _numberOfTroops = amount;
            _targetDistrict = targetedDistrict;
        }
    }
}