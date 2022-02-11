using System;
using DefaultNamespace;

namespace Parisk.Action
{
    public class RigElection : IAction
    {
        public string Name()
        {
            return "Truquer une élection";
        }

        public string Description()
        {
            return "Truquer une élection avec un taux de réussite de "
                   + Convert.ToInt32(ActionCost.RigElectionSuccessRate)
                   + ". En cas de réussite, gagne "
                   + Convert.ToInt32(ActionCost.RigElectionSuccess)
                   + " points de controle, "
                   + Convert.ToInt32(ActionCost.RigElectionFailure)
                   + " sinon";
        }

        public bool CanExecute(Player side, District district)
        {
            return district.GetNextElection() != null;
        }

        public void Execute(Player side, District district)
        {
            bool success = new Random().Next(0, 100) <= Convert.ToInt32(ActionCost.RigElectionSuccessRate);

            ControlPointContainer controlPointContainer = district.getPointController();
            if (success)
                controlPointContainer.AddPointsTo(side.Side, Convert.ToInt32(ActionCost.RigElectionSuccess));
            else
                controlPointContainer.RemovePointsTo(side.Side, Convert.ToInt32(ActionCost.RigElectionFailure));
        }
    }
}