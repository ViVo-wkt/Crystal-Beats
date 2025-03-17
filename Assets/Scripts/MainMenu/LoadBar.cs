using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadBar : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject loading;
    private Animator animator;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
        animator = GetComponent<Animator>();
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf)
        {
            animator.SetBool("Load", true);
            if(MainMenu != null)
            {
                MainMenu.SetActive(false);
            }
            
        }
        if(slider != null && slider.value == 1)
        {
            animator.SetBool("Load", false);
            slider.value = 0;
            if(MainMenu != null)
            {
                MainMenu.SetActive(true);
            }
            
            loading.SetActive(false);
        }
    }
}
