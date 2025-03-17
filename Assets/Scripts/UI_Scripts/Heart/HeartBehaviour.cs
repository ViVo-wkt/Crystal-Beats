using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBehaviour : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        AudioManager.BeatUpdated += BeatDrop;
    }

    private void OnDestroy()
    {
        AudioManager.BeatUpdated -= BeatDrop;

    }
    private void BeatDrop()
    {
        //animator.SetBool("IsBeatActive", true);
        animator.SetTrigger("IsBeatTriggered");
    }
}
