using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public static Settings instance;

    public Toggle[] Music; // Lista Toggle dla muzyki
    public Toggle[] SFX;   // Lista Toggle dla efektów


    public Toggle ResolutionToggle;
    
    void Start()
    {

        instance = this;
        foreach (Toggle toggle in Music)
        {
            toggle.onValueChanged.AddListener(delegate { Volume(Music,toggle); });
            
        }

        foreach (Toggle toggle in SFX)
        {
            toggle.onValueChanged.AddListener(delegate { Volume(SFX, toggle); });
            
        }
    }

    public void Volume(Toggle[] toggles, Toggle clickedtoggle)
    {
        



        bool isOn = false;

        // Iteracja od prawej do lewej (dla zaznaczania Toggle)
        for (int i = toggles.Length - 1; i >= 0; i--)
        {

            if (toggles[i].isOn)
            {
                isOn = true; // Zaznaczamy wszystko w lewo od aktywnego Toggle
                
            }

            if (isOn)
            {
                toggles[i].isOn = true;
            }
            
        }

        
    }
    
    
    public void ScreenSet()
    {
        if(ResolutionToggle.isOn)
        {
            
            Screen.SetResolution(1920, 1080, true);
        }
        else
        {
            Screen.SetResolution(1920, 1080, false);
        }
    }
    public void Save(ref SaveToggles data)
    {
        
        data.Music = new bool[Music.Length];
        data.SFX = new bool[SFX.Length];

        for (int i = 0; i < Music.Length; i++)
            data.Music[i] = Music[i].isOn;

        for (int i = 0; i < SFX.Length; i++)
            data.SFX[i] = SFX[i].isOn;
    }
    

    public void Load(SaveToggles data)
    {
        
        
        for (int i = 0; i < Music.Length; i++)
            Music[i].isOn = data.Music[i];

        for (int i = 0; i < SFX.Length; i++)
            SFX[i].isOn = data.SFX[i];
    }

}
[System.Serializable]
public struct SaveToggles
{
    public bool[] Music;
    public bool[] SFX;

}













