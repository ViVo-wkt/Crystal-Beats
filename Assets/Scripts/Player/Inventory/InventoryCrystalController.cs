using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCrystalController : MonoBehaviour
{
    public Crystal crystal;
    public InventoryManager inventoryManager;

    
    private void Start()
    {
        
        inventoryManager = GameObject.FindWithTag("Manager").GetComponent<InventoryManager>();
    }
    public void DestroySingleCrystals()//niszczenie single crystal aby stworzyæ small cluster
    {
        
        for (int i = InventoryManager.Instance.NumberOfMergedSingleCrystals ; i > 0; i--)
        {
            if(crystal != null)
            {
                InventoryManager.Instance.Remove(crystal);
                InventoryManager.Instance.ListCrystals(crystal);
                Destroy(gameObject);
                Texture.Destroy(gameObject);
            }
            
        }
        
        
    }
    public void DestroySmallClusters()//niszczenie ma³ego clustera podczas mergowania na large cluster
    {
        for (int i = InventoryManager.Instance.NumberOfMergedSmallCLusters; i > 0; i--)
        {
            if(crystal != null)
            {
                InventoryManager.Instance.Remove(crystal);
                InventoryManager.Instance.ListCrystals(crystal);
                Destroy(gameObject);
                Texture.Destroy(gameObject);
            }
            
        }
    }
    public void CrumbleLargeCluster()//kruszenie du¿ego kryszta³u
    {
        for (int i = InventoryManager.Instance.NumberOfDestroyedLargeClusters; i > 0; i--)
        {
            if (crystal != null)
            {
                InventoryManager.Instance.Remove(crystal);
                InventoryManager.Instance.ListCrystals(crystal);
                Destroy(gameObject);
                Texture.Destroy(gameObject);
            }
        }
    }
    public void CrumbleSmallCluster()
    {
        for (int i = InventoryManager.Instance.NumberOfDestroyedSmallClusters; i > 0; i--)
        {
            if (crystal != null)
            {
                InventoryManager.Instance.Remove(crystal);
                InventoryManager.Instance.ListCrystals(crystal);
                Destroy(gameObject);
                Texture.Destroy(gameObject);
            }
        }
    }
    
}
