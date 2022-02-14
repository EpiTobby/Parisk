using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCameraController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSetSound()
    {
        var audioSource = GetComponent<AudioSource>();
        audioSource.volume = slider.value;
    }
}
