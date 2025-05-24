using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SaveScript;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject settings;

    public void Continue()
    {
        throw new NotImplementedException();
    }
    public void NewGame()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.MouseClick, this.transform.position);
        SaveScript.ClearJsonData();
        AudioManager.Instance.Save(ref saveValues.saveVolumes);
        Settings.instance.Save(ref saveValues.saveToggles);
        
        SceneManager.LoadScene("HUB");
        
    }
    public void Settingss()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.MouseClick, this.transform.position);
        settings.SetActive(true);
    }
    public void Quit()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.MouseClick, this.transform.position);
        Application.Quit();
    }
}
