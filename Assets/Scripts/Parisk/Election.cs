namespace Parisk
{
    public class Election
    {
        private readonly int _turn;
        private readonly Side _startingElectionSide;
        private Side? _fakedSide;

        public Election(int turn, Side startingElectionSide)
        {
            _turn = turn;
            _startingElectionSide = startingElectionSide;
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

        public Side GetStartingElectionSide()
        {
            return _startingElectionSide;
        }
    }
}