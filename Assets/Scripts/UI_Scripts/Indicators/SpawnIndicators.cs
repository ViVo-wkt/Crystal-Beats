using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIndicators : MonoBehaviour
{

    public RectTransform[] Indicator; // Zamiast GameObject u�ywamy RectTransform
    public Transform Canvas;
    public RectTransform StartMovementPosition; // Punkt pocz�tkowy r�wnie� jako RectTransform
    public RectTransform StartAttackPosition; // Punkt pocz�tkowy r�wnie� jako RectTransform
    public Movement movement;
    public PlayerAttack playerAttack;
    void Awake()
    {
        // Ustawienie pocz�tkowej pozycji wska�nika

        AudioManager.BeatUpdated += Spawn;
    }

    
    private void OnDestroy()
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
