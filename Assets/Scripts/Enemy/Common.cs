using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Common : MonoBehaviour
{
    public EnemyInfo enemyInfo;


    private void Start()
    {
        enemyInfo = Instantiate(enemyInfo);
    }
    // Update is called once per frame
    void Update()
    {
        
        if(enemyInfo.HP <= 0 && gameObject != null)
        {
            Destroy(gameObject);
            MeshRenderer.Destroy(gameObject);

        }
    }
    private void OnDestroy()
    {
         enemyInfo.HP = 2;
    }
    public void TakeDamage(int damage)
    {
        enemyInfo.HP -= damage;
    }

    
    
}
