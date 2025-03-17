using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public EnemyInfo enemyInfo;


    private void Start()
    {
        enemyInfo = Instantiate(enemyInfo);
    }
    // Update is called once per frame
    void Update()
    {
        if (enemyInfo.HP <= 0 && gameObject != null)
        {
            gameObject.SetActive(false);
            
            MeshRenderer.Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        enemyInfo.HP = 4;
    }
    public void TakeDamage(int damage)
    {
        enemyInfo.HP -= damage;
    }
}
