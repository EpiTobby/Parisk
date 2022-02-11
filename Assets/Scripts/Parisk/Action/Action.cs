using DefaultNamespace;

namespace Parisk.Action
{
    public interface IAction
    {
        public string Name();

        public string Description();

        public bool CanExecute(Player side, District district);
        
        public void Execute(Player side, District district);
    }
}