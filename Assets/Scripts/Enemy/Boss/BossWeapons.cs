using UnityEngine;

public class BossWeapons : MonoBehaviour
{

    Weapon_Controller controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        


        foreach (Transform item in transform)
        {
            Weapon_Controller controller = item.GetComponent<Weapon_Controller>();

            
            if (controller != null && item.name == WeaponManager.Instance.weaponName)
            {
                item.gameObject.SetActive(true);
            }
            else
            {
                item.gameObject.SetActive(false);
            }
            if (WeaponManager.Instance.weaponName == "Gun" && item.name == "Pickaxe")

            {
                item.gameObject.SetActive(true);
            }
        }
    }

    
}
