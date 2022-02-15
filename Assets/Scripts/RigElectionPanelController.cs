using System;
using System.Globalization;
using Parisk.Event;
using UnityEngine;
using UnityEngine.UI;

public class RigElectionPanelController : MonoBehaviour
{
    [SerializeField] private Text resultTitle;
    [SerializeField] private Text resultText;
    private const string successText = "Vous avez réussi à truquer l'élection !";
    private const string failureText = "Vous n'avez pas réussi à truquer l'élection.";

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void DisplayModal(bool success)
    {
        if (success)
        {
            resultTitle.text = "SUCCÈS";
            resultText.text = successText;
        }
        else
        {
            resultTitle.text = "ÉCHEC";
            resultText.text = failureText;
        }
        gameObject.SetActive(true);
        
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
