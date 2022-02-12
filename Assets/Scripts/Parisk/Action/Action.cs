using System;
using DefaultNamespace;
using UnityEngine;

namespace Parisk.Action
{
    public interface IAction
    {
        public string Name();

        public string Description();

        public string Image();

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
                      + district.getPointController().GetPointsFor(Side.Communards)
                      + " & Versaillais: "
                      + district.getPointController().GetPointsFor(Side.Versaillais));
        }
    }
}