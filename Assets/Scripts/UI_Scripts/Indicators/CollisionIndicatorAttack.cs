using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CollisionIndicatorAttack : MonoBehaviour
{
    public SpawnIndicators spawnIndicators;
    public float Speed = 500f;
    private Image image;
    private RectTransform rectTransform;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        rb = GetComponent<Rigidbody2D>();
        rectTransform = GetComponent<RectTransform>();
        spawnIndicators = FindAnyObjectByType<SpawnIndicators>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && (!PlayerAttack.CanAttackUI) && spawnIndicators.playerAttack.AttackTimer <= 0)
        {


            
            rb.bodyType = RigidbodyType2D.Dynamic;// nic siê nie dzieje
            


        }
        else
        {
            rectTransform.anchoredPosition += Vector2.left * Speed * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SweetPotAttack") && rectTransform.localPosition.y == -400)
        {

            PlayerAttack.CanAttackUI = true;
        }


        if (other.gameObject.CompareTag("FadeIn"))
        {
            image.CrossFadeAlpha(0f, 0.1f, false);
        }
        if (other.gameObject.CompareTag("End"))
        {
            Destroy(gameObject);
            Texture.Destroy(image);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SweetPotAttack") && rectTransform.localPosition.y == -400)
        {

            PlayerAttack.CanAttackUI = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SweetPotAttack"))
        {
            
            PlayerAttack.CanAttackUI = false;
        }
        if (spawnIndicators.playerAttack.blockCounter > 0 && rectTransform.localPosition.y == -400)
        {
            spawnIndicators.playerAttack.blockCounter--;
        }
        
    }
    
}
