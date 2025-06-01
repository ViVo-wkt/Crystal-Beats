using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartHighLight : MonoBehaviour
{
    public static HeartHighLight instance;

    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        animator = GetComponent<Animator>();

    }
    
    public void BeatDrop()
    {

        animator.SetTrigger("IsBeatTriggered");
    }
}