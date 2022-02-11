using UnityEngine;
using UnityEngine.UI;

public class DistrictSelectionPanelController : MonoBehaviour
{
    [SerializeField] private Text _titleText;
    
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
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
