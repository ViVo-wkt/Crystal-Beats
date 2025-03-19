using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public WeaponManager weaponManager;
    public Weapon Pickaxe;
    public Weapon Hammer;
    public Weapon Axe;
    public Weapon Gun;


    private Common common;
    private Ranger ranger;
    private Tank tank;

    //private new Collider  collider;
    private Animator player_anim;

    //public GameObject CanAttack;
   [HideInInspector] public float AttackTimer;
    public float AttackWindow;
    public int BlockDuration; // ustawianie iloœci blokad
   [HideInInspector] public int blockCounter = 0; // Licznik blokady (liczba beatów)
    private bool BlockIsActive = false;
   [HideInInspector] public static bool CanAttackUI;
    //public float AttackSpeed = 1;
    public double Attackbeats =2; // liczba pó³beatów do ataku
   [HideInInspector] public double AttackbeatsCounter = 0; // liczba beatów do ataku

    public PlayerInteracionHUB playerInteracionHUB;
    private bool CanAttackCommon;
    private bool CanAttackRanger;
    private bool CanAttackTank;

    [SerializeField] private float GunRangeRaycast;
    private Vector3 PlayerForward;
    // Start is called before the first frame update
    void Start()
    {
        //collider = GetComponent<Collider>();
        player_anim = GetComponent<Animator>();
        AudioManager.BeatUpdated += PlayerAttacking;
        AttackbeatsCounter = Attackbeats;
        
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        AudioManager.BeatUpdated -= PlayerAttacking;
    }
    void Update()
    {

        

        //Raycast do broni
        if (weaponManager.WeaponIndex == 3)
        {
            Debug.DrawRay(transform.position + Vector3.up, PlayerForward * GunRangeRaycast, Color.blue, 1f);
            int layerMask = LayerMask.GetMask("Enemy");
            PlayerForward = transform.forward;
            if(Physics.Raycast(transform.position + Vector3.up, PlayerForward,out RaycastHit shoot ,GunRangeRaycast, layerMask))
            {
                
                if (Input.GetKeyDown(KeyCode.Space) && CanAttackUI)
                {
                    if (shoot.collider.CompareTag("Common"))
                    {
                        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Shoot, this.transform.position);
                        common = shoot.collider.GetComponent<Common>();
                        common.TakeDamage(Gun.Damage);
                        
                    }
                    else if (shoot.collider.CompareTag("Ranger"))
                    {
                        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Shoot, this.transform.position);
                        ranger.gameObject.GetComponent<Ranger>();
                        ranger.TakeDamage(Gun.Damage);
                    }
                    else if (shoot.collider.CompareTag("Tank"))
                    {
                        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Shoot, this.transform.position);
                        tank.gameObject.GetComponent<Tank>();
                        tank.TakeDamage(Gun.Damage);
                    }
                    else
                    {
                        Debug.Log("Trafia w coœ jeszcze");
                    }
                    //HUB interkacja
                    
                }
                
                    


            }
            if (playerInteracionHUB != null)
            {
                
                if (Physics.Raycast(transform.position + Vector3.up, PlayerForward ,GunRangeRaycast, LayerMask.GetMask("Default")))
                {
                    
                    if (Input.GetKeyDown(KeyCode.Space) && CanAttackUI)
                    {
                        
                        if (playerInteracionHUB.CanInteractCraft)
                        {
                            playerInteracionHUB.CraftPanel.gameObject.SetActive(true);

                        }
                        
                        
                            if (playerInteracionHUB.CanInteractShop)
                            {
                                playerInteracionHUB.WeaponPanel.gameObject.SetActive(true);

                            }
                        
                        if (playerInteracionHUB.CanInteractTutorial)
                        {
                            playerInteracionHUB.Tutorial_Panel.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
        //broñ

        if (AttackbeatsCounter > 0)
        {
            return;
        }  
        if (blockCounter > 0)
        {
            // Jeœli gracz jest zablokowany, zmniejsz licznik na kolejnych beatach
            return;
        }
        
        if (AttackTimer > 0  && !BlockIsActive)
        {
            
            AttackTimer -= Time.deltaTime; // Zmniejsz timer o czas miniony od ostatniej klatki
            if (CanAttackUI)
            {
                //CanAttack.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space) && weaponManager.Weapons[weaponManager.WeaponIndex] != null && weaponManager.WeaponIndex != 3)
                {
                    player_anim.SetTrigger("TriggerAttack");
                    if (CanAttackCommon)
                    {
                        
                        //player_anim.SetBool("PlayerCanAttack", true);
                        if (weaponManager.Weapons[weaponManager.WeaponIndex] != null && weaponManager.WeaponIndex == 0)
                        {
                            player_anim.SetTrigger("TriggerAttack");
                            common.TakeDamage(Pickaxe.Damage);/* -= Pickaxe.Damage;*/
                        }
                        else if(weaponManager.Weapons[weaponManager.WeaponIndex] != null && weaponManager.WeaponIndex == 1)
                        {
                            player_anim.SetTrigger("TriggerAttack");
                            common.TakeDamage(Hammer.Damage); /*-= Hammer.Damage;*/
                        }
                        else if(weaponManager.Weapons[weaponManager.WeaponIndex] != null && weaponManager.WeaponIndex == 2)
                        {
                            player_anim.SetTrigger("TriggerAttack");
                            common.TakeDamage(Axe.Damage); /*-= Axe.Damage;*/
                        }
                        
                        
                        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Punch, this.transform.position);
                        
                    }
                    if (CanAttackRanger)
                    {
                        
                        //player_anim.SetBool("PlayerCanAttack", true);
                        if (weaponManager.Weapons[weaponManager.WeaponIndex] != null && weaponManager.WeaponIndex == 0)
                        {
                            player_anim.SetTrigger("TriggerAttack");
                            ranger.TakeDamage(Pickaxe.Damage);
                        }
                        else if (weaponManager.Weapons[weaponManager.WeaponIndex] != null && weaponManager.WeaponIndex == 1)
                        {
                            player_anim.SetTrigger("TriggerAttack");
                            ranger.TakeDamage(Hammer.Damage);
                        }
                        else if (weaponManager.Weapons[weaponManager.WeaponIndex] != null && weaponManager.WeaponIndex == 2)
                        {
                            player_anim.SetTrigger("TriggerAttack");
                            ranger.TakeDamage(Axe.Damage);
                        }

                         
                        
                        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Punch, this.transform.position);
                    }
                    if(CanAttackTank)
                    {
                        
                        //player_anim.SetBool("PlayerCanAttack", true);
                        if (weaponManager.Weapons[weaponManager.WeaponIndex] != null && weaponManager.WeaponIndex == 0)
                        {
                            player_anim.SetTrigger("TriggerAttack");
                            tank.TakeDamage(Pickaxe.Damage);
                        }
                        else if (weaponManager.Weapons[weaponManager.WeaponIndex] != null && weaponManager.WeaponIndex == 1)
                        {
                            player_anim.SetTrigger("TriggerAttack");
                            tank.TakeDamage(Hammer.Damage);
                        }
                        else if (weaponManager.Weapons[weaponManager.WeaponIndex] != null && weaponManager.WeaponIndex == 2)
                        {
                            player_anim.SetTrigger("TriggerAttack");
                            tank.TakeDamage(Axe.Damage);
                        }

                         
                        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Punch, this.transform.position);
                    }
                    if (playerInteracionHUB != null)
                    {
                        
                        if (playerInteracionHUB.CanInteractCraft)
                        {
                            playerInteracionHUB.CraftPanel.gameObject.SetActive(true);
                            
                        }
                        if (playerInteracionHUB.CanInteractShop)
                        {
                            playerInteracionHUB.WeaponPanel.gameObject.SetActive(true);
                            
                        }
                        if(playerInteracionHUB.CanInteractTutorial)
                        {
                            playerInteracionHUB.Tutorial_Panel.gameObject.SetActive(true);
                        }
                    }
                }
                
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.Space)  && !BlockIsActive)
        {
            blockCounter = BlockDuration;
        }
        if (AttackTimer <= 0)
        {
            AttackTimer = 0;
            //CanAttack.SetActive(false);
            //AttackbeatsCounter = Attackbeats;
        }


    }
    public void PlayerAttacking()
    {
        //player_anim.SetBool("PlayerCanAttack", false);
        if (AttackbeatsCounter > 0)
        {
            AttackbeatsCounter--;
        }
        if (blockCounter > 0)
        {
            blockCounter--;
            BlockIsActive = true;

        }
        if(blockCounter <= 0 && AttackbeatsCounter <= 0)
        {
            AttackTimer = AttackWindow;
            BlockIsActive = false;
            

        }
        
        



    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Common"))
        {

            common = other.gameObject.GetComponent<Common>();

        }
        if (other.gameObject.CompareTag("Ranger"))
        {
            ranger = other.gameObject.GetComponent<Ranger>();

        }
        if (other.gameObject.CompareTag("Tank"))
        {
            tank = other.gameObject.GetComponent<Tank>();

        }
    }
    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.CompareTag("Common"))
        {
            
            CanAttackCommon = true;
            
        }
        if (other.gameObject.CompareTag("Ranger"))
        {
            CanAttackRanger = true;

        }
        if (other.gameObject.CompareTag("Tank"))
        {
            CanAttackTank = true;

        }


    }
    private void OnTriggerExit(Collider other)
    {
        CanAttackCommon = false;
        CanAttackRanger = false;
        CanAttackTank = false;
    }
}
