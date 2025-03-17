using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionIndicator : MonoBehaviour
{
    private Image image;
    private RectTransform rectTransform;
    public SpawnIndicators spawnIndicators;
    public float Speed = 500f; // Prêdkoœæ przemieszczania w jednostkach UI
    private Rigidbody2D rb;
    public bool IsFalling;
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
        
        
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) && (!Movement.CanMove) && spawnIndicators.movement.moveTimer <= 0) 
        {
            
            
                rb.bodyType = RigidbodyType2D.Dynamic;
            
        }
        else 
        {
            
            rectTransform.anchoredPosition += Vector2.right * Speed * Time.deltaTime;
        }
        

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SweetPot") && rectTransform.localPosition.y == -400)
        {
            Movement.CanMove = true;
            
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
        if (other.gameObject.CompareTag("SweetPot") && rectTransform.localPosition.y == -400)
        {
            Movement.CanMove = true;
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SweetPot"))
        {
             Movement.CanMove = false;
           
        }
        if (spawnIndicators.movement.blockCounter > 0 && rectTransform.localPosition.y == -400)
        {
            spawnIndicators.movement.blockCounter--;
        }
        
    }
    

}
