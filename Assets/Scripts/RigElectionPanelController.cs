using System;
using System.Globalization;
using Parisk.Event;
using UnityEngine;
using UnityEngine.UI;

public class RigElectionPanelController : MonoBehaviour
{
    [SerializeField] private Text resultTitle;
    [SerializeField] private Text resultText;
    private const string successText = "Vous avez r�ussi � truquer l'�lection !";
    private const string failureText = "Vous n'avez pas r�ussi � truquer l'�lection.";

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void DisplayModal(bool success)
    {
        if (success)
        {
            resultTitle.text = "SUCC�S";
            resultText.text = successText;
        }
        else
        {
            resultTitle.text = "�CHEC";
            resultText.text = failureText;
        }
        gameObject.SetActive(true);
        
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
