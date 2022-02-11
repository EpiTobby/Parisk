using DefaultNamespace;

namespace Parisk.Action
{
    public class ElectionAction : IAction
    {
        public string Name()
        {
            return "Lancer une élection";
        }

        public string Description()
        {
            return "";
        }

        public bool CanExecute(Player side, District district)
        {
            return district.GetNextElection() == null;
        }

        public void Execute(Player side, District district)
        {
            district.StartElections();
        }
    }
}