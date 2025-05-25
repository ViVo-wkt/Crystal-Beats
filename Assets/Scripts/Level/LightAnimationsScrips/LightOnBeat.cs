using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnBeat : MonoBehaviour
{
    private Animator animator;

    [HideInInspector] public int LightIndex = 0;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        
    }
    
    private void OnEnable()
    {
        AudioManager.BeatUpdated += BeatDrop;
    }
    private void OnDestroy()
    {
        AudioManager.BeatUpdated -= BeatDrop;

    }
    private void OnDisable()
    {
        AudioManager.BeatUpdated -= BeatDrop;
    }
    private void BeatDrop()
    {
        if(animator.CompareTag("Boss"))
        {
            animator.SetTrigger("BossFight");
        }
        else
        {
            animator.SetTrigger("IsBeatTriggered");
        }
        
    }
}
