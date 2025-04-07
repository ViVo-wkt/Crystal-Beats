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
    void Awake()
    {
        // Ustawienie poczπtkowej pozycji wskaünika

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
        
        RectTransform newIndicator = Instantiate(Indicator[0], Canvas);
        newIndicator.anchoredPosition = StartMovementPosition.anchoredPosition;


        if (playerAttack.AttackbeatsCounter <= 0)
        {
            RectTransform AttackIndicator = Instantiate(Indicator[1], Canvas);
            AttackIndicator.anchoredPosition = StartAttackPosition.anchoredPosition;
            playerAttack.AttackbeatsCounter = playerAttack.Attackbeats;
        }
        
    }
}
