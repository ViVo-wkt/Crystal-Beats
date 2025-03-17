using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CraftArea : MonoBehaviour
{
    public Crystal SingleCrystal;
    public Crystal SmallCluster;
    public Crystal LargeCluster;
    public Crystal RedCluster;
    public Potion SmallPotion;
    public Potion MediumPotion;
    public Potion BigPotion;

    public InventoryManager inventoryManager;
    public LayoutPotions layoutPotions;

    public GameObject[] SlotsPrefabs;
    private GameObject[] Slots;
    // Start is called before the first frame update
    void Start()
    {
        Slots = new GameObject[SlotsPrefabs.Length];
        
    }

    

    public void CreateSmallPotion()
    {
        RefreshInstances();
        for (int i = 1; i > 0; i--)// i = 1 ile pojedyñczych kryszta³ów zniszczyæ
        {

            if (inventoryManager.Inventory_SingleCrystal_Slots.Count >= 1 && inventoryManager.Inventory_RedCrystalCluster_Slots.Count >= 1 && inventoryManager.Inventory_SingleCrystal_Slots != null && inventoryManager.Inventory_RedCrystalCluster_Slots != null)
            {

                if (SingleCrystal != null && RedCluster != null && Slots[0] != null && Slots[3] != null)
                {
                    InventoryManager.Instance.Remove(SingleCrystal);
                    InventoryManager.Instance.ListCrystals(SingleCrystal);
                    Destroy(Slots[0]);
                    
                    InventoryManager.Instance.Remove(RedCluster);
                    InventoryManager.Instance.ListCrystals(RedCluster);
                    Destroy(Slots[3]);
                    Debug.Log("Craft");
                    LayoutPotions.Instance.AddPotion(SmallPotion);
                    layoutPotions.ShowLayoutSmallPotions();

                    AudioManager.Instance.PlayOneShot(FMODEvents.Instance.CraftPotion, this.transform.position);
                    
                }
                else
                {
                    Debug.Log("Bug");
                }
            }
            else
            {
                Debug.Log("Nie masz wszystkich kryszta³ów do zrobienia SmallPotion");
            }
        }
        
    }
    public void CreateMediumPotion()
    {
        RefreshInstances();
        for (int i = 1; i > 0; i--)// i = 1 ile pojedyñczych kryszta³ów zniszczyæ
        {
            if (inventoryManager.Inventory_SingleCrystal_Slots.Count >= 1 && inventoryManager.Inventory_RedCrystalCluster_Slots.Count >= 1 && inventoryManager.Inventory_SmallCrystalCluster_Slots.Count >= 1)
            {
                if (SingleCrystal != null && RedCluster != null && SmallCluster != null && Slots[0] != null && Slots[1] != null && Slots[3] != null)
                {
                    InventoryManager.Instance.Remove(SingleCrystal);
                    InventoryManager.Instance.ListCrystals(SingleCrystal);
                    Destroy(Slots[0]);

                    InventoryManager.Instance.Remove(SmallCluster);
                    InventoryManager.Instance.ListCrystals(SmallCluster);
                    Destroy(Slots[1]);

                    InventoryManager.Instance.Remove(RedCluster);
                    InventoryManager.Instance.ListCrystals(RedCluster);
                    Destroy(Slots[3]);
                    LayoutPotions.Instance.AddPotion(MediumPotion);
                    layoutPotions.ShowLayoutMediumPotions();
                    AudioManager.Instance.PlayOneShot(FMODEvents.Instance.CraftPotion, this.transform.position);
                }
                else
                {
                    Debug.Log("Bug");
                }
            }
            else
            {
                Debug.Log("Nie masz wszystkich kryszta³ów do zrobienia MediumPotion");
            }
        }
    }
    public void CreateBigPotion()
    {
        RefreshInstances();
        for (int i = 1; i > 0; i--)// i = 1 ile pojedyñczych kryszta³ów zniszczyæ
        {
            if (inventoryManager.Inventory_SingleCrystal_Slots.Count >= 1 && inventoryManager.Inventory_RedCrystalCluster_Slots.Count >= 1 && inventoryManager.Inventory_SmallCrystalCluster_Slots.Count >= 1 && inventoryManager.Inventory_LargeCrystalCLuster_Slots.Count >= 1)
            {
                if (SingleCrystal != null && RedCluster != null && SmallCluster != null && LargeCluster != null && Slots[0] != null && Slots[1] != null && Slots[2] != null && Slots[3] != null)
                {
                    InventoryManager.Instance.Remove(SingleCrystal);
                    InventoryManager.Instance.ListCrystals(SingleCrystal);
                    Destroy(Slots[0]);

                    InventoryManager.Instance.Remove(SmallCluster);
                    InventoryManager.Instance.ListCrystals(SmallCluster);
                    Destroy(Slots[1]);

                    InventoryManager.Instance.Remove(LargeCluster);
                    InventoryManager.Instance.ListCrystals(LargeCluster);
                    Destroy(Slots[2]);

                    InventoryManager.Instance.Remove(RedCluster);
                    InventoryManager.Instance.ListCrystals(RedCluster);
                    Destroy(Slots[3]);
                    LayoutPotions.Instance.AddPotion(BigPotion);
                    layoutPotions.ShowLayoutBigPotions();
                    AudioManager.Instance.PlayOneShot(FMODEvents.Instance.CraftPotion, this.transform.position);
                }
                else
                {
                    Debug.Log("Bug");
                }
            }
            else
            {
                Debug.Log("Nie masz wszystkich kryszta³ów do zrobienia BigPotion");
            }
        }
    }
    private void RefreshInstances()
    {
        for (int i = 0; i < SlotsPrefabs.Length; i++)
        {
            if (SlotsPrefabs[i] != null)
            {
                Slots[i] = Instantiate(SlotsPrefabs[i], transform); // Instancjonowanie pod obiektem CraftArea

            }
        }
    }
    
}
