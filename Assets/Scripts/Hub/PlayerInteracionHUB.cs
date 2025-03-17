using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteracionHUB : MonoBehaviour
{
    public CraftArea craftArea;
    public WeaponShop weaponShop;

    public GameObject WeaponPanel;
    public GameObject CraftPanel;
    public GameObject Tutorial_Panel;

    public bool CanInteractCraft;
    public bool CanInteractShop;
    public bool CanInteractTutorial;
    public PlayerAttack playerAttack;


    public Inventory inventory;
    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.CompareTag("CraftWorkshop") && !inventory.PausePanel.activeSelf && !inventory.Inventory_Panel.activeSelf)
        {
            CanInteractCraft = true;
        }
        if (other.gameObject.CompareTag("WeaponWorkshop") && !inventory.PausePanel.activeSelf && !inventory.Inventory_Panel.activeSelf)
        {
            
            CanInteractShop = true;
        }
        if(other.gameObject.CompareTag("Tutorial") && !inventory.PausePanel.activeSelf && !inventory.Inventory_Panel.activeSelf)
        {
            CanInteractTutorial = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        CanInteractTutorial = false;

        CanInteractCraft = false;
        CanInteractShop = false;
        CraftPanel.SetActive(false);
        WeaponPanel.SetActive(false);
    }
}
