using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Parisk;

public class ScoutModal : MonoBehaviour
{
    [SerializeField] private GameObject modal = null;
    [SerializeField] private Text number = null;
    [SerializeField] private Text versaillais = null;
    [SerializeField] private Text communards = null;
    [SerializeField] private Text absenteeism = null;

    public void OpenModal(District district)
    {
        number.text = district.GetNumber().ToString();
        versaillais.text = district.GetPointController().GetPointsFor(Side.Versaillais).ToString();
        communards.text = district.GetPointController().GetPointsFor(Side.Communards).ToString();
        absenteeism.text = district.GetPointController().GetAbsenteeism().ToString();
        modal.SetActive(true);
    }

    public void CloseModal()
    {
        modal.SetActive(false);
    }
}
