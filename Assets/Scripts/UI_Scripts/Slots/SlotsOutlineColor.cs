using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsOutlineColor : MonoBehaviour
{
    private Animator animator;
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

        animator.SetTrigger("IsBeatTriggered");
    }
}
