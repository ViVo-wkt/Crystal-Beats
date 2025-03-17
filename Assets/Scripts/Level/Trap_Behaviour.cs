using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Behaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public Player_Info player_Info;
    public float TimeToHit;
    public int DamageTakenFromTrap;
    private bool isTakingDamage = false;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isTakingDamage = true;
            StartCoroutine(TimeToDealDamage());
            
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        isTakingDamage = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTakingDamage = false;
            StopAllCoroutines();
            
        }
            
    }
    IEnumerator TimeToDealDamage()
    {
        while (isTakingDamage)
        {

            yield return new WaitForSeconds(TimeToHit);
            anim.SetTrigger("TrapAttack");
            if (isTakingDamage != false)
            {
                
                player_Info.Player_HP -= DamageTakenFromTrap;
                player_Info.PlayerHpUpdate();
                player_Info.CheckIfDead();
            }
            
        }
    }
}
