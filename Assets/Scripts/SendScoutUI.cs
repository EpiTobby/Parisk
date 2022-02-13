using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Parisk.Action;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class SendScoutUI : MonoBehaviour
{
    [SerializeField]
    public GameObject panel = null;
    
    [SerializeField]
    private Dropdown DistrictDropdown = null;

    public SendScout sendScout = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    
    public void SendScout()
    {
        GameController gameController = GameController.Get();
        Player active = gameController.GetActive();
        District targeted = gameController.GetDistricts()[int.Parse(DistrictDropdown.options[DistrictDropdown.value].text)];
        sendScout.SetupExecute(targeted);
        sendScout.Execute(active, targeted);
        panel.SetActive(false);
    }
}
