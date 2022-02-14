using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Parisk.Action;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionScrollView : MonoBehaviour
{
    public Dictionary<IAction, GameObject> ActionbuttonDictionary = new Dictionary<IAction, GameObject>();
    public Transform parent = null;
    public GameObject ActionButtonPrefab = null;

    [SerializeField] 
    public DeployTroopsUI deployTroopsUI = null;

    [SerializeField] 
    public SendScoutUI sendScoutUI = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createButtons(IAction[] actions)
    {
        int y = 285;
        foreach (IAction action in actions)
        {
            if (action is DeployTroops deployTroops)
                deployTroopsUI.deployTroops = deployTroops;
            if (action is SendScout sendScout)
                sendScoutUI.sendScout = sendScout;
            Debug.Log(action.Name());
            GameObject button = Instantiate(ActionButtonPrefab,new Vector3(0, y, 0), Quaternion.identity);
            button.transform.SetParent(parent.transform, false);
            button.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = action.Name();
            button.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = action.Description();
            button.transform.GetChild(2).gameObject.GetComponent<RawImage>().texture = Resources.Load("Images/" + action.Image()) as Texture2D;
            ActionbuttonDictionary.Add(action,button);
            y-=150;
        }
    }

    public void DisplayButtons(Player player, District district)
    {
        foreach (var pair in ActionbuttonDictionary)
        {
            if (pair.Key.CanExecute(player, district))
            {
                pair.Value.SetActive(true);
                pair.Value.GetComponent<Button>().onClick.RemoveAllListeners();
                if (pair.Key is DeployTroops)
                {
                    deployTroopsUI.DistrictDropdownSetUp();
                    pair.Value.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        deployTroopsUI.panel.SetActive(true);
                    });
                }
                else if (pair.Key is SendScout)
                {
                    sendScoutUI.DistrictDropdownSetUp();
                    pair.Value.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        sendScoutUI.panel.SetActive(true);
                    });
                }
                else
                {
                    pair.Value.GetComponent<Button>().onClick.AddListener(() =>
                    { 
                        GameController.Get().ExecuteAction(player, pair.Key, district);
                    });
                }
            }
            else
                pair.Value.SetActive(false);
        }
    }
}
