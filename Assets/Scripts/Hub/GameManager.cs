using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool playerMoved;
    private Vector3 lastPlayerPosition;
    public Transform player;

    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        //DynamicGI.UpdateEnvironment();
        SaveScript.LoadFromJson();

        SaveScript.ClearJsonData();
    }

    private void Update()
    {
        playerMoved = Vector3.Distance(lastPlayerPosition, player.position) > 0.2f;
        lastPlayerPosition = player.position;
    }
    public void OnClickQuitToMenu()
    {
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene("MainMenu");
    }
    public void OnClickLoadTutorial()
    {
        SaveScript.SaveToJson();
        SceneManager.LoadScene("Tutorial");
    }

    public void RestartLevel()
    {
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}

