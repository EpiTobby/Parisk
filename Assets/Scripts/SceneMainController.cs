using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneMainController : MonoBehaviour
{
    private int turn = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello world!");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject nextTurnButton = GameObject.Find("TextNextTurn");
        Text text = nextTurnButton.GetComponentInChildren<Text>();
        text.text = "" + turn;
    }

    public void nextTurn()
    {
        Debug.Log("next");
        turn++;
    } 
}
