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
            SaveScript.SaveToJson();
            if (other.gameObject.CompareTag("Player"))
            {
                Resources.UnloadUnusedAssets();
                System.GC.Collect();

                AudioManager.Instance.Load(saveValues.saveVolumes);
                SceneManager.LoadScene("HUB");
            }
        }
        
        
        
    }
    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
