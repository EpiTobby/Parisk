namespace Parisk
{
    public class Election
    {
        private readonly int _turn;
        private Side? _fakedSide;

        public Election(int turn)
        {
            _turn = turn;
        }

        public int GetTurn()
        {
            return _turn;
        }

        public void FakeElection(Side side)
        {
            _fakedSide = side;
        }

        public Side? GetFakedSide()
        {
            return _fakedSide;
        }
    }
}