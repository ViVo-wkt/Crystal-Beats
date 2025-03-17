using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
     public Crystal crystal;
     public InventoryManager inventoryManager;
    public ImageCrystal imageCrystal;


    public InventoryCrystalController inventoryCrystalController;
    public InventoryCrumbleController inventoryCrumbleController;
    public void Start()
    {

        inventoryManager = GameObject.FindWithTag("Manager").GetComponent<InventoryManager>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        ImageCrystal draggedCrystal = eventData.pointerDrag.GetComponent<ImageCrystal>();
        if (transform.childCount == 0)
        {
            ImageCrystal imagecrystal = eventData.pointerDrag.GetComponent<ImageCrystal>();
            imagecrystal.parentAfterDrag = transform;
            
        }
        if (transform.childCount == 1 && inventoryManager.Inventory_SmallCrystalCluster_Slots.Count < inventoryManager.SmallClusters_slots && imageCrystal.CompareTag("SingleCrystal")&& draggedCrystal.CompareTag("SingleCrystal"))
        {
            if(inventoryManager.Inventory_SingleCrystal_Slots.Count >= inventoryManager.NumberOfMergedSingleCrystals && Input.GetMouseButtonUp(0) && inventoryManager.Inventory_SingleCrystal_Slots != null)
            {
                //dodaje smallcluster
                inventoryManager.Inventory_SmallCrystalCluster_Slots.Add(crystal);
                inventoryManager.ListCrystals(crystal);
                Debug.Log("wariant1");
                inventoryCrystalController.DestroySingleCrystals();
            }
            else
            {
                Debug.Log("Nie uda³o siê zmergowaæ single crystals");
            }
            
            

        }
        

        if (transform.childCount == 1 && inventoryManager.Inventory_LargeCrystalCLuster_Slots.Count < inventoryManager.LargeClusters_slots && imageCrystal.CompareTag("SmallCluster")&& draggedCrystal.CompareTag("SmallCluster"))
        {

            if (inventoryManager.Inventory_SmallCrystalCluster_Slots.Count >= inventoryManager.NumberOfMergedSmallCLusters && Input.GetMouseButtonUp(0) && inventoryManager.Inventory_SmallCrystalCluster_Slots != null)
            {
                //dodaje large cluster
                Debug.Log("wariant2");
                inventoryManager.Inventory_LargeCrystalCLuster_Slots.Add(crystal);
                inventoryManager.ListCrystals(crystal);
                inventoryCrystalController.DestroySmallClusters();
            }
            else 
            {
                Debug.Log("nie uda³o siê zmergowaæ small clusters");
            }
            
        }
        
        

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            InventoryCrystalController clickedCrystal = eventData.pointerClick.GetComponent<InventoryCrystalController>();
            
            if(transform.childCount == 1 && clickedCrystal.crystal.id == 2)
            {
                if(inventoryManager.Inventory_SmallCrystalCluster_Slots.Count + inventoryManager.SmallClustersAddedFromCrumble < inventoryManager.SmallClusters_slots)
                {
                    if (inventoryManager.Inventory_LargeCrystalCLuster_Slots.Count >= inventoryManager.NumberOfDestroyedLargeClusters && inventoryManager.Inventory_LargeCrystalCLuster_Slots != null)
                    {
                        inventoryCrystalController.CrumbleLargeCluster();
                        inventoryCrumbleController.AddSmallClustersFromCrumble();
                    }
                    else
                    {
                        Debug.Log("Nie ma wystarczaj¹cej iloœci kryszta³ów do zkruszenia");
                    }
                }
                else
                {
                    Debug.Log("Nie ma miejsca w Smallcluster_slots");
                }    
                
            }
            else
            {
                Debug.Log("nie ma miejsca na kruszenie");
            }
            if(transform.childCount == 1 && clickedCrystal.crystal.id == 1)
            {
                if (inventoryManager.Inventory_SingleCrystal_Slots.Count + inventoryManager.SingleCrystalsAddedFromCrumble < inventoryManager.SingleCrystal_slots)
                {
                    if (inventoryManager.Inventory_SmallCrystalCluster_Slots.Count >= inventoryManager.NumberOfDestroyedSmallClusters && inventoryManager.Inventory_SmallCrystalCluster_Slots != null)
                    {
                        inventoryCrumbleController.AddSingleCrystalFromCrumble();
                        inventoryCrystalController.CrumbleSmallCluster();
                    }
                    else 
                    {
                        Debug.Log("Nie ma wystarczaj¹cej iloœci kryszta³ów do zkruszenia");
                    }
                    
                }
                else
                {
                    Debug.Log("Nie ma miejsca w singleCrystals_slots");
                }
            }
            if (transform.childCount == 1 && clickedCrystal.crystal.id == 3)
            {
                Debug.Log("Obiekt nie do kruszenia");
            }
            //else
            //{
            //    throw new NotImplementedException();
            //}

        }
    }
    
}
