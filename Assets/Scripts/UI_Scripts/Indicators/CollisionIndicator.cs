using DG.Tweening;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class CollisionIndicator : MonoBehaviour
{
    private Image image;
    private RectTransform rectTransform;
    //public SpawnIndicators spawnIndicators;
    public float Speed; // Prêdkoœæ przemieszczania w jednostkach UI
    private Rigidbody2D rb;
    public bool IsFalling;

    private float rotation;
    

    private Tween rotateTween;
    private bool isFalling;
    
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        rb = GetComponent<Rigidbody2D>();
        rectTransform = GetComponent<RectTransform>();
        //spawnIndicators = FindAnyObjectByType<SpawnIndicators>();
        
    }

    // Update is called once per frame
    void Update()
    {

        
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) && (!Movement.CanMove)/* && spawnIndicators.movement.moveTimer <= 0*/) 
        {
            //rotation = Random.Range(50f, 400f);
            //float distance = Vector2.Distance(gameObject.transform.position, Vector2.zero);
            
            //if (gameObject.activeSelf)
            //{
                
            //    rb.bodyType = RigidbodyType2D.Dynamic;

            //   rotateTween = rb.DORotate(rotation, 0.5f);
            //    isFalling = true;
                
            //}
            
            
        }
        else 
        {

            rectTransform.anchoredPosition += Vector2.right * Speed * Time.deltaTime;
        }
        

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SweetPot") && !isFalling)
        {
            Movement.CanMove = true;
            Movement.HasMovedThisBeat = false;
        }
        

        if (other.gameObject.CompareTag("FadeIn"))
        {
            image.CrossFadeAlpha(0f, 0.1f, false);
        }
        if (other.gameObject.CompareTag("End"))
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);

            

            ResetIndicator();
            
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SweetPot") && isFalling)
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
        //if (spawnIndicators.movement.blockCounter > 0 && rectTransform.localPosition.y == -400)
        //{
        //    spawnIndicators.movement.blockCounter--;
        //}
        
    }
    private void ResetIndicator()
    {
        isFalling = false;
        rotateTween?.Kill();

        rb.bodyType = RigidbodyType2D.Kinematic;
        
        gameObject.transform.rotation = Quaternion.identity;
        rectTransform.localScale = new Vector3(60, 60, 60);
        
    }

}
