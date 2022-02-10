namespace Parisk.Action
{
    public interface IAction
    {
        public string Name();

        public string Description();

        public bool CanExecute(Side side, District district);
        
        public void Execute(Side side, District district);
    }
}