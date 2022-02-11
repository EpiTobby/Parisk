using System;
using DefaultNamespace;

namespace Parisk.Action
{
    public abstract class UniqueActionGame : IAction
    {
        private bool _alreadyDone = false;

        public virtual string Name()
        {
            throw new System.NotImplementedException();
        }

        public virtual string Description()
        {
            throw new System.NotImplementedException();
        }

        public virtual bool CanExecute(Player side, District district)
        {
            return !_alreadyDone && district.GetOwner() != null && district.GetOwner().Side == side.Side;
        }

        public virtual void Execute(Player side, District district)
        {
            _alreadyDone = true;
        }
    }

    public class GermanPact : UniqueActionGame
    {
        public override string Name()
        {
            return "German Pact";
        }

        public override string Description()
        {
            return "Pacte avec les allemands (libération de prisonniers de guerre) : +"
                   + Convert.ToInt32(ActionCost.GermanPact)
                   + " points aux arrondissements possédés.";
        }

        public override void Execute(Player side, District district)
        {
            foreach (var playerDistrict in GameController.Get().GetPlayerDistrict(side))
            {
                playerDistrict.getPointController().AddPointsTo(side.Side, Convert.ToInt32(ActionCost.GermanPact));
            }
            base.Execute(side, district);
        }
    }

    public class NationalGuardReinstatement : UniqueActionGame
    {
        public override string Name()
        {
            return "Rétablissement de la solde de la garde nationale";
        }

        public override string Description()
        {
            return "Rétablissement de la solde de la garde nationale : +"
                   + Convert.ToInt32(ActionCost.GermanPact)
                   + " points aux arrondissements possédés.";
        }

        public override void Execute(Player side, District district)
        {
            foreach (var playerDistrict in GameController.Get().GetPlayerDistrict(side))
            {
                playerDistrict.getPointController().AddPointsTo(side.Side, Convert.ToInt32(ActionCost.NationalGuardReinstatement));
            }
            base.Execute(side, district);
        }
    }
}