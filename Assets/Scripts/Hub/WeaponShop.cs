using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Weapon_Controller;

public class WeaponShop : MonoBehaviour
{
    public static WeaponShop instance;

    public Crystal SingleCrystal;
    public Crystal SmallCluster;
    public Crystal LargeCluster;

    

    [Header("Hammer")]
    public int HammerCost;
    
    [Header("Axe")]
    public int AxeCost;
    
    [Header("Gun")]
    public int GunCost;

    private int Crystals_Sum;
    

    private int SingleCrystal_Value;
    private int SmallCluster_Value;
    private int LargeCluster_Value;

    private int QuantityOfSinglesCrystals;
    private int QuantityOfSmallsClusters;
    private int QuantityOfLargeClusters;
    [Header("Multiplication of Crystals")]
    public int SingleMultiply;
    public int SmallMultiply;
    public int LargeMultiply;

    private bool ActiveCase = false;

    public TextMeshProUGUI[] SlotsInfo;
    public TextMeshProUGUI[] CostUI;

    public InventoryManager inventoryManager;
    public PlayerInteracionHUB playerInteracionHUB;

    public GameObject[] SlotsPrefabs;
    private GameObject[] Slots;

    public GameObject[] ButtonsEquip;
    public GameObject[] ButtonsBuy;


    private enum ChoosenCrystalToPay
    {
        Single,
        Small,
        Large
    }
    ChoosenCrystalToPay choosenCrystalToPay;

