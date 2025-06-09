using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    private Animator chest_Anim;
    private void Start()
    {
        chest_Anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            chest_Anim.SetTrigger("Treasure");
        }
        
    }
}
