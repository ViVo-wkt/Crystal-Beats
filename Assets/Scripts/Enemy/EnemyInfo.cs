using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[CreateAssetMenu (fileName = "New Enemy", menuName = "Enemy/Create New Enemy")]
public class EnemyInfo : ScriptableObject
{
    public int id;
    public string enemyName;
    public int HP;
    public Enemy_type enemy_Type;


    public enum Enemy_type
    {
        common,
        ranger,
        tank
    }
}
