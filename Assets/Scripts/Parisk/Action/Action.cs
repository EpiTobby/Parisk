using System;
using DefaultNamespace;
using UnityEngine;

namespace Parisk.Action
{
    public interface IAction
    {
        public string Name();

        public string Description();

        public bool CanExecute(Player side, District district);
        
        public void Execute(Player side, District district);

        
    }

    public class Logger
    {
        public static void LogExecute(String executeAction, District district)
        {
            Debug.Log("Executing "
                      + executeAction + " on District " 
                      + district.GetNumber() 
                      + ": Communards: " 
                      + district.GetPointController().GetPointsFor(Side.Communards)
                      + " & Versaillais: "
                      + district.GetPointController().GetPointsFor(Side.Versaillais));
        }
    }
}