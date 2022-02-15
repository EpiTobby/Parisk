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

        public string Image()
        {
            return "document";
        }

        public bool CanExecute(Player side, District district)
        {
            return district.GetNextElection() != null;
        }

        public void Execute(Player side, District district)
        {
            district.GetNextElection()?.FakeElection(side.Side);
        }
    }
}