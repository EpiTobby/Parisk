using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Parisk.Action;
using TMPro;
using UnityEngine;

public class ActionScrollView : MonoBehaviour
{
    public Dictionary<IAction, GameObject> ActionbuttonDictionary = new Dictionary<IAction, GameObject>();
    public Transform parent = null;
    public GameObject ActionButtonPrefab = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        District district = GameController.Get().SelectedDistrict;
        if (district != null)
        {
            DisplayButtons(GameController.Get().GetActive(),district);
        }
    }

    public void createButtons(IAction[] actions)
    {
        int y = 285;
        foreach (IAction action in actions)
        {
            Debug.Log(action.Name());
            GameObject button = Instantiate(ActionButtonPrefab,new Vector3(0, y, 0), Quaternion.identity);
            button.transform.SetParent(parent.transform, false);
            button.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = action.Name();
            button.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = action.Description();
            ActionbuttonDictionary.Add(action,button);
            y-=150;
        }
    }

    public void DisplayButtons(Player player, District district)
    {
        foreach (var pair in ActionbuttonDictionary)
        {
            if (pair.Key.CanExecute(player, district))
                pair.Value.SetActive(true);
            else
                pair.Value.SetActive(false);
        }
    }
}
