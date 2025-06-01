using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //public AudioManager audioManager;
    [HideInInspector] public float moveTimer = 0f; // Attack window
    public float timeWindow = 0.5f; // Attack window duration to Set in inspector

    public int BlockDuration; 
   [HideInInspector] public int blockCounter = 0; // Block counter (measure: number of beats)
    private bool BlockIsActive = false;

    public static bool CanMove; // UI Indicators
    
    [HideInInspector] public bool KeyboardActivity;
    public static bool HasMovedThisBeat;

    private int DirectoryIndex;
    private Rigidbody rb;
    public float raycastDistance;

    private bool canMoveLeft = true;
    private bool canMoveRight = true;
    private bool canMoveForward = true;
    private bool canMoveBackward = true;

    private Animator player_anim;

   public Vector3 LastPosition;
    void Start()
    {
        
        player_anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        /*AudioManager.BeatUpdated += OnBeat;*/ 
    }

    void Update()
    {
        KeyboardActivity = true;
        
        
        CheckWallCollision();
        
        if (blockCounter > 0)
        {
            
            return;
        }

        



        if (CanMove && !BlockIsActive)
        {

            if(!HasMovedThisBeat)
            {
                RotationCharacter();
                CheckOnBeat();
            }
            
            

        }

        // Jeœli naciœnięto przycisk poza oknem czasowym, aktywuj blokadę
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) && !BlockIsActive && !CanMove)
        {
            blockCounter = BlockDuration; 
        }
    }

    private void OnDestroy()
    {
        /*AudioManager.BeatUpdated -= OnBeat;*/ // Odsubskrybowanie eventu
    }
    private void OnDisable()
    {
        //AudioManager.BeatUpdated -= OnBeat;
    }

    
    private void CheckOnBeat()
    {
        LastPosition = gameObject.transform.position;
        if (Input.GetKeyDown(KeyCode.A) && canMoveLeft && KeyboardActivity && !HasMovedThisBeat)
        {
            TriggerAnimation();
            Move(Vector3.left);
            HasMovedThisBeat = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) && canMoveRight && KeyboardActivity && !HasMovedThisBeat)
        {
            TriggerAnimation();
            Move(Vector3.right);
            HasMovedThisBeat = true;
        }
        else if (Input.GetKeyDown(KeyCode.W) && canMoveForward && KeyboardActivity && !HasMovedThisBeat )
        {
            TriggerAnimation();
            Move(Vector3.forward);
            HasMovedThisBeat = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) && canMoveBackward && KeyboardActivity && !HasMovedThisBeat)
        {
            TriggerAnimation();
            Move(Vector3.back);
            HasMovedThisBeat = true;

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

    
    //private void OnBeat()
    //{
        
    //    //if (blockCounter > 0 && CanMove)
    //    //{
    //    //    blockCounter--; // Zmniejsz licznik blokady
    //    //    BlockIsActive = true;
    //    //}
    //    if(blockCounter <= 0)
    //    {
    //        moveTimer = timeWindow; // Ustaw timer na nowe okno czasowe
    //        BlockIsActive = false;
    //    }
    //}
    
    private void Move(Vector3 direction)
    {
        if(gameObject != null)
        {
            transform.DOMove(direction, .1f).SetRelative(true);
        }

        if(HeartHighLight.instance != null)
        {
            HeartHighLight.instance.BeatDrop();
        }
        
        //if(direction == Vector3.forward) {
        //    gameObject.transform.rotation = Quaternion.Euler(0, -360, 0);
        //} else if(direction == Vector3.back) {
        //    gameObject.transform.rotation = Quaternion.Euler(0, -180, 0);
        //}
        //else if (direction == Vector3.left)
        //{
        //    gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
        //}
        //else if (direction == Vector3.right)
        //{
        //    gameObject.transform.rotation = Quaternion.Euler(0, -270, 0);
        //}

        moveTimer = 0;
        //BlockIsActive = true;
        KeyboardActivity = false;
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Swoosh, this.transform.position);
    }
    private void RotationCharacter()
    {
        if (Input.GetKeyDown(KeyCode.A) && KeyboardActivity && !HasMovedThisBeat)
        {
            //gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
            transform.DORotateQuaternion(Quaternion.Euler(0, -90, 0), 0.1f);
        }
        else if (Input.GetKeyDown(KeyCode.D) && KeyboardActivity && !HasMovedThisBeat)
        {
            //gameObject.transform.rotation = Quaternion.Euler(0, -270, 0);
            transform.DORotateQuaternion(Quaternion.Euler(0, -270, 0), 0.1f);
        }
        else if (Input.GetKeyDown(KeyCode.W) && KeyboardActivity && !HasMovedThisBeat)
        {
            //gameObject.transform.rotation = Quaternion.Euler(0, -360, 0);
            transform.DORotateQuaternion(Quaternion.Euler(0, -360, 0), 0.1f);
        }
        else if (Input.GetKeyDown(KeyCode.S) && KeyboardActivity && !HasMovedThisBeat)
        {
            //gameObject.transform.rotation = Quaternion.Euler(0, -180, 0);
            transform.DORotateQuaternion(Quaternion.Euler(0, -180, 0), 0.1f);
        }
    }
    private void TriggerAnimation()
    {
        player_anim.SetTrigger("TriggerSwoosh");
    }
}


