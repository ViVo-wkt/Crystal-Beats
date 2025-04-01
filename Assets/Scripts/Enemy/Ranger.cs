using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : MonoBehaviour
{
    public EnemyInfo enemyInfo;

    private int EnemyHP;
    private void Start()
    {
        EnemyHP = enemyInfo.HP;
    }
    // Update is called once per frame
    void Update()
    {
        if (EnemyHP <= 0 && gameObject != null)
        {
            Destroy(gameObject);
            
        }
    }
    
    public void TakeDamage(int damage)
    {
        EnemyHP -= damage;
    }
}
