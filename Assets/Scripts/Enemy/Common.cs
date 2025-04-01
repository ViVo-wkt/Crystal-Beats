using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Common : MonoBehaviour
{
    public EnemyInfo enemyInfo;

    private int EnemyHP;
    private void Start()
    {
        //enemyInfo = Instantiate(enemyInfo);
        EnemyHP = enemyInfo.HP;
    }
    // Update is called once per frame
    void Update()
    {
        
        if(EnemyHP <= 0 && gameObject != null)
        {
            Destroy(gameObject);
            

        }
    }

    public void TakeDamage(int damage)
    {
        EnemyHP -= damage;
    }

    
    
}