    // Start is called before the first frame update
    void Start()
    {

        CrystalsMultiplication();
        
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        Slots = new GameObject[SlotsPrefabs.Length];
        
        WeaponManager.Instance.WeaponSwitch();
    }
    private void Update()
    {
        


        if(playerInteracionHUB.WeaponPanel.activeSelf)
        {
            for (int i = 1; i > 0; i--)
            {
                UpdateCostText();
            }
        }
    }
    //private void OnDisable()
    //{
    //    DontDestroyOnLoad(this);
    //}
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
        if (Crystals_Sum >= HammerCost)
        {
           
            for (int payment = HammerCost; payment > 0;)
                {

                //It turn on when one of Crystal group have more Crystals than others
                Case_1();

                //it turn on when minimum 2 group of Crystals have the same Quantity of Crystals and the case above do not turn on
                Case_2();

                // It turn on when rest of the payment for weapon is equal to value of Crystal
                Case_3(payment);

                payment = Payment(payment);

                ActiveCase = false;
                UpdateCostText();
                
            }




            ButtonsBuy[0].SetActive(false);
            ButtonsEquip[0].SetActive(true);
        }
        else
        {
            Debug.Log("Brakuje Kryszta³ów do zakupu");
        }
        

    }
    public void BuyAxe()
    {
        RefreshInstances();
        if (Crystals_Sum >= AxeCost)
        {
            for (int payment = AxeCost; payment > 0;)
            {

                //It turn on when one of Crystal group have more Crystals than others
                Case_1();

                //it turn on when minimum 2 group of Crystals have the same Quantity of Crystals and the case above do not turn on starting from the smallest one
                Case_2();

                // It turn on when rest of the payment for weapon is equal to value of Crystal, starting from the biggest one
                Case_3(payment);

                payment = Payment(payment);

                ActiveCase = false;

                UpdateCostText();
                Debug.Log(payment);
            }
            ButtonsBuy[1].SetActive(false);
            ButtonsEquip[1].SetActive(true);
        }
        else
        {
            Debug.Log("Brakuje Kryszta³ów do zakupu");
        }
        
    }
    public void BuyGun()
    {
        RefreshInstances();
        if (Crystals_Sum >= GunCost)
        {
            for (int payment = GunCost; payment > 0;)
            {

                //It turn on when one of Crystal group have more Crystals than others
                Case_1();

                //it turn on when minimum 2 group of Crystals have the same Quantity of Crystals and the case above do not turn on
                Case_2();

                // It turn on when rest of the payment for weapon is equal to value of Crystal
                Case_3(payment);

                payment = Payment(payment);

                ActiveCase = false;
                UpdateCostText();
            }
            ButtonsBuy[2].SetActive(false);
            ButtonsEquip[2].SetActive(true);
        }
        else
        {
            Debug.Log("Brakuje Kryszta³ów do zakupu");
        }
        
    }
    public void EquipPickaxe()
    {
        WeaponManager.Instance.weaponType = WeaponType.Pickaxe;

        ColorButtonSwitch();
        WeaponManager.Instance.WeaponSwitch();
    }
    public void EquipHammer()
    {
        WeaponManager.Instance.weaponType = WeaponType.Hammer;
        ColorButtonSwitch();
        WeaponManager.Instance.WeaponSwitch();
    }
    public void EquipAxe()
    {
        WeaponManager.Instance.weaponType = WeaponType.Axe;
        ColorButtonSwitch();
        WeaponManager.Instance.WeaponSwitch();
    }
    public void EquipGun()
    {
        WeaponManager.Instance.weaponType = WeaponType.Gun;
        ColorButtonSwitch();
        WeaponManager.Instance.WeaponSwitch();
    }
    private void ColorButtonSwitch()
    {
        foreach (var button in ButtonsEquip)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
        switch ((int)WeaponManager.Instance.weaponType)

        {
            case 0:
                ButtonsEquip[3].GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow;
                break;
            case 1:
                ButtonsEquip[0].GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow;
                break;
            case 2:
                ButtonsEquip[1].GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow;
                break;
            case 3:
                ButtonsEquip[2].GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow;
                break;
            default:
                break;
        }
    }
    private void UpdateCostText()
    {
        QuantityOfSinglesCrystals = inventoryManager.Inventory_SingleCrystal_Slots.Count;
        QuantityOfSmallsClusters = inventoryManager.Inventory_SmallCrystalCluster_Slots.Count;
        QuantityOfLargeClusters = inventoryManager.Inventory_LargeCrystalCLuster_Slots.Count;


        CostUI[0].text = HammerCost.ToString();
        CostUI[1].text = AxeCost.ToString();
        CostUI[2].text = GunCost.ToString();

        Crystals_Sum = QuantityOfSinglesCrystals * SingleCrystal_Value + QuantityOfSmallsClusters * SmallCluster_Value + QuantityOfLargeClusters * LargeCluster_Value;

            foreach (var item in SlotsInfo)
            {
                SlotsInfo[0].text = inventoryManager.Inventory_SingleCrystal_Slots.Count.ToString();
                SlotsInfo[1].text = inventoryManager.Inventory_SmallCrystalCluster_Slots.Count.ToString();
                SlotsInfo[2].text = inventoryManager.Inventory_LargeCrystalCLuster_Slots.Count.ToString();
                SlotsInfo[3].text = Crystals_Sum.ToString();

                break;
            }

        
    }
    
    private void Case_1()
    {
        if (inventoryManager.Inventory_SingleCrystal_Slots.Count > inventoryManager.Inventory_SmallCrystalCluster_Slots.Count && inventoryManager.Inventory_SingleCrystal_Slots.Count > inventoryManager.Inventory_LargeCrystalCLuster_Slots.Count)
        {
            
            
                choosenCrystalToPay = ChoosenCrystalToPay.Single;
                ActiveCase = true;
            
            
        }
        else if (inventoryManager.Inventory_SmallCrystalCluster_Slots.Count > inventoryManager.Inventory_SingleCrystal_Slots.Count && inventoryManager.Inventory_SmallCrystalCluster_Slots.Count > inventoryManager.Inventory_LargeCrystalCLuster_Slots.Count)
        {
            
            
                choosenCrystalToPay = ChoosenCrystalToPay.Small;
                ActiveCase = true;
            
            
        }
        else if (inventoryManager.Inventory_LargeCrystalCLuster_Slots.Count > inventoryManager.Inventory_SingleCrystal_Slots.Count && inventoryManager.Inventory_LargeCrystalCLuster_Slots.Count > inventoryManager.Inventory_SmallCrystalCluster_Slots.Count)
        {
            
            
                choosenCrystalToPay = ChoosenCrystalToPay.Large;
                ActiveCase = true;
            
            
        }
    }
    private void Case_2()
    {
        if (ActiveCase) return;

        int singles = inventoryManager.Inventory_SingleCrystal_Slots.Count;
        int smalls = inventoryManager.Inventory_SmallCrystalCluster_Slots.Count;
        int larges = inventoryManager.Inventory_LargeCrystalCLuster_Slots.Count;

        // Find pairs with equal count
        if (singles == smalls)
        {
            choosenCrystalToPay = ChoosenCrystalToPay.Single;
        }
        else if (singles == larges)
        {
            choosenCrystalToPay = ChoosenCrystalToPay.Single;
        }
        else if (smalls == larges)
        {
            choosenCrystalToPay = ChoosenCrystalToPay.Small;
        }
    }
    private void Case_3(int restOfPayment)
    {
        
        if (restOfPayment <= LargeCluster_Value && QuantityOfLargeClusters > 0)
        {
            choosenCrystalToPay = ChoosenCrystalToPay.Large;
        }
        
        else if (restOfPayment <= SmallCluster_Value && QuantityOfSmallsClusters > 0)
        {
            choosenCrystalToPay = ChoosenCrystalToPay.Small;
        }
        else if (restOfPayment <= SingleCrystal_Value && QuantityOfSinglesCrystals > 0)
        {
            choosenCrystalToPay = ChoosenCrystalToPay.Single;
        }
    }
    private int Payment(int payment)
    {
        switch (choosenCrystalToPay)
        {
            case ChoosenCrystalToPay.Single:
                if (SingleCrystal != null && Slots[0] != null)
                {
                    InventoryManager.Instance.Remove(SingleCrystal);
                    InventoryManager.Instance.ListCrystals(SingleCrystal);
                }

                payment -= SingleCrystal_Value;
                break;
            case ChoosenCrystalToPay.Small:
                if (SmallCluster != null && Slots[1] != null)
                {
                    InventoryManager.Instance.Remove(SmallCluster);
                    InventoryManager.Instance.ListCrystals(SmallCluster);
                }
                payment -= SmallCluster_Value;
                break;
            case ChoosenCrystalToPay.Large:
                if (LargeCluster != null && Slots[2] != null)
                {
                    InventoryManager.Instance.Remove(LargeCluster);
                    InventoryManager.Instance.ListCrystals(LargeCluster);
                }
                payment -= LargeCluster_Value;
                break;

            default:
                Debug.Log("Error");
                payment = 0;
                break;

        }
        return payment;
    }
    private void CrystalsMultiplication()
    {
        if (SingleMultiply != 0)
        {
            SingleCrystal_Value = 1 * SingleMultiply;
        }
        else
        {
            SingleCrystal_Value = 1;
        }

        if (SmallMultiply != 0)
        {
            SmallCluster_Value = 1 * SmallMultiply;
        }
        else
        {
            SmallCluster_Value = 1;
        }

        if (LargeMultiply != 0)
        {
            LargeCluster_Value = 1 * LargeMultiply;
        }
        else
        {
            LargeCluster_Value = 1;
        }
    }

    public void Save(ref WeaponShop_data data)
    {
        data.ButtonsEquipStates = new bool[ButtonsEquip.Length];
        data.ButtonsBuyStates = new bool[ButtonsBuy.Length];

        for (int i = 0; i < ButtonsEquip.Length; i++)
            data.ButtonsEquipStates[i] = ButtonsEquip[i].activeSelf;

        for (int i = 0; i < ButtonsBuy.Length; i++)
            data.ButtonsBuyStates[i] = ButtonsBuy[i].activeSelf;
    }


    public void Load(WeaponShop_data data)
    {
        if (data.ButtonsEquipStates != null && ButtonsEquip != null)
        {
            for (int i = 0; i < Mathf.Min(ButtonsEquip.Length, data.ButtonsEquipStates.Length); i++)
            {
                if (ButtonsEquip[i] != null)
                    ButtonsEquip[i].SetActive(data.ButtonsEquipStates[i]);
            }
        }

        if (data.ButtonsBuyStates != null && ButtonsBuy != null)
        {
            for (int i = 0; i < Mathf.Min(ButtonsBuy.Length, data.ButtonsBuyStates.Length); i++)
            {
                if (ButtonsBuy[i] != null)
                    ButtonsBuy[i].SetActive(data.ButtonsBuyStates[i]);
            }
        }
        ColorButtonSwitch();
    }


}
[System.Serializable]
public struct WeaponShop_data
{
    public bool[] ButtonsEquipStates; 
    public bool[] ButtonsBuyStates;
}


