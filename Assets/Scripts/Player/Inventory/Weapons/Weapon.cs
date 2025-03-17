using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon/Create New Weapon")]
public class Weapon : ScriptableObject
{
    public int Id;
    public int Damage;
    public GameObject GunObject;
}
