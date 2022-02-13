namespace Parisk.Event
{
    public interface IEvent
    {
        public string Name();

        public string Description();

        public int Turn();

        public void Execute();
    }
}