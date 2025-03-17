using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public AudioManager audioManager;
    [HideInInspector] public float moveTimer = 0f; // U¿ywamy float dla wiêkszej precyzji w czasie, im mniej tym trzeba byæ bardziej precyzyjnym
    public float timeWindow = 0.5f; // Okno czasowe (w sekundach) na wykonanie ruchu po beacie
    public int BlockDuration; // ustawianie iloœci blokad
   [HideInInspector] public int blockCounter = 0; // Licznik blokady (liczba beatów)
    private bool BlockIsActive = false;
    public static bool CanMove;
    //public GameObject CanMoveImage;
    [HideInInspector] public bool KeyboardActivity;

    private Rigidbody rb;
    public float raycastDistance;

    private bool canMoveLeft = true;
    private bool canMoveRight = true;
    private bool canMoveForward = true;
    private bool canMoveBackward = true;

    private Animator player_anim;
    
    void Start()
    {
        
        player_anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        AudioManager.BeatUpdated += OnBeat; // Subskrypcja na event BeatUpdated
    }

    void Update()
    {
        KeyboardActivity = true;
        
        
        CheckWallCollision();
        // Sprawdzaj czas pomiêdzy beatami
        if (blockCounter > 0)
        {
            // Jeœli gracz jest zablokowany, zmniejsz licznik na kolejnych beatach
            return;
        }
        if (moveTimer <= 0)

        {
            //CanMoveImage.SetActive(false);
            
        }

        if (moveTimer > 0)
        {
            moveTimer -= Time.deltaTime; // Zmniejsz timer o czas miniony od ostatniej klatki
            if (CanMove && !BlockIsActive)
            {
                
                
                CheckOnBeat(); // Pozwalaj na ruch tylko w oknie czasowym
                //CanMoveImage.SetActive(true);
            }
            
            
        }

        // Jeœli naciœniêto przycisk poza oknem czasowym, aktywuj blokadê
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) && !BlockIsActive && !CanMove)
        {
            blockCounter = BlockDuration; 
        }
    }

    private void OnDestroy()
    {
        AudioManager.BeatUpdated -= OnBeat; // Odsubskrybowanie eventu
    }

    // Funkcja sprawdzaj¹ca mo¿liwoœæ ruchu w oknie czasowym
    private void CheckOnBeat()
    {
        
        
        
            
        
        
        
        

        if (Input.GetKeyDown(KeyCode.A) && canMoveLeft && KeyboardActivity)
        {

            player_anim.SetTrigger("TriggerSwoosh");
        }
        else if (Input.GetKeyDown(KeyCode.D) && canMoveRight && KeyboardActivity)
        {
            player_anim.SetTrigger("TriggerSwoosh1");

        }
        else if (Input.GetKeyDown(KeyCode.W) && canMoveForward && KeyboardActivity)
        {
            player_anim.SetTrigger("TriggerSwoosh2");

        }
        else if (Input.GetKeyDown(KeyCode.S) && canMoveBackward && KeyboardActivity)
        {

            player_anim.SetTrigger("TriggerSwoosh3");






        }






    }

    private void CheckWallCollision()
    {
        canMoveLeft = true;
        canMoveRight = true;
        canMoveForward = true;
        canMoveBackward = true;

        if (Physics.Raycast(transform.position, Vector3.left, out RaycastHit hitLeft, raycastDistance))
        {
            if (!hitLeft.collider.CompareTag("Crystal") && !hitLeft.collider.CompareTag("Trap") && !hitLeft.collider.CompareTag("Floor"))
            {
                canMoveLeft = false;
            }
        }
        if (Physics.Raycast(transform.position, Vector3.right, out RaycastHit hitRight, raycastDistance))
        {
            if (!hitRight.collider.CompareTag("Crystal") && !hitRight.collider.CompareTag("Trap") && !hitRight.collider.CompareTag("Floor"))
            {
                canMoveRight = false;
            }
        }
        if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hitForward, raycastDistance) && !hitForward.collider.CompareTag("Floor"))
        {
            if (!hitForward.collider.CompareTag("Crystal") && !hitForward.collider.CompareTag("Trap"))
            {
                canMoveForward = false;
            }
        }
        if (Physics.Raycast(transform.position, Vector3.back, out RaycastHit hitBackward, raycastDistance) && !hitBackward.collider.CompareTag("Floor"))
        {
            if (!hitBackward.collider.CompareTag("Crystal") && !hitBackward.collider.CompareTag("Trap"))
            {
                canMoveBackward = false;
            }
        }
    }

    // Funkcja wywo³ywana na ka¿dym beacie
    private void OnBeat()
    {
        
        //if (blockCounter > 0 && CanMove)
        //{
        //    blockCounter--; // Zmniejsz licznik blokady
        //    BlockIsActive = true;
        //}
        if(blockCounter <= 0)
        {
            moveTimer = timeWindow; // Ustaw timer na nowe okno czasowe
            BlockIsActive = false;
        }
    }
    private void RuchWDó³()
    {
        Vector3 S = new Vector3(0, 0, -1); // Ruch do ty³u

        gameObject.transform.position += S;
        gameObject.transform.rotation = Quaternion.Euler(0, -180, 0);
        moveTimer = 0;
        BlockIsActive = true;
        KeyboardActivity = false;
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Swoosh, this.transform.position);
    }
    private void RuchWGóre()
    {
        Vector3 W = new Vector3(0, 0, 1);  // Ruch do przodu

        gameObject.transform.position += W;
        gameObject.transform.rotation = Quaternion.Euler(0, -360, 0);
        moveTimer = 0;
        BlockIsActive = true;
        KeyboardActivity = false;
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Swoosh, this.transform.position);
    }
    private void RuchWLewo()
    {
        Vector3 A = new Vector3(-1, 0, 0); // Ruch w lewo

        gameObject.transform.position += A;
        gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
        moveTimer = 0; // Resetuj timer po wykonaniu ruchu
        BlockIsActive = true;
        KeyboardActivity = false;
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Swoosh, this.transform.position);
    }
    private void RuchWPrawo()
    {
        Vector3 D = new Vector3(1, 0, 0);  // Ruch w prawo

        gameObject.transform.position += D;
        gameObject.transform.rotation = Quaternion.Euler(0, -270, 0);
        moveTimer = 0;
        BlockIsActive = true;
        KeyboardActivity = false;
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Swoosh, this.transform.position);
    }
}


