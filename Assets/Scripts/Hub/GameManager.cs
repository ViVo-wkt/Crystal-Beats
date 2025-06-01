using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SaveScript;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    

    private Player_Info player_Info;
    private InventoryManager inventoryManager;

    public static bool asd; // What should be turn on before loading Hub scene for the firs time;

    public static bool Level_1_Complete;
    public static bool Level_2_Complete;
    public static bool Level_3_Complete;
    // Start is called before the first frame update
    void Start()
    {
        
        if(SceneManager.GetActiveScene().name != "MainMenu")
        {
            inventoryManager = GetComponent<InventoryManager>();
            player_Info = GetComponent<Player_Info>();
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            
            
            SaveScript.LoadFromJson();
            
            AudioManager.Instance.Load(saveValues.saveVolumes);
            SaveScript.ClearJsonData();
        }
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            
            if(asd)
            {
                AudioManager.Instance.Load(saveValues.saveVolumes);
                Settings.instance.Load(saveValues.saveToggles);
            }
            else if(!asd)
            {
                Settings.instance.ToggleUpdate();
            }
        }


    }

    
    public void OnClickQuitToMenu()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.MouseClick, this.transform.position);
        Resources.UnloadUnusedAssets();
        AudioManager.Instance.Save(ref saveValues.saveVolumes);
        Settings.instance.Save(ref saveValues.saveToggles);
        SceneManager.LoadScene("MainMenu");
        

    }
    public void OnClickLoadTutorial()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.MouseClick, this.transform.position);
        SaveScript.SaveToJson();
        SceneManager.LoadScene("Tutorial");
    }
    public void OpenSettings()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.MouseClick, this.transform.position);
    }
    public void RestartLevel()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.MouseClick, this.transform.position);
        Resources.UnloadUnusedAssets();
        inventoryManager.CrystalCleanUp();
        player_Info.Player_HP = 4;
        SaveScript.SaveToJson();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}

