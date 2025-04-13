using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteracionHUB : MonoBehaviour
{
    public CraftArea craftArea;
    public WeaponShop weaponShop;

    public GameObject WeaponPanel;
    public GameObject CraftPanel;
    public GameObject Tutorial_Panel;
    public GameObject Lore_Panel;
    public GameObject[] Lore_Pages;
    private int Page_Index;


    public bool CanInteractInHub;
    public bool CanInteractCraft;
    public bool CanInteractShop;
    public bool CanInteractTutorial;
    public bool CanInteractLore;
    public PlayerAttack playerAttack;


    public Inventory inventory;
    private void OnTriggerStay(Collider other)
    {
        
        Lore();
        
        if (other.gameObject.CompareTag("CraftWorkshop"))
        {
            if(!inventory.PausePanel.activeSelf && !inventory.Inventory_Panel.activeSelf)
            {
                CanInteractCraft = true;
            }
            
        }
        else if (other.gameObject.CompareTag("WeaponWorkshop") && !inventory.PausePanel.activeSelf && !inventory.Inventory_Panel.activeSelf)
        {

            CanInteractShop = true;
        }
        else if (other.gameObject.CompareTag("Tutorial") && !inventory.PausePanel.activeSelf && !inventory.Inventory_Panel.activeSelf)
        {
            CanInteractTutorial = true;
        }
        else if (other.gameObject.CompareTag("Lore") && !inventory.PausePanel.activeSelf && !inventory.Inventory_Panel.activeSelf)
        {
            CanInteractLore = true;

        }

    }
    private void OnTriggerExit(Collider other)
    {

        
        CanInteractTutorial = false;
        CanInteractLore = false;
        CanInteractCraft = false;
        CanInteractShop = false;
        CraftPanel.SetActive(false);
        WeaponPanel.SetActive(false);
    }
    private void Lore()
    {
        switch (Page_Index)
        {
            case 0:
                foreach (GameObject item in Lore_Pages)
                {
                    item.SetActive(false);

                }

                Lore_Pages[0].SetActive(true);
                break;
            case 1:
                foreach (GameObject item in Lore_Pages)
                {
                    item.SetActive(false);

                }

                Lore_Pages[1].SetActive(true);
                break;
            case 2:
                foreach (GameObject item in Lore_Pages)
                {
                    item.SetActive(false);

                }

                Lore_Pages[2].SetActive(true);
                break;
            case 3:
                foreach (GameObject item in Lore_Pages)
                {
                    item.SetActive(false);

                }

                Lore_Pages[3].SetActive(true);
                break;
            default:
                break;
        }
    }

    //Buttons-----------------
    public void OnClickPage1()
    {
        Page_Index = 0;
    }
    public void OnClickPage2()
    {
        Page_Index = 1;
    }
    public void OnClickPage3()
    {
        Page_Index = 2;
    }
    public void OnClickPage4()
    {
        Page_Index = 3;
    }
    public void OnClickExit_Lore()
    {
        Page_Index = 0;
        Lore_Panel.SetActive(false);
        
    }
    //Buttons------------------------
}
