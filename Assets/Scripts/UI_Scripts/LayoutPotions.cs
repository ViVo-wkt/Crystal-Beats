using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LayoutPotions : MonoBehaviour
{
    public static LayoutPotions Instance;
    public List<Potion> Smallpotions = new List<Potion>();
    public List<Potion> Mediumpotions = new List<Potion>();
    public List<Potion> Bigpotions = new List<Potion>();

    public GameObject[] SmallPotionsObjects;
    public GameObject[] MediumPotionsObjects;
    public GameObject[] BigPotionsObjects;


    public TextMeshProUGUI[] PotionCounter;

    private void Awake()
    {
        Instance = this;
    }
    public void ShowLayoutSmallPotions()
    {
        PotionCounter[0].text = Smallpotions.Count.ToString();

        if(Smallpotions.Count >= 1)
        {
            SmallPotionsObjects[0].SetActive(true);
        }
        

        if (SmallPotionsObjects[0].activeSelf && Smallpotions.Count != 1)
        {
            SmallPotionsObjects[1].SetActive(true);
        }
        
        
        
    }
    public void ShowLayoutMediumPotions()
    {
        PotionCounter[1].text = Mediumpotions.Count.ToString();

        if(Mediumpotions.Count >= 1)
        {
            MediumPotionsObjects[0].SetActive(true);
        }
        

        if (MediumPotionsObjects[0].activeSelf && Mediumpotions.Count != 1)
        {
            MediumPotionsObjects[1].SetActive(true);
        }
        

        
    }
    public void ShowLayoutBigPotions()
    {
        PotionCounter[2].text = Bigpotions.Count.ToString();

        if (Bigpotions.Count >= 1)
        {
            BigPotionsObjects[0].SetActive(true);
        }
        

        if (BigPotionsObjects[0].activeSelf && Bigpotions.Count != 1)
        {
            BigPotionsObjects[1].SetActive(true);
        }
        

        
    }
    public void AddPotion(Potion potion)
    {
        
        switch (potion.id)
        {
            case 0:
                {
                    Smallpotions.Add(potion);
                    break;
                }
            case 1:
                {
                    Mediumpotions.Add(potion);
                    break;
                }
            case 2:
                {
                    Bigpotions.Add(potion);
                    break;
                }
            default:
                break;
        }
    }
    public void RemovePotion(Potion potion)
    {
        switch (potion.id)
        {
            case 0:
                {
                    Smallpotions.Remove(potion);
                    break;
                }
            case 1:
                {
                    Mediumpotions.Remove(potion);
                    break;
                }
            case 2:
                {
                    Bigpotions.Remove(potion);
                    break;
                }
            default:
                break;
        }
    }
    
    
    public void Save(ref PotionSaveData data)
    {
        data.Smallpotions = Smallpotions;
        data.Mediumpotions = Mediumpotions;
        data.Bigpotions = Bigpotions;
    }
    public void Load(PotionSaveData data)
    {
        //Smallpotions = data.Smallpotions;
        //Mediumpotions = data.Mediumpotions;
        //Bigpotions = data.Bigpotions;
        Smallpotions = data.Smallpotions ?? new List<Potion>();
        Mediumpotions = data.Mediumpotions ?? new List<Potion>();
        Bigpotions = data.Bigpotions ?? new List<Potion>();

        ShowLayoutSmallPotions();
        ShowLayoutMediumPotions();
        ShowLayoutBigPotions();


    }


}

[System.Serializable]
public struct PotionSaveData
{
    public List<Potion> Smallpotions;
    public List<Potion> Mediumpotions;
    public List<Potion> Bigpotions;
}




