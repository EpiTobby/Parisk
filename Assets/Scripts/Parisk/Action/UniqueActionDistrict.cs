using System;
using DefaultNamespace;

namespace Parisk.Action
{
    public abstract class UniqueActionDistrict : IAction
    {
        private bool _alreadyDone = false;
        
        public virtual string Name()
        {
            throw new System.NotImplementedException();
        }

        public virtual string Description()
        {
            throw new System.NotImplementedException();
        }

        public virtual bool CanExecute(Player side, District district)
        {
            return !_alreadyDone && district.GetOwner() != null && district.GetOwner().Side == side.Side;
        }

        public virtual void Execute(Player side, District district)
        {
            _alreadyDone = true;
        }
    }

    public class DestroyBuilding : UniqueActionDistrict
    {
        public override string Name()
        {
            return "Destruction d’un bâtiment (hôtel particulier de Thiers, colonne Vendôme)";
        }

        public override string Description()
        {
            return "Destruction d’un bâtiment (hôtel particulier de Thiers, colonne Vendôme): -"
                   + Convert.ToInt32(ActionCost.DestroyBuilding)
                   + "pour l’adversaire (qui passent en absentéisme) à faire qu’une fois dans la partie pour cet arrondissement.";
        }

        public override void Execute(Player side, District district)
        {
            district.GetPointController().RemovePointsTo(side.Side.GetOpposite(), Convert.ToInt32(ActionCost.DestroyBuilding));
            base.Execute(side, district);
        }
    }

    public class ExecutePrisoners : UniqueActionDistrict
    {
        public override string Name()
        {
            return "Exécution de prisonniers";
        }

        public override string Description()
        {
            return "Exécution de prisonniers : -"
                   + Convert.ToInt32(ActionCost.ExecutePrisoners)
                   + " pour l’adversaire (qui passent en absentéisme) à faire qu’une fois dans la partie pour cet arrondissement.";
        }

        public override void Execute(Player side, District district)
        {
            district.GetPointController().RemovePointsTo(side.Side.GetOpposite(), Convert.ToInt32(ActionCost.ExecutePrisoners));
            base.Execute(side, district);
        }
    }
}