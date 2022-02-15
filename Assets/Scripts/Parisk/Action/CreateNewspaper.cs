using System;
using System.Linq;
using DefaultNamespace;

namespace Parisk.Action
{
    public class CreateNewspaper : IAction
    {
        public string Name()
        {
            return "Créer un journal";
        }

        public string Description()
        {
            return "Créer un journal rapport: " 
                   + Convert.ToInt32(ActionCost.CreateNewsPaperInertia) 
                   + " en inertie politique " + Convert.ToInt32(ActionCost.CreateNewsPaperControl) 
                   + " points de controle par arrondissement adjacent controlé";
        }

        public string Image()
        {
            return "newspaper";
        }

        public bool CanExecute(Player side, District district)
        {
            return true;
        }

        public void Execute(Player side, District district)
        {
            var adjCount = district.adj.Count(adj => side.Equals(adj.GetOwner()));
            var amountControl = Convert.ToInt32(ActionCost.CreateNewsPaperControl) * adjCount;
            
            district.AddPointsTo(side.Side, amountControl);
            district.UpdateInertiaPoints(Convert.ToInt32(ActionCost.CreateNewsPaperInertia), true);
            
            Logger.LogExecute("Create Newspaper", district);
        }
    }
}