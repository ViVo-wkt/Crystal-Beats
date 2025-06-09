using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_3 : MonoBehaviour
{
    public SaveScript saveScript;

    private void OnTriggerEnter(Collider other)
    {
        if (saveScript != null)
        {
            SaveScript.SaveToJson();
        }
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.TeleportEntrance, this.transform.position);
        if (other.gameObject.CompareTag("Player"))
        {
            Resources.UnloadUnusedAssets();
            System.GC.Collect();

            SceneManager.LoadScene("Level 3");
        }
    }
    private void OnDestroy()
    {
        Destroy(gameObject);

    }
}
