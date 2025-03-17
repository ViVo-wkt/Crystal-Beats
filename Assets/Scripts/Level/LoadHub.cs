using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadHub : MonoBehaviour
{
    


    private void OnTriggerEnter(Collider other)
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.TeleportEntrance, this.transform.position);


        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            if (other.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene("HUB");
            }
        }
        else
        {
            SaveScript.SaveToJson();
            if (other.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene("HUB");
            }
        }
        
        
        
    }
}
