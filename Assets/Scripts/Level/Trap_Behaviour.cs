using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Behaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public Player_Info player_Info;
    public int BeatToHit;
    public int DamageTakenFromTrap;
    //private bool isTakingDamage = false;
    private int Beats = 0;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //isTakingDamage = true;
            //StartCoroutine(TimeToDealDamage());
            AudioManager.BeatUpdated += TimeToDealDamage;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        //isTakingDamage = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //isTakingDamage = false;
            //StopCoroutine(TimeToDealDamage());
            AudioManager.BeatUpdated -= TimeToDealDamage;
            Beats = 0;
        }
            
    }

    private void TimeToDealDamage()
    {
        Beats++;
        if(Beats == BeatToHit)
        {
            anim.SetTrigger("TrapAttack");
            player_Info.Player_HP -= DamageTakenFromTrap;
            if(player_Info.Player.activeSelf)
            {
                player_Info.PlayerHpUpdate();
                player_Info.CheckIfDead();
            }
            
            Beats = 0;
        }
    }
    //IEnumerator TimeToDealDamage()
    //{
    //    while (isTakingDamage)
    //    {

    //        yield return new WaitForSeconds(TimeToHit);
    //        anim.SetTrigger("TrapAttack");
    //        if (isTakingDamage != false)
    //        {
                
    //            player_Info.Player_HP -= DamageTakenFromTrap;
    //            player_Info.PlayerHpUpdate();
    //            player_Info.CheckIfDead();
    //        }
            
    //    }
    //}
}
