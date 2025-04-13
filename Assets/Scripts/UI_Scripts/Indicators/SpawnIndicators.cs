using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIndicators : MonoBehaviour
{

    public RectTransform[] Indicator; 
    public Transform Canvas;
    public RectTransform StartMovementPosition; 
    public RectTransform StartAttackPosition; 
    public Movement movement;
    public PlayerAttack playerAttack;

    private int IndicatorsInstances = 5;
    private List<RectTransform> PooledIndicatorsMovement = new List<RectTransform>();
    private List<RectTransform> PooledIndicatorsAttack = new List<RectTransform>();
    private void Awake()
    {
        
        for (int i = 0; i < IndicatorsInstances; i++)
        {
            RectTransform newIndicator = Instantiate(Indicator[0], Canvas);
            RectTransform AttackIndicator = Instantiate(Indicator[1], Canvas);

            newIndicator.gameObject.SetActive(false);
            AttackIndicator.gameObject.SetActive(false);

            PooledIndicatorsMovement.Add(newIndicator);
            PooledIndicatorsAttack.Add(AttackIndicator);
        }
    }
    void OnEnable()
    {


        AudioManager.BeatUpdated += Spawn;
    }

    
    private void OnDestroy()
    {
        AudioManager.BeatUpdated -= Spawn;
    }
    private void OnDisable()
    {
        AudioManager.BeatUpdated -= Spawn;
    }

    private void Spawn()
    {

        RectTransform newIndicator = GetPooledObjects(Indicator[0]);
        RectTransform AttackIndicator = GetPooledObjects(Indicator[1]);

        if (newIndicator != null)
        {
            newIndicator.anchoredPosition = StartMovementPosition.anchoredPosition;
            newIndicator.gameObject.SetActive(true);

        }
        if (AttackIndicator != null)
        {
            if (playerAttack.AttackbeatsCounter <= 0)
            {
                AttackIndicator.anchoredPosition = StartAttackPosition.anchoredPosition;
                AttackIndicator.gameObject.SetActive(true);
                playerAttack.AttackbeatsCounter = playerAttack.Attackbeats;
                
            }
        }
    }
    public RectTransform GetPooledObjects(RectTransform indicatorToGet)
    {
        if (indicatorToGet == Indicator[0])
        {


            for (int i = 0; i < PooledIndicatorsMovement.Count; i++)
            {
                if (!PooledIndicatorsMovement[i].gameObject.activeInHierarchy)
                {
                    return PooledIndicatorsMovement[i];
                }
            }
        }
        else
        {
            for (int i = 0; i < PooledIndicatorsAttack.Count; i++)
            {
                if (!PooledIndicatorsAttack[i].gameObject.activeInHierarchy)
                {
                    return PooledIndicatorsAttack[i];
                }
            }
        }
        return null;
    }
    
}
