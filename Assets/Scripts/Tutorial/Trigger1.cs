using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger1 : MonoBehaviour
{
    public GameObject Trigger1_Guide;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            Trigger1_Guide.SetActive(true);
        }
    }
}
