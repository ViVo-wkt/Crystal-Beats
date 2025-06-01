using Cinemachine;
using FMODUnity;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{

    private RectTransform Movement;
    private RectTransform Attack;
    public RectTransform[] SpawnIndicatorsPoints = new RectTransform[2] ;

    public GameObject Boss;
    private EnemyController BossScript;

    public GameObject[] Gate;
    private void Start()
    {
        BossScript = Boss.GetComponent<EnemyController>();

        Movement = SpawnIndicatorsPoints[0];
        Attack = SpawnIndicatorsPoints[1];
        
        HubMusic();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(Boss != null)
        {
            GateClose();
            BossMusic();
        }
        
    }
    private void BossMusic()
    {
        Movement.anchoredPosition = new Vector2(-1801, Movement.anchoredPosition.y);
        Attack.anchoredPosition = new Vector2(1801, Attack.anchoredPosition.y);


        RuntimeManager.StudioSystem.setParameterByName("Beat", 1);
        BossScript.enabled = true;
    }
    private void HubMusic()
    {
        Movement.anchoredPosition = new Vector2(-1900, Movement.anchoredPosition.y);
        Attack.anchoredPosition = new Vector2(1900, Attack.anchoredPosition.y);

        RuntimeManager.StudioSystem.setParameterByName("Beat", 0);
        
        
    }
    private void GateClose()
    {
        foreach (GameObject block in Gate)
        {
            block.SetActive(true);
        }
    }
}
