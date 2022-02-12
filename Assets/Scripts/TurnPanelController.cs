using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnPanelController : MonoBehaviour
{
    [SerializeField] private Button nextTurnButton;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickNextTurn()
    {
        GameController.Get().endActivePlayerTurn();
    }
}
