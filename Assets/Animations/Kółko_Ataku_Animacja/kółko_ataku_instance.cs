using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kółko_ataku_instance : MonoBehaviour
{
    public static kółko_ataku_instance instance;
    private  Animator animatorCircle;
    public AnimationClip animatorClip;

    public float NewCircleAnimationLength;
    private void Awake()
    {
        if(instance == null)
        {
            animatorCircle = GetComponent<Animator>();
            
            instance = this;
            float originalLength = animatorClip.length; // np. 2.0 sek

            // Obliczamy nową prędkość tak, aby animacja trwała `newLength` sekund
            float speed = originalLength / NewCircleAnimationLength;

            // Ustawiamy prędkość Animatora
            animatorCircle.speed = speed;
        }
        else
        {
            Destroy(gameObject);
        }
        
        
    }
    public void CircleActive(bool active)
    {
        
        if(active && animatorCircle != null)
        {
            animatorCircle.SetBool("Bool", true);


        }
        else if(!active && animatorCircle != null)
        {
            animatorCircle.SetBool("Bool", false);
        }
        
        

    }
}
