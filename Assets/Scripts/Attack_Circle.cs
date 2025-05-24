using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Circle : MonoBehaviour
{
    public static Attack_Circle instance;
    public Animator animatorCircle;
    public AnimationClip animatorClip;
    public AnimatorStateInfo stateInfo;
    [HideInInspector] public float NewCircleAnimationLength;

    public  float CommonCircleLenght;
    public  float RangerCircleLenght;
    public  float TankCircleLenght;
    public float BossCircleLenght  = 4.5f;

    public bool End;
    private void Awake()
    {

        if (instance == null)
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
    public void CircleCommonActive(bool active)
    {

        if (active && animatorCircle != null)
        {
            float originalLength = animatorClip.length; // np. 2.0 sek

            // Obliczamy nową prędkość tak, aby animacja trwała `newLength` sekund
            float speed = originalLength / NewCircleAnimationLength;

            // Ustawiamy prędkość Animatora
            animatorCircle.speed = speed;


            animatorCircle.SetBool("CommonCircle", true);


        }
        else if (!active && animatorCircle != null)
        {
            animatorCircle.SetBool("CommonCircle", false);
        }



    }
    public void CircleRangerActive(bool active)
    {

        if (active && animatorCircle != null)
        {
            float originalLength = animatorClip.length; // np. 2.0 sek

            // Obliczamy nową prędkość tak, aby animacja trwała `newLength` sekund
            float speed = originalLength / NewCircleAnimationLength;

            // Ustawiamy prędkość Animatora
            animatorCircle.speed = speed;


            animatorCircle.SetBool("RangerCircle", true);


        }
        else if (!active && animatorCircle != null)
        {
            animatorCircle.SetBool("RangerCircle", false);
        }



    }
    public void CircleTankActive(bool active)
    {

        if (active && animatorCircle != null)
        {
            float originalLength = animatorClip.length; // np. 2.0 sek

            // Obliczamy nową prędkość tak, aby animacja trwała `newLength` sekund
            float speed = originalLength / NewCircleAnimationLength;

            // Ustawiamy prędkość Animatora
            animatorCircle.speed = speed;



            animatorCircle.SetBool("TankCircle", true);


        }
        else if (!active && animatorCircle != null)
        {
            animatorCircle.SetBool("TankCircle", false);
        }



    }
    public void CircleBossActive(bool active)
    {

        if (active && animatorCircle != null)
        {
            float originalLength = animatorClip.length; // np. 2.0 sek

            // Obliczamy nową prędkość tak, aby animacja trwała `newLength` sekund
            float speed = originalLength / NewCircleAnimationLength;

            // Ustawiamy prędkość Animatora
            animatorCircle.speed = speed;



            animatorCircle.SetBool("BossCircle", true);


        }
        else if (!active && animatorCircle != null)
        {
            animatorCircle.SetBool("BossCircle", false);
        }



    }

    public void AnimationEnds()
    {
        End = true;
    }
}
