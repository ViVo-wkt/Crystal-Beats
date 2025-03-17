using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    
     public int WeaponIndex = 0;
    public GameObject[] Weapons;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        WeaponSwitch();
    }

    
    public void WeaponSwitch()
    {
        foreach (GameObject weapon in Weapons)
        {
            weapon.SetActive(false);

        }
        switch (WeaponIndex)
        {
            case 0:
                {


                    Weapons[WeaponIndex].SetActive(true);

                    break;
                }
            case 1:
                {

                    Weapons[WeaponIndex].SetActive(true);
                    break;
                }
            case 2:
                {

                    Weapons[WeaponIndex].SetActive(true);
                    break;
                }
            case 3:
                {

                    Weapons[WeaponIndex].SetActive(true);
                    
                    break;
                }
            default:
                break;
        }

        
    }
    public void Save(ref WeaponSaveData data)
    {
        data.WeaponIndex = WeaponIndex;
    }
    public void Load(WeaponSaveData data)
    {

        WeaponIndex = data.WeaponIndex;
        WeaponSwitch();
    }
    [System.Serializable]
    public struct WeaponSaveData
    {
        public int WeaponIndex;
    }
}
