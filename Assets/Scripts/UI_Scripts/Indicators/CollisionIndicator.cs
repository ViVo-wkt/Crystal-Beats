using DG.Tweening;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
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
    
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        rb = GetComponent<Rigidbody2D>();
        rectTransform = GetComponent<RectTransform>();
        //spawnIndicators = FindAnyObjectByType<SpawnIndicators>();
        //Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) && (!Movement.CanMove)/* && spawnIndicators.movement.moveTimer <= 0*/) 
        {
            rotation = Random.Range(50f, 750f);
            if(gameObject != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;

                rb.DORotate(rotation, 0.5f);
                rb.mass = 1000f;
            }
            
            
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
        //if (spawnIndicators.movement.blockCounter > 0 && rectTransform.localPosition.y == -400)
        //{
        //    spawnIndicators.movement.blockCounter--;
        //}
        
    }
    private void ResetIndicator()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        gameObject.transform.rotation = Quaternion.identity;
        rectTransform.localScale = new Vector3(60, 60, 60);
        rb.mass = 1f;
    }

}
