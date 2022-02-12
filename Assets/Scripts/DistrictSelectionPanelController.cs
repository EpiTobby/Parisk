using UnityEngine;
using UnityEngine.UI;

public class DistrictSelectionPanelController : MonoBehaviour
{
    [SerializeField] private Text _titleText;
    [SerializeField] private ActionScrollView actionScrollView;
    
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
        if (!GameController.Get().GetActive().ExecutedActions.ContainsKey(district))
        {
            actionScrollView.DisplayButtons(GameController.Get().GetActive(), district);
            actionScrollView.gameObject.SetActive(true);
        }
        else
        {
            actionScrollView.gameObject.SetActive(false);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
