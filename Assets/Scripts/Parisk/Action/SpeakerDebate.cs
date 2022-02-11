using System;
using DefaultNamespace;

namespace Parisk.Action
{
    public class SpeakerDebate : IAction
    {
        public string Name()
        {
            return "Débat d'orateur";
        }

        public string Description()
        {
            return "Lancer un débat entre orateur pour gagner entre 5 et 25 points de contrôle.";
        }

        public bool CanExecute(Player side, District district)
        {
            return district.GetNextElection() == null;
        }

        public void Execute(Player side, District district)
        {
            var amount = new Random().Next(Convert.ToInt32(ActionCost.DebateMin), Convert.ToInt32(ActionCost.DebateMax));
            district.getPointController().AddPointsTo(side.Side, amount);
        }
    }
}