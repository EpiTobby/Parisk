using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Parisk.Action;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class DeployTroopsUI : MonoBehaviour
{
    [SerializeField]
    private Text errorText = null;

    [SerializeField]
    private Text value = null;

    [SerializeField]
    public GameObject panel = null;
    
    [SerializeField]
    private Dropdown DistrictDropdown = null;

    [SerializeField]
    private TurnPanelController _turnPanelController = null;

    public DeployTroops deployTroops = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetValueFromInputText(string s)
    {
        try
        {
            return int.Parse(s);
        }
        catch (Exception e)
        {
            return -1;
        }
    }

    public void DistrictDropdownSetUp()
    {
        DistrictDropdown.ClearOptions();
        GameController gameController = GameController.Get();
        Player active = gameController.GetActive();

        List<string> options = gameController.GetDistricts()
            .Where(district => district.GetOwner() == null || district.GetOwner().Side == active.Side)
            .Select(district => district.GetNumber().ToString()).ToList();

        DistrictDropdown.AddOptions(options);
    }
    
    public bool CheckValues()
    {
        GameController gameController = GameController.Get();
        Player active = gameController.GetActive();
        District selectedDistrict = gameController.SelectedDistrict;
        int soldiers = selectedDistrict.GetPointController().GetPointsFor(active.Side);
        int requested = GetValueFromInputText(value.text);
        if (requested > 0)
        {
            if (requested <= soldiers)
            {
                errorText.text = "";
                return true;
                
            }
            else
                errorText.text = "Vous n'avez pas assez de troupes";
        }
        else
        {
            errorText.text = "Valeur invalide";
        }
        value.text = "";
        return false;
    }

    public void DeployTroops()
    {
        if (CheckValues())
        {
            GameController gameController = GameController.Get();
            Player active = gameController.GetActive();
            District selectedDistrict = gameController.SelectedDistrict;
            District targeted =  gameController.GetDistricts()[int.Parse(DistrictDropdown.options[DistrictDropdown.value].text) - 1];
            if (deployTroops.SetupExecute(active, GetValueFromInputText(value.text), targeted))
            {
                errorText.text = "";
                deployTroops.Execute(active, selectedDistrict);
                panel.SetActive(false);
                _turnPanelController.OnAction();
            }
            else
                errorText.text = "Cette action ne peut pas être réalisée";
        }
    }
}
