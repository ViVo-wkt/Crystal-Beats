using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToggleVolume : MonoBehaviour
{
    //public static ToggleVolume Instance;
    private enum VolumeType
    {
        Music,
        SFX
    }

    [Header("Type")]
    [SerializeField] private VolumeType volumeType;
    public int activeToggles = 0;
    private float ToggleValue;

    private void Awake()
    {
        
        //Instance = this;
        //ToggleCounter();
        //VolumUpdate();
    }


    // Update is called once per frame
    void Update()
    {
        
        switch (volumeType)
        {
            case VolumeType.Music:
                ToggleValue = AudioManager.Instance.MusicVolume;
                break;
            case VolumeType.SFX:
                ToggleValue = AudioManager.Instance.SFXVolume;
                break;
            default:
                Debug.Log("Error");
                break;
        }
    }
    public void OnToggleValueChanged()
    {
        ToggleCounter();
        VolumUpdate();
        switch (volumeType)
        {
            case VolumeType.Music:
                AudioManager.Instance.MusicVolume = ToggleValue;
                break;
            case VolumeType.SFX:
                AudioManager.Instance.SFXVolume = ToggleValue;
                break;
            default:
                Debug.Log("Error");
                break;
        }
    }
    public void ToggleCounter()
    {

        activeToggles = 0;
        // Pobieramy wszystkie Toggle w obiekcie i jego childach
        Toggle[] toggles = GetComponentsInChildren<Toggle>();

        // Sprawdzamy, ile jest aktywnych toggli
        foreach (Toggle toggle in toggles)
        {
            if (toggle.isOn)
            {
                activeToggles++;
            }
        }
    }
    public void VolumUpdate()
    {
        ToggleValue = Mathf.Clamp(activeToggles * 0.20f, 0f, 1f);
        

    }
    
    
    
}

