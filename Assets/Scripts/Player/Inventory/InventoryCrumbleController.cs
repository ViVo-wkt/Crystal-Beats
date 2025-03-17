using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCrumbleController : MonoBehaviour
{
    public Crystal crystal;
    public InventoryManager inventoryManager;
    
    void Start()
    {
        inventoryManager = GameObject.FindWithTag("Manager").GetComponent<InventoryManager>();
    }

    public void AddSmallClustersFromCrumble()//dodanie pomniejszych kryszta³ów
    {
        for (int i = InventoryManager.Instance.SmallClustersAddedFromCrumble; i > 0; i--)
        {
            if (crystal != null)
            {

                InventoryManager.Instance.Add(crystal);
                InventoryManager.Instance.ListCrystals(crystal);

            }
        }
            
    }
    public void AddSingleCrystalFromCrumble()
    {
        for (int i = InventoryManager.Instance.SingleCrystalsAddedFromCrumble; i > 0; i--)
        {
            if (crystal != null)
            {

                InventoryManager.Instance.Add(crystal);
                InventoryManager.Instance.ListCrystals(crystal);

            }
        }
    }
}
