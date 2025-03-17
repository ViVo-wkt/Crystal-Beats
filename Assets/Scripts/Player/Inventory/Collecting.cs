using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Collecting : MonoBehaviour
{
    public Crystal crystal;
    public InventoryManager inventoryManager;

    private bool CanAddCrystal = false;
    private void Start()
    {
        inventoryManager = GameObject.FindWithTag("Manager").GetComponent<InventoryManager>();
    }
    void PickUpCrystal()
    {

        InventoryManager.Instance.Add(crystal);
        Destroy(gameObject);
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.CollectCrystal, this.transform.position);
    }
    

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.CompareTag("Player"))
    //    {
    //        PickUpCrystal();
    //        inventoryManager.ListCrystals(crystal);
    //    }
    //}
    public void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            CanAddCrystal = true;
            
            
        }
    }
    public void OnTriggerStay(Collider other)
    {
        switch (crystal.id)
        {

            case 0:
        //ogranicznik do zbierania ma³ych kryszta³ów
        if (inventoryManager.Inventory_SingleCrystal_Slots.Count >= inventoryManager.SingleCrystal_slots)
        {
            gameObject.tag = "Wall";
        }
        else if (inventoryManager.Inventory_SingleCrystal_Slots.Count < inventoryManager.SingleCrystal_slots)
        {
            gameObject.tag = "Crystal";
        }
                break;
            case 1:
        //ogranicznik do zbierania ma³ych clustrów
        if (inventoryManager.Inventory_SmallCrystalCluster_Slots.Count >= inventoryManager.SmallClusters_slots)
        {
            gameObject.tag = "Wall";
        }
        else if (inventoryManager.Inventory_SmallCrystalCluster_Slots.Count < inventoryManager.SmallClusters_slots)
        {
            gameObject.tag = "Crystal";
        }
                break;
            case 2:
        // ogranicznik do zbierania du¿ych clustrów 
        if (inventoryManager.Inventory_LargeCrystalCLuster_Slots.Count >= inventoryManager.LargeClusters_slots)
        {
            gameObject.tag = "Wall";
        }
        else if (inventoryManager.Inventory_LargeCrystalCLuster_Slots.Count < inventoryManager.LargeClusters_slots)
        {
            gameObject.tag = "Crystal";
        }
                break;
            case 3:
        //ogranicznik do zbierania czerwonych clustrów
        if (inventoryManager.Inventory_RedCrystalCluster_Slots.Count >= inventoryManager.RedClusters_slots)
        {
            gameObject.tag = "Wall";
        }
        else if (inventoryManager.Inventory_RedCrystalCluster_Slots.Count < inventoryManager.RedClusters_slots)
        {
            gameObject.tag = "Crystal";
        }
                break;
            default:
                break;
    }
        
    }
    public void Update()
    {
        if(CanAddCrystal)
        {
            PickUpCrystal();
            InventoryManager.Instance.ListCrystals(crystal);
            CanAddCrystal = false;
        }
    }


}
