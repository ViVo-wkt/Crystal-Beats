using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject settings;

    public void Continue()
    {
        throw new NotImplementedException();
    }
    public void NewGame()
    {

        SaveScript.ClearJsonData();
        SceneManager.LoadScene("Tutorial");
        
    }
    public void Settings()
    {
        settings.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
