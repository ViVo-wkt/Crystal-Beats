using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CollisionIndicatorAttack : MonoBehaviour
{
    //public SpawnIndicators spawnIndicators;
    public float Speed;
    private Image image;
    private RectTransform rectTransform;
    private Rigidbody2D rb;

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
        
        if (Input.GetKeyDown(KeyCode.Space) && (!PlayerAttack.CanAttackUI))
        {
            rotation = Random.Range(50f, 750f);

            if(gameObject != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;

                rb.DORotate(rotation, 0.5f);
            }
            

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
            PlayerAttack.HasAttackedThisBeat = false;
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
        //if (spawnIndicators.playerAttack.blockCounter > 0 && rectTransform.localPosition.y == -400)
        //{
        //    spawnIndicators.playerAttack.blockCounter--;
        //}
        
    }
    private void ResetIndicator()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        gameObject.transform.rotation = Quaternion.identity;
        rectTransform.localScale = new Vector3(50, 50, 50);
    }
}
