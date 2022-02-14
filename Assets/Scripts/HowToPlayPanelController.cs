using System;
using System.Globalization;
using Parisk;
using Parisk.Event;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayPanelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
