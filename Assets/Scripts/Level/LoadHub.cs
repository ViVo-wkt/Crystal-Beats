using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.SceneManagement;
using static SaveScript;

public class LoadHub : MonoBehaviour
{
    


    private void OnTriggerEnter(Collider other)
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.TeleportEntrance, this.transform.position);


        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Resources.UnloadUnusedAssets();
                System.GC.Collect();
                SceneManager.LoadScene("HUB");
            }
        }
        else
        {
            LevelComplete();
            SaveScript.SaveToJson();
            if (other.gameObject.CompareTag("Player"))
            {
                Resources.UnloadUnusedAssets();
                System.GC.Collect();

                //AudioManager.Instance.Load(saveValues.saveVolumes);
                SceneManager.LoadScene("HUB");
            }
        }
        
        
        
    }
    private void LevelComplete()
    {
        if(gameObject.CompareTag("Level_1"))
        {
            GameManager.Level_1_Complete = true;
        }
        else if (gameObject.CompareTag("Level_2"))
        {
            GameManager.Level_2_Complete = true;
        }
        else if (gameObject.CompareTag("Level_3"))
        {
            GameManager.Level_3_Complete = true;
        }
    }
    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
