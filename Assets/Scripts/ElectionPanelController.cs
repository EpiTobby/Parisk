using System;
using System.Globalization;
using Parisk;
using Parisk.Event;
using UnityEngine;
using UnityEngine.UI;

public class ElectionPanelController : MonoBehaviour
{
    [SerializeField] private Text resultTitle;
    [SerializeField] private Text resultText;

    private Color VersaillaisColor = new Color(57f / 255f, 69f / 255f, 212f / 255f);
    private Color CommunardColor = new Color(215f / 255f, 38f / 255f, 38f / 255f);
    private Color AbsenteeismColor = new Color(115f / 255f, 115f / 255f, 115f / 255f);

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void DisplayModal(ElectionsResult result, District district)
    {
        if (result.Side == Side.Communards)
        {
            resultTitle.color = CommunardColor;
            resultTitle.text = "COMMUNARDS";
            resultText.text = "ont gagné l'élection du " + district.GetNumber() + "e arrondissement.";
        }
        else if (result.Side == Side.Versaillais)
        {
            resultTitle.color = VersaillaisColor;
            resultTitle.text = "VERSAILLAIS";
            resultText.text = "ont gagné l'élection du " + district.GetNumber() + "e arrondissement.";
        }
        else
        {
            resultTitle.color = AbsenteeismColor;
            resultTitle.text = "PERSONNE";
            resultText.text = "n'a gagné l'élection du " + district.GetNumber() + "e arrondissement";
        }
        gameObject.SetActive(true);
        
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
