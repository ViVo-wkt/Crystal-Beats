using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    public static WeaponShop instance;

    public Crystal SingleCrystal;
    public Crystal SmallCluster;
    public Crystal LargeCluster;

    public WeaponManager weaponManager;
    public Weapon Hammer;
    public Weapon Axe;
    public Weapon Gun;

    [Header("Hammer")]
    public int SingleCrystalHammerCost;
    public int SmallClusterHammerCost;
    public int LargeClusterHammerCost;
    [Header("Axe")]
    public int SingleCrystalAxeCost;
    public int SmallClusterAxeCost;
    public int LargeClusterAxeCost;
    [Header("Gun")]
    public int SingleCrystalGunCost;
    public int SmallClusterGunCost;
    public int LargeClusterGunCost;

    public TextMeshProUGUI[] CostUI;

    public InventoryManager inventoryManager;

    public GameObject[] SlotsPrefabs;
    private GameObject[] Slots;

    
    

    public GameObject[] Buttons;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Slots = new GameObject[SlotsPrefabs.Length];
        UpdateCostText();
        weaponManager.WeaponSwitch();
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
    public void BuyHammer()
    {
        RefreshInstances();
        if (inventoryManager.Inventory_SingleCrystal_Slots.Count >= SingleCrystalHammerCost && inventoryManager.Inventory_SmallCrystalCluster_Slots.Count >= SmallClusterHammerCost && inventoryManager.Inventory_LargeCrystalCLuster_Slots.Count >= LargeClusterHammerCost)
        {
            for (int i = SingleCrystalHammerCost; i > 0; i--)
            {
                if (SingleCrystal != null && Slots[0] != null)
                {
                    InventoryManager.Instance.Remove(SingleCrystal);
                    InventoryManager.Instance.ListCrystals(SingleCrystal);
        
                }
            }
            for (int i = SmallClusterHammerCost; i > 0; i--)
            {
                if (SmallCluster != null && Slots[1] != null)
                {
                    InventoryManager.Instance.Remove(SmallCluster);
                    InventoryManager.Instance.ListCrystals(SmallCluster);
                }
            }
            for (int i = LargeClusterHammerCost; i > 0; i--)
            {
                if (LargeCluster != null && Slots[2] != null)
                {
                    InventoryManager.Instance.Remove(LargeCluster);
                    InventoryManager.Instance.ListCrystals(LargeCluster);
                }
            }
            Buttons[0].SetActive(true);
        }
        else
        {
            Debug.Log("Brakuje Kryszta³ów do zakupu");
        }
            
        
    }
    public void BuyAxe()
    {
        RefreshInstances();
        if (inventoryManager.Inventory_SingleCrystal_Slots.Count >= SingleCrystalAxeCost && inventoryManager.Inventory_SmallCrystalCluster_Slots.Count >= SmallClusterAxeCost && inventoryManager.Inventory_LargeCrystalCLuster_Slots.Count >= LargeClusterAxeCost)
        {
            for (int i = SingleCrystalAxeCost; i > 0; i--)
            {
                if (SingleCrystal != null && Slots[0] != null)
                {
                    InventoryManager.Instance.Remove(SingleCrystal);
                    InventoryManager.Instance.ListCrystals(SingleCrystal);

                }
            }
            for (int i = SmallClusterAxeCost; i > 0; i--)
            {
                if (SmallCluster != null && Slots[1] != null)
                {
                    InventoryManager.Instance.Remove(SmallCluster);
                    InventoryManager.Instance.ListCrystals(SmallCluster);
                }
            }
            for (int i = LargeClusterAxeCost; i > 0; i--)
            {
                if (LargeCluster != null && Slots[2] != null)
                {
                    InventoryManager.Instance.Remove(LargeCluster);
                    InventoryManager.Instance.ListCrystals(LargeCluster);
                }
            }
            Buttons[1].SetActive(true);
        }
        else
        {
            Debug.Log("Brakuje Kryszta³ów do zakupu");
        }
    }
    public void BuyGun()
    {
        RefreshInstances();
        if (inventoryManager.Inventory_SingleCrystal_Slots.Count >= SingleCrystalGunCost && inventoryManager.Inventory_SmallCrystalCluster_Slots.Count >= SmallClusterGunCost && inventoryManager.Inventory_LargeCrystalCLuster_Slots.Count >= LargeClusterGunCost)
        {
            for (int i = SingleCrystalGunCost; i > 0; i--)
            {
                if (SingleCrystal != null && Slots[0] != null)
                {
                    InventoryManager.Instance.Remove(SingleCrystal);
                    InventoryManager.Instance.ListCrystals(SingleCrystal);

                }
            }
            for (int i = SmallClusterGunCost; i > 0; i--)
            {
                if (SmallCluster != null && Slots[1] != null)
                {
                    InventoryManager.Instance.Remove(SmallCluster);
                    InventoryManager.Instance.ListCrystals(SmallCluster);
                }
            }
            for (int i = LargeClusterGunCost; i > 0; i--)
            {
                if (LargeCluster != null && Slots[2] != null)
                {
                    InventoryManager.Instance.Remove(LargeCluster);
                    InventoryManager.Instance.ListCrystals(LargeCluster);
                }
            }
            Buttons[2].SetActive(true);
        }
        else
        {
            Debug.Log("Brakuje Kryszta³ów do zakupu");
        }
    }
    public void EquipPickaxe()
    {
        weaponManager.WeaponIndex = 0;
        weaponManager.WeaponSwitch();
    }
    public void EquipHammer()
    {
        weaponManager.WeaponIndex = 1;
        weaponManager.WeaponSwitch();
    }
    public void EquipAxe()
    {
        weaponManager.WeaponIndex = 2;
        weaponManager.WeaponSwitch();
    }
    public void EquipGun()
    {
        weaponManager.WeaponIndex = 3;
        weaponManager.WeaponSwitch();
    }
    
    private void UpdateCostText()
    {
        CostUI[0].text = SingleCrystalHammerCost.ToString();
        CostUI[1].text = SmallClusterHammerCost.ToString();
        CostUI[2].text = LargeClusterHammerCost.ToString();
        CostUI[3].text = SingleCrystalAxeCost.ToString();
        CostUI[4].text = SmallClusterAxeCost.ToString();
        CostUI[5].text = LargeClusterAxeCost.ToString();
        CostUI[6].text = SingleCrystalGunCost.ToString();
        CostUI[7].text = SmallClusterGunCost.ToString();
        CostUI[8].text = LargeClusterGunCost.ToString();
    }
    
}


