using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player_Info : MonoBehaviour
{
    public static Player_Info Instance;

    public Potion smallPotion;
    public Potion mediumPotion;
    public Potion bigPotion;
    
    public int Player_HP = 4;
    public GameObject[] Heart_Pieces;
    public LayoutPotions layoutPotions;

    private GameObject Player;
    
    public Inventory inventory;
    private void Awake()
    {
        
        Instance = this;
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerHpUpdate();
    }

    private void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Alpha1) && layoutPotions.Smallpotions.Count > 0 && Player_HP < 4)
        {
            UsesmallPotion();
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Healing, this.transform.position);
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && layoutPotions.Mediumpotions.Count > 0 && Player_HP < 4)
        {
            UsemediumPotion();
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Healing, this.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && layoutPotions.Bigpotions.Count > 0 && Player_HP < 4)
        {

            UsebigPotion();
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Healing, this.transform.position);
        }
        
        
        
    }
    public void PlayerHpUpdate()
    {
       
        



            for (int i = 0; i < Heart_Pieces.Length; i++)
            {
                Heart_Pieces[i].SetActive(i < Player_HP); // Aktywuj serca tylko dla zdrowia wiêkszego ni¿ i

            }
        
        
    }
    private void UsesmallPotion()
    {
        if (layoutPotions.Smallpotions.Count < 3)
        {
            if (layoutPotions.SmallPotionsObjects[1].activeSelf)
            {
                layoutPotions.SmallPotionsObjects[1].SetActive(false);
            }
            else if (!layoutPotions.SmallPotionsObjects[1].activeSelf && layoutPotions.SmallPotionsObjects[0].activeSelf)
            {
                layoutPotions.SmallPotionsObjects[0].SetActive(false);
            }
        }
        LayoutPotions.Instance.RemovePotion(smallPotion);
        layoutPotions.PotionCounter[0].text = layoutPotions.Smallpotions.Count.ToString();
        Player_HP += smallPotion.heal;

        PlayerHpUpdate();
    }
    private void UsemediumPotion()
    {
        if (layoutPotions.Mediumpotions.Count < 3)
        {
            if (layoutPotions.MediumPotionsObjects[1].activeSelf)
            {
                layoutPotions.MediumPotionsObjects[1].SetActive(false);
            }
            else if (!layoutPotions.MediumPotionsObjects[1].activeSelf && layoutPotions.MediumPotionsObjects[0].activeSelf)
            {
                layoutPotions.MediumPotionsObjects[0].SetActive(false);
            }

        }
        LayoutPotions.Instance.RemovePotion(mediumPotion);
        layoutPotions.PotionCounter[1].text = layoutPotions.Mediumpotions.Count.ToString();
        Player_HP += mediumPotion.heal;
        if (Player_HP > 4)
        {
            Player_HP = 4;
        }
        PlayerHpUpdate();
    }
    private void UsebigPotion()
    {
        if (layoutPotions.Bigpotions.Count < 3)
        {
            if (layoutPotions.BigPotionsObjects[1].activeSelf)
            {
                layoutPotions.BigPotionsObjects[1].SetActive(false);
            }
            else if (!layoutPotions.BigPotionsObjects[1].activeSelf && layoutPotions.BigPotionsObjects[0].activeSelf)
            {
                layoutPotions.BigPotionsObjects[0].SetActive(false);

            }

        }
        LayoutPotions.Instance.RemovePotion(bigPotion);
        layoutPotions.PotionCounter[2].text = layoutPotions.Bigpotions.Count.ToString();
        Player_HP += bigPotion.heal;
        if (Player_HP > 4)
        {
            Player_HP = 4;
        }
        PlayerHpUpdate();
    }
    public void CheckIfDead()
    {
        if (Player_HP <= 0)
        {
            inventory.DeathPanel.SetActive(true);
            Player.SetActive(false);
            
            
        }
    }
    public void Save(ref PlayerHpData data)
    {
        data.HP = Player_HP;
    }
    public void Load(PlayerHpData data)
    {

        Player_HP = data.HP;
        PlayerHpUpdate();

    }
    
}

[System.Serializable]
public struct PlayerHpData
{
    public int HP;
}


