using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.SceneManagement;
using static SaveScript;
using static Weapon_Controller;

public class PlayerAttack : MonoBehaviour
{
    
    public Weapon Pickaxe;
    public Weapon Hammer;
    public Weapon Axe;
    public Weapon Gun;

    private Enemy enemy;
    
    private Animator player_anim;


    [HideInInspector] public float AttackTimer;
    //public float AttackWindow;
    public int BlockDuration; // number of beats when Block is active
    [HideInInspector] public int blockCounter = 0; // Block counter (measure: number of beats)
    //private bool BlockIsActive = false;
    [HideInInspector] public static bool CanAttackUI;
    
    public int Attackbeats = 2; // Beats to attack
    [HideInInspector] public int AttackbeatsCounter = 0; // Number Of Current Beat Index to Attack

    public static bool HasAttackedThisBeat = true;
    public PlayerInteracionHUB playerInteracionHUB;

    private bool CanAttackEnemy;


    [SerializeField] private float GunRangeRaycast;
    private Vector3 PlayerForward;
    


    void Start()
    {


        player_anim = GetComponent<Animator>();

        AttackbeatsCounter = Attackbeats;

    }


    private void OnEnable()
    {
        AudioManager.BeatUpdated += PlayerAttacking;

    }
    // Update is called once per frame

    private void OnDestroy()
    {
        AudioManager.BeatUpdated -= PlayerAttacking;

    }
    private void OnDisable()
    {
        AudioManager.BeatUpdated -= PlayerAttacking;




    }
    void Update()
    {


        //if (AttackTimer > 0  && !BlockIsActive)
        //{

        //    AttackTimer -= Time.deltaTime; 


        //}

        if (CanAttackUI)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {

                if (!Movement.HasMovedThisBeat && !HasAttackedThisBeat)
                {

                    if (WeaponManager.Instance.weaponType != WeaponType.Gun)
                    {
                        player_anim.SetTrigger("TriggerAttack");
                    }
                    else
                    {
                        player_anim.SetTrigger("TriggerShot");
                    }
                    if (CanAttackEnemy && WeaponManager.Instance.weaponType != WeaponType.Gun) //CloseCombat
                    {
                        
                        
                        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Punch, this.transform.position);
                        

                        
                        if (WeaponManager.Instance.weaponType == WeaponType.Pickaxe)
                        {             
                            enemy.TakeDamage(Pickaxe.Damage);
                        }
                        else if (WeaponManager.Instance.weaponType == WeaponType.Hammer)
                        {
                            
                            enemy.TakeDamage(Hammer.Damage);
                        }
                        else if (WeaponManager.Instance.weaponType == WeaponType.Axe)
                        {
                            
                            enemy.TakeDamage(Axe.Damage);
                        }



                    }
                    if (WeaponManager.Instance.weaponType == WeaponType.Gun) // With RaycastDetection
                    {
                        GunAttack();
                    }
                    HubInteraction();

                    HasAttackedThisBeat = true;
                }
            }

        }
        //else if (Input.GetKeyDown(KeyCode.Space) && !BlockIsActive)
        //{
        //    blockCounter = BlockDuration;
        //}
        if (AttackTimer <= 0)
        {
            AttackTimer = 0;

            //AttackbeatsCounter = Attackbeats;
        }
        if (AttackbeatsCounter > 0)
        {
            return;
        }
        if (blockCounter > 0)
        {

            return;
        }

    }
    public void PlayerAttacking()
    {
        //player_anim.SetBool("PlayerCanAttack", false);
        if (AttackbeatsCounter > 0)
        {
            AttackbeatsCounter--;
        }
        //if (blockCounter > 0)
        //{
        //    blockCounter--;
        //    BlockIsActive = true;

        //}
        //if(blockCounter <= 0 && AttackbeatsCounter <= 0)
        //{
        //    AttackTimer = AttackWindow;
        //    BlockIsActive = false;


        //}





    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Common") || other.gameObject.CompareTag("Ranger") || other.gameObject.CompareTag("Tank") || other.gameObject.CompareTag("Boss"))
        {

            enemy = other.gameObject.GetComponent<Enemy>();

        }

    }
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.GetComponent<Enemy>())
        {

            CanAttackEnemy = true;

        }



    }
    private void OnTriggerExit(Collider other)
    {
        CanAttackEnemy = false;
    }
    private void GunAttack()
    {

        if (WeaponManager.Instance.weaponType == WeaponType.Gun)
        {
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Shoot, this.transform.position);
            int layerMask = LayerMask.GetMask("Enemy");
            PlayerForward = transform.forward;
            if (Physics.Raycast(transform.position + Vector3.up, PlayerForward, out RaycastHit shoot, GunRangeRaycast, layerMask))
            {


                
                if (shoot.collider.CompareTag("Common") || shoot.collider.CompareTag("Ranger") || shoot.collider.CompareTag("Tank"))
                {

                    enemy = shoot.collider.GetComponent<Enemy>();
                    enemy.TakeDamage(Gun.Damage);

                }

                else
                {
                    Debug.Log("Trafia w coœ jeszcze");

                }


            }

        }
    }
    private void HubInteraction()
    {
        if (playerInteracionHUB != null)
        {



            if (playerInteracionHUB.CanInteractCraft)
            {
                playerInteracionHUB.CraftPanel.gameObject.SetActive(true);

            }


            if (playerInteracionHUB.CanInteractShop)
            {
                playerInteracionHUB.WeaponPanel.gameObject.SetActive(true);
                SaveScript.LoadWeaponShop();
            }

            if (playerInteracionHUB.CanInteractTutorial)
            {

                playerInteracionHUB.Tutorial_Panel.gameObject.SetActive(true);
            }

            if (playerInteracionHUB.CanInteractLore)
            {

                playerInteracionHUB.Lore_Panel.gameObject.SetActive(true);
            }



        }
    }

   
}
