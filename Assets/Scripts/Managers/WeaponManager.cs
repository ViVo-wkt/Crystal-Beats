using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Weapon_Controller;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    
     public int WeaponIndex = 0;
    public WeaponType weaponType;
    public string weaponName;
    public GameObject[] Weapons = new GameObject[4];
    void Awake()
    {
        
        
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        WeaponSwitch();
    }

    
    public void WeaponSwitch()
    {
        foreach (GameObject weapon in Weapons)
        {
            weapon.SetActive(false);
            
        }
        Weapons[(int)weaponType].SetActive(true);

        weaponName = weaponType.ToString();
        
    }
    public void Save(ref WeaponSaveData data)
    {
        data.WeaponIndex = weaponType;
    }
    public void Load(WeaponSaveData data)
    {

        weaponType = data.WeaponIndex;
        WeaponSwitch();
    }
    [System.Serializable]
    public struct WeaponSaveData
    {
        public WeaponType WeaponIndex;
    }
}
