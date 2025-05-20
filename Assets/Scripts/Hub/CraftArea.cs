using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public TextMeshProUGUI[] PotionCounter;
    public TextMeshProUGUI[] CrystalCounter;
    
    public GameObject[] PotionImages;
    public GameObject[] CrystalImages;
    public GameObject[] CrystalSlotsInCraft;
    public  Transform[] Canvas;

    public bool IsSingle;
    public bool IsSmall;
    public bool IsLarge;

    public PlayerInteracionHUB playerInteracionHUB;
    // Start is called before the first frame update
    private void Awake()
    {
        PotionImagesRestart();
    }
    void Start()
    {
        Slots = new GameObject[SlotsPrefabs.Length];
        
    }

    public void Update()
    {
        CrystalSlotsChecker();
        if (IsSmall && IsSingle && IsLarge)
        {
            PotionImages[2].SetActive(true);
        }
        else if (IsSmall && IsSingle)
        {
            PotionImages[1].SetActive(true);
        }
        else if (IsSingle)
        {
            PotionImages[0].SetActive(true);
        }

        if (playerInteracionHUB.CraftPanel.activeSelf)
        {
            for (int i = 1; i > 0; i--)
            {
                RefreshCounters();
            }
        }
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
                    RefreshCounters();
                    CrystalImages[0].transform.SetParent(Canvas[0].parent);
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
                    RefreshCounters();
                    CrystalImages[0].transform.SetParent(Canvas[0].parent);
                    CrystalImages[1].transform.SetParent(Canvas[1].parent);
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
                    RefreshCounters();
                    CrystalImages[0].transform.SetParent(Canvas[0].parent);
                    CrystalImages[1].transform.SetParent(Canvas[1].parent);
                    CrystalImages[2].transform.SetParent(Canvas[2].parent);
                    
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
    public void PotionImagesRestart()
    {
        foreach (GameObject item in PotionImages)
        {
            item.SetActive(false);
        }
    }
    public void CrystalSlotsChecker(/*ImageCrystal crystal*/)
    {
        IsSingle = false;
        IsSmall = false;
        IsLarge = false;

        PotionImagesRestart();
        foreach (GameObject item in CrystalSlotsInCraft)
        {
            foreach (Transform child in item.transform)
            {
                GameObject childObject = child.gameObject;

                
                if (childObject.CompareTag("SingleCrystal"))
                {
                    IsSingle = true;
                }


                if (childObject.CompareTag("SmallCluster"))
                {
                    IsSmall = true;
                }

                if (childObject.CompareTag("LargeCluster"))
                {
                    IsLarge = true;
                }
            }
        }






    }
    private void RefreshCounters()
    {
        PotionCounter[0].text = layoutPotions.Smallpotions.Count.ToString();
        PotionCounter[1].text = layoutPotions.Mediumpotions.Count.ToString();
        PotionCounter[2].text = layoutPotions.Bigpotions.Count.ToString();

        CrystalCounter[0].text = inventoryManager.Inventory_SingleCrystal_Slots.Count.ToString();
        CrystalCounter[1].text = inventoryManager.Inventory_SmallCrystalCluster_Slots.Count.ToString();
        CrystalCounter[2].text = inventoryManager.Inventory_LargeCrystalCLuster_Slots.Count.ToString();
        CrystalCounter[3].text = inventoryManager.Inventory_RedCrystalCluster_Slots.Count.ToString();

        CrystalImages[0].SetActive(true);
        CrystalImages[1].SetActive(true);
        CrystalImages[2].SetActive(true);
        CrystalImages[3].SetActive(true);


        if (inventoryManager.Inventory_SingleCrystal_Slots.Count <= 0)
        {
            
            CrystalImages[0].SetActive(false);

           
        }
        if (inventoryManager.Inventory_SmallCrystalCluster_Slots.Count <= 0)
        {
            CrystalImages[1].SetActive(false);
        }
        if (inventoryManager.Inventory_LargeCrystalCLuster_Slots.Count <= 0)
        {
            CrystalImages[2].SetActive(false);
        }
        if (inventoryManager.Inventory_RedCrystalCluster_Slots.Count <= 0)
        {
            CrystalImages[3].SetActive(false);
        }
    }
}
