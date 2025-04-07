using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public GameObject Inventory_Panel;

    public InventoryManager inventoryManager;
    
    public TextMeshProUGUI[] SlotsInfo;

    public GameObject PausePanel;

    public GameObject DeathPanel;

    

    public Texture2D CursorTexture;
    
    private CursorMode cursorMode = CursorMode.Auto;
    private bool CursorSet;

    public PlayerInteracionHUB playerInteracionHUB;
    public Tutorial_Guid_Panels tutorial_Guid_Panel;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {

            Destroy(gameObject);
        }    
        //inventoryManager = GameObject.FindWithTag("Manager").GetComponent<InventoryManager>();
        if(!CursorSet)
        {
            Cursor.SetCursor(CursorTexture, new Vector2(CursorTexture.width / 2, 0), cursorMode);
            CursorSet = true;
        }
        

        
    }
    

    // Update is called once per frame
    void Update()
    {
        if (playerInteracionHUB == null && tutorial_Guid_Panel == null)//sceny Levele
        {

            
            if (Inventory_Panel.activeSelf || PausePanel.activeSelf || DeathPanel.activeSelf)
            {

                Cursor.lockState = CursorLockMode.None;
                CursorVisibility(true);

            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                CursorVisibility(false);
            }
        }
        
        if (playerInteracionHUB != null)// scena HUB
        {
            
            if (playerInteracionHUB.WeaponPanel.activeSelf || playerInteracionHUB.CraftPanel.activeSelf || Inventory_Panel.activeSelf || PausePanel.activeSelf || DeathPanel.activeSelf || playerInteracionHUB.Tutorial_Panel.activeSelf || playerInteracionHUB.Lore_Panel.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                CursorVisibility(true);
            }
            else 
            {
                Cursor.lockState = CursorLockMode.Locked;
                CursorVisibility(false);
            }
        }
        if (tutorial_Guid_Panel != null)// scena Tutorial
        {
            
            if(Inventory_Panel.activeSelf || PausePanel.activeSelf || DeathPanel.activeSelf || tutorial_Guid_Panel.GuidePanels[0].activeSelf || tutorial_Guid_Panel.GuidePanels[1].activeSelf || tutorial_Guid_Panel.GuidePanels[2].activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                CursorVisibility(true);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                CursorVisibility(false);
            }
        }

        //Level
        if (playerInteracionHUB == null && tutorial_Guid_Panel == null)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !Inventory_Panel.activeSelf && !DeathPanel.activeSelf)
            {
                PausePanel.SetActive(!PausePanel.activeSelf);

            }
            if (Input.GetKeyDown(KeyCode.Tab) && !PausePanel.activeSelf && !DeathPanel.activeSelf)
            {
                Inventory_Panel.SetActive(!Inventory_Panel.activeSelf);
            }
        }
        //Level

        //HUB
        if (playerInteracionHUB != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !Inventory_Panel.activeSelf && !DeathPanel.activeSelf)
            {
                PausePanel.SetActive(!PausePanel.activeSelf);

            }
            if (Input.GetKeyDown(KeyCode.Tab) && !PausePanel.activeSelf && !DeathPanel.activeSelf)
            {
                Inventory_Panel.SetActive(!Inventory_Panel.activeSelf);
            }
            if(playerInteracionHUB.WeaponPanel.activeSelf || playerInteracionHUB.CraftPanel.activeSelf)
            {
                PausePanel.SetActive(false);
                Inventory_Panel.SetActive(false);
            }
        }
        //HUB

        //Tutorial
        if (tutorial_Guid_Panel != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !Inventory_Panel.activeSelf && !DeathPanel.activeSelf)
            {
                PausePanel.SetActive(!PausePanel.activeSelf);

            }
            if (Input.GetKeyDown(KeyCode.Tab) && !PausePanel.activeSelf && !DeathPanel.activeSelf)
            {
                Inventory_Panel.SetActive(!Inventory_Panel.activeSelf);
            }
            if(tutorial_Guid_Panel.GuidePanels[0].activeSelf || tutorial_Guid_Panel.GuidePanels[1].activeSelf || tutorial_Guid_Panel.GuidePanels[2].activeSelf)
            {
                Inventory_Panel.SetActive(false);
                PausePanel.SetActive(false);

            }
            
        }
        //Tutorial

        //if (Inventory_Panel.activeSelf) ////liczyd³o
        //{
        //    foreach (var item in SlotsInfo)
        //    {
        //        SlotsInfo[0].text = "Single Crystals slots left: " + inventoryManager.Inventory_SingleCrystal_Slots.Count.ToString() + "/40";
        //        SlotsInfo[1].text = "Small Clusters slots left: " + inventoryManager.Inventory_SmallCrystalCluster_Slots.Count.ToString() + "/20";
        //        SlotsInfo[2].text = "Large Clusters slots left: " + inventoryManager.Inventory_LargeCrystalCLuster_Slots.Count.ToString() + "/9";
        //        SlotsInfo[3].text = "Red Clusters slots left: " + inventoryManager.Inventory_RedCrystalCluster_Slots.Count.ToString() + "/8";
        //        break;
        //    }
          
        //}
        
        
    }
    public void CursorVisibility(bool CursorActive)

    {
        if(CursorActive)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
            
        }
    }
}
