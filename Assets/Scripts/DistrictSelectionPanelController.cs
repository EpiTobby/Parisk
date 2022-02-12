using UnityEngine;
using UnityEngine.UI;

public class DistrictSelectionPanelController : MonoBehaviour
{
    [SerializeField] private Text _titleText;
    [SerializeField] private ActionScrollView actionScrollView;
    [SerializeField] private GameObject alreadyPlayedText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(District district)
    {
        _titleText.text = district.GetNumber() + "e arrondissement";
        gameObject.SetActive(true);
        if (!GameController.Get().GetActive().ExecutedActions.ContainsKey(district) && GameController.Get().GetActive().ExecutedActions.Count < 2)
        {
            actionScrollView.DisplayButtons(GameController.Get().GetActive(), district);
            actionScrollView.gameObject.SetActive(true);
            alreadyPlayedText.SetActive(false);
        }
        else
        {
            actionScrollView.gameObject.SetActive(false);
            alreadyPlayedText.SetActive(true);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
