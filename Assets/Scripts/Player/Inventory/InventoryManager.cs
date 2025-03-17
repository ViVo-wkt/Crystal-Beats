using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    InventoryCrystalController InventoryCrystalController;
    public int NumberOfMergedSingleCrystals;
    public int NumberOfMergedSmallCLusters;

    public int NumberOfDestroyedLargeClusters;
    public int NumberOfDestroyedSmallClusters;

    public int SingleCrystalsAddedFromCrumble;
    public int SmallClustersAddedFromCrumble;



    public List<Crystal> Inventory_SingleCrystal_Slots = new List<Crystal>();
    public List<Crystal> Inventory_SmallCrystalCluster_Slots = new List<Crystal>();
    public List<Crystal> Inventory_LargeCrystalCLuster_Slots = new List<Crystal>();
    public List<Crystal> Inventory_RedCrystalCluster_Slots = new List<Crystal>();
    [field: Header("Slots Size")]
    public int SingleCrystal_slots;
    public int SmallClusters_slots;
    public int LargeClusters_slots;
    public int RedClusters_slots;



    public Transform[] CrystalContent;

    public GameObject[] InventoryCrystal;
    public void Awake()
    {
        Instance = this;
    }



    public void Add(Crystal crystal)
    {

        switch (crystal.id)
        {
            case 0:
                {
                    if (Inventory_SingleCrystal_Slots.Count < SingleCrystal_slots)
                    {
                        Inventory_SingleCrystal_Slots.Add(crystal);
                    }
                    else
                    {
                        Debug.Log("InventoryFull");
                    }

                    break;
                }
            case 1:
                {
                    if (Inventory_SmallCrystalCluster_Slots.Count < SmallClusters_slots)
                    {
                        Inventory_SmallCrystalCluster_Slots.Add(crystal);

                    }
                    else
                    {
                        Debug.Log("InventoryFull");
                    }
                    break;
                }
            case 2:
                {
                    if (Inventory_LargeCrystalCLuster_Slots.Count < LargeClusters_slots)
                    {
                        Inventory_LargeCrystalCLuster_Slots.Add(crystal);
                    }
                    else
                    {
                        Debug.Log("InventoryFull");
                    }
                    break;
                }
            case 3:
                {
                    if (Inventory_RedCrystalCluster_Slots.Count < RedClusters_slots)
                    {
                        Inventory_RedCrystalCluster_Slots.Add(crystal);
                    }
                    else
                    {
                        Debug.Log("InventoryFull");
                    }
                    break;
                }
            default:
                break;
        }

    }
    public void Remove(Crystal crystal)
    {
        switch (crystal.id)
        {
            case 0:
                {
                    if (Inventory_SingleCrystal_Slots.Count < SingleCrystal_slots + 1)
                    {
                        Inventory_SingleCrystal_Slots.Remove(crystal);
                    }
                    else
                    {
                        Debug.Log("InventoryFull");
                    }

                    break;
                }
            case 1:
                {
                    if (Inventory_SmallCrystalCluster_Slots.Count < SmallClusters_slots + 1)
                    {
                        Inventory_SmallCrystalCluster_Slots.Remove(crystal);

                    }
                    else
                    {
                        Debug.Log("InventoryFull");
                    }
                    break;
                }
            case 2:
                {
                    if (Inventory_LargeCrystalCLuster_Slots.Count < LargeClusters_slots + 1)
                    {
                        Inventory_LargeCrystalCLuster_Slots.Remove(crystal);
                    }
                    else
                    {
                        Debug.Log("InventoryFull");
                    }
                    break;
                }
            case 3:
                {
                    if (Inventory_RedCrystalCluster_Slots.Count < RedClusters_slots + 1)
                    {
                        Inventory_RedCrystalCluster_Slots.Remove(crystal);
                    }
                    else
                    {
                        Debug.Log("InventoryFull");
                    }
                    break;
                }
            default:
                break;
        }
    }
    public void ListCrystals(Crystal crystal)
    {


        switch (crystal.id)
        {


            case 0:
                {
                    if (Inventory_SingleCrystal_Slots.Count < SingleCrystal_slots + 1)
                    {
                        foreach (Transform item in CrystalContent[crystal.id])
                        {
                            Destroy(item.gameObject);

                        }
                        foreach (var item in Inventory_SingleCrystal_Slots)
                        {
                            GameObject obj = Instantiate(InventoryCrystal[crystal.id], CrystalContent[crystal.id]);
                            var Crystalicon = obj.transform.Find("ItemIconSingle").GetComponent<Image>();

                            Crystalicon.sprite = crystal.icon;
                        }
                    }
                    else
                    {
                        Debug.Log("InventoryFull");
                    }
                    break;
                }
            case 1:
                {
                    if (Inventory_SmallCrystalCluster_Slots.Count < SmallClusters_slots + 1)
                    {
                        foreach (Transform item in CrystalContent[crystal.id])
                        {
                            Destroy(item.gameObject);

                        }

                        foreach (var item in Inventory_SmallCrystalCluster_Slots)
                        {
                            GameObject obj = Instantiate(InventoryCrystal[crystal.id], CrystalContent[crystal.id]);
                            var Crystalicon = obj.transform.Find("ItemIconSmall").GetComponent<Image>();

                            Crystalicon.sprite = crystal.icon;
                        }
                    }
                    else
                    {
                        Debug.Log("InventoryFull");
                    }
                    break;
                }
            case 2:
                {
                    if (Inventory_LargeCrystalCLuster_Slots.Count < LargeClusters_slots + 1)
                    {
                        foreach (Transform item in CrystalContent[crystal.id])
                        {
                            Destroy(item.gameObject);

                        }
                        foreach (var item in Inventory_LargeCrystalCLuster_Slots)
                        {
                            GameObject obj = Instantiate(InventoryCrystal[crystal.id], CrystalContent[crystal.id]);
                            var Crystalicon = obj.transform.Find("ItemIconLarge").GetComponent<Image>();

                            Crystalicon.sprite = crystal.icon;
                        }
                    }
                    else
                    {
                        Debug.Log("InventoryFull");
                    }
                    break;
                }
            case 3:
                {
                    if (Inventory_RedCrystalCluster_Slots.Count < RedClusters_slots + 1)
                    {
                        foreach (Transform item in CrystalContent[crystal.id])
                        {
                            Destroy(item.gameObject);

                        }
                        foreach (var item in Inventory_RedCrystalCluster_Slots)
                        {
                            GameObject obj = Instantiate(InventoryCrystal[crystal.id], CrystalContent[crystal.id]);
                            var Crystalicon = obj.transform.Find("ItemIconRed").GetComponent<Image>();

                            Crystalicon.sprite = crystal.icon;
                        }
                    }
                    else
                    {
                        Debug.Log("InventoryFull");
                    }
                    break;
                }
            default:

                break;
        }

    }

    public void Save(ref InventorySavedData data)
    {
        data.singleCrystalSlots = Inventory_SingleCrystal_Slots;
        data.SmallCrystalSlots = Inventory_SmallCrystalCluster_Slots;
        data.LargeCrystalSlots = Inventory_LargeCrystalCLuster_Slots;
        data.RedCrystalSlots = Inventory_RedCrystalCluster_Slots;

        

    }
    public void Load(InventorySavedData data)
    {
        Inventory_SingleCrystal_Slots = data.singleCrystalSlots;
        Inventory_SmallCrystalCluster_Slots = data.SmallCrystalSlots;
        Inventory_LargeCrystalCLuster_Slots = data.LargeCrystalSlots;
        Inventory_RedCrystalCluster_Slots = data.RedCrystalSlots;

        foreach (var crystal in Inventory_SingleCrystal_Slots)
        {
            ListCrystals(crystal);
        }

        foreach (var crystal in Inventory_SmallCrystalCluster_Slots)
        {
            ListCrystals(crystal);
        }

        foreach (var crystal in Inventory_LargeCrystalCLuster_Slots)
        {
            ListCrystals(crystal);
        }

        foreach (var crystal in Inventory_RedCrystalCluster_Slots)
        {
            ListCrystals(crystal);
        }
    }
    
}
[System.Serializable]
public struct InventorySavedData
{
    public List<Crystal> singleCrystalSlots;
    public List<Crystal> SmallCrystalSlots;
    public List<Crystal> LargeCrystalSlots;
    public List<Crystal> RedCrystalSlots;

    

}
