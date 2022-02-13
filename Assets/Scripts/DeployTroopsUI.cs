using System.Collections;
using System.Collections.Generic;
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
        foreach (char c in s)
        {
            if (!(c >= '0' && c <= '9')) {
                return -1;
            }
        }
 
        return int.Parse(s);
    }

    public void DistrictDropdownSetUp()
    {
        DistrictDropdown.ClearOptions();
        GameController gameController = GameController.Get();
        Player active = gameController.GetActive();
        List<string> options = new List<string>();
        foreach (District district in gameController.GetDistricts())
        {
            if (district.GetOwner() == null || district.GetOwner().Side == active.Side)
            {
                options.Add(district.GetNumber().ToString());
            }
        }
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
            District targeted = gameController.GetDistricts()[int.Parse(DistrictDropdown.options[DistrictDropdown.value].text)];
            deployTroops.SetupExecute(active,GetValueFromInputText(value.text),targeted);
            deployTroops.Execute(active, selectedDistrict);
        }
    }
}
