using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Potion/Create New Potion")]
public class Potion : ScriptableObject
{

    public int id;
    public int heal;
    public Sprite PotionImage;
}
