using System.Collections;
//using Unity.Mathematics;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Player_Info player_info;

    public float tileSize = 1f; 
    public int gridSizeX = 3; // Grid size in X Axis
    public int gridSizeZ = 3; // Grid size in Z Axis
    
    public Transform Player; 
    public float detectionRadius = 3f; // Radius of Player detection
    private int BeatsCollection;
    public int BeatsToMove;
    //private bool CanMove;
    public float ReachDistanceToAttack = 0.6f;

    private Vector3 gridCenter; 
    private Vector3 CurrentGridPosition; //Actual Enemy Position
    public bool isChasingPlayer; 
    private Vector3 lastPlayerPosition; 
    
    private Vector3 lastDirection; 
    private Vector3 secondLastDirection;


    public Transform[] RangerPositionsToAttack;

    public Attack_Circle attackCircle;

    private Animator anim;
    void Start()
    {
        // Grid Center as a start enemy position
        gridCenter = transform.position;
        CurrentGridPosition = transform.position;
        //CurrentGridPosition = transform.position;

        //lastPlayerPosition = Player.position;
        lastDirection = Vector3.zero;
        secondLastDirection = Vector3.zero;

        anim = GetComponent<Animator>();
        AudioManager.BeatUpdated += UpdateMoveDelay;
        
    }
    private void OnDisable()
    {
        AudioManager.BeatUpdated -= UpdateMoveDelay;
        if (AgrroStatus.instance != null)
        {
            AgrroStatus.instance.SetAggroState(false);
        }

        if (GameManager.instance != null)
        {
            Attack_Circle.instance.CircleCommonActive(false);
            Attack_Circle.instance.CircleRangerActive(false);
            Attack_Circle.instance.CircleTankActive(false);
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        AudioManager.BeatUpdated -= UpdateMoveDelay;

        

    }
    private void Update()
    {
        if (!IsPositionValid(CurrentGridPosition))
        {
            gridCenter = transform.position;
        }
        if (isChasingPlayer)
        {


            if (IsAdjacentToPlayerWithRaycast() && !GameManager.instance.playerMoved)
            {


                if (gameObject.CompareTag("Common"))
                {
                    attackCircle.NewCircleAnimationLength = Attack_Circle.CommonCircleLenght;
                    Attack_Circle.instance.CircleCommonActive(true);
                }
                else if (gameObject.CompareTag("Ranger"))
                {
                    attackCircle.NewCircleAnimationLength = Attack_Circle.RangerCircleLenght;
                    Attack_Circle.instance.CircleRangerActive(true);
                }
                else if (gameObject.CompareTag("Tank"))
                {
                    attackCircle.NewCircleAnimationLength = Attack_Circle.TankCircleLenght;
                    Attack_Circle.instance.CircleTankActive(true);
                }

                Vector3 snappedDirection = SnapDirection(GameManager.instance.player.position - transform.position);
                Quaternion targetRotation = Quaternion.LookRotation(snappedDirection, Vector3.up);
                transform.rotation = targetRotation;

            }
            //else if (GameManager.instance.playerMoved || !IsAdjacentToPlayerWithRaycast())
            //{


            //}

        }

    }
    private void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, GameManager.instance.player.position);
        
        if (distanceToPlayer <= detectionRadius /*&& IsPositionValid(Player.position)*/)
        {
            isChasingPlayer = true;
            AgrroStatus.instance.SetAggroState(true);
        }


        if (isChasingPlayer && distanceToPlayer > detectionRadius /*&& !IsPositionValid(Player.position)*/)
        {
            isChasingPlayer = false;
            AgrroStatus.instance.SetAggroState(false);
        }

        if (distanceToPlayer > ReachDistanceToAttack)
        {
            Attack_Circle.instance.CircleCommonActive(false);
            Attack_Circle.instance.CircleRangerActive(false);
            Attack_Circle.instance.CircleTankActive(false);

        }
    }



    
    private Vector3 GenerateRandomDirection()
    {
        Vector3[] directions = {
            new Vector3(0, 0, tileSize),  // Up (Z+)
            new Vector3(0, 0, -tileSize), // Down (Z-)
            new Vector3(tileSize, 0, 0),  // Right (X+)
            new Vector3(-tileSize, 0, 0) // Left (X-)
        };

        Vector3 chosenDirection;
        do
        {
            chosenDirection = directions[Random.Range(0, directions.Length)];

        }
        while (chosenDirection == lastDirection && chosenDirection == secondLastDirection);

        return chosenDirection;
    }

    // Aktualizacja historii ruchów
    private void UpdateMovementHistory(Vector3 newDirection)
    {
        secondLastDirection = lastDirection;
        lastDirection = newDirection;
    }

    // Wall Checker
    private bool IsWallBlocking(Vector3 direction)
    {
        if (Physics.Raycast(transform.position, direction.normalized, out RaycastHit hit, tileSize))
        {
            if (hit.collider != null && hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Enemy"))
            {
                return true;
            }
        }
        return false;
    }

    //Move in Player Direction
    private void MoveTowardsPlayer()
    {

        Vector3 playerGridPosition = WorldToGrid(Player.position);
        Vector3 direction = playerGridPosition - CurrentGridPosition;

        //Move Normalization
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
        {
            direction = new Vector3(Mathf.Sign(direction.x), 0, 0);
        }
        else
        {
            direction = new Vector3(0, 0, Mathf.Sign(direction.z));
        }

        Vector3 newPosition = CurrentGridPosition + direction * tileSize;


        Vector3 RangerNewPostion = CurrentGridPosition - direction * tileSize;





        if (gameObject.CompareTag("Common") || gameObject.CompareTag("Tank"))
        {
            if (!IsWallBlocking(direction))
            {



                CurrentGridPosition = newPosition;
                transform.position = CurrentGridPosition;


                if (!IsAdjacentToPlayerWithRaycast())
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                    transform.rotation = targetRotation;
                }

            }
            else
            {



                Vector3 NewChasingPosition;
                Vector3 positionNew;

                do
                {
                    NewChasingPosition = GenerateRandomDirection();

                    positionNew = CurrentGridPosition + NewChasingPosition * tileSize;
                }
                while (Vector3.Distance(positionNew, GameManager.instance.player.position) > detectionRadius
                 || IsWallBlocking(NewChasingPosition));

                CurrentGridPosition = positionNew;
                transform.position = CurrentGridPosition;



            }
        }
        else
        {
            float distance = Vector3.Distance(transform.position, GameManager.instance.player.position);

            if (gameObject.CompareTag("Ranger"))
            {
                bool hasLineOfSight = IsAdjacentToPlayerWithRaycast();

                // Ranger ma możliwość strzału – nie rusza się
                if (hasLineOfSight)
                    return;

                // Gracz jest za blisko – Ranger ucieka
                if (distance < 2.9f)
                {
                    Vector3 directionAway = (transform.position - GameManager.instance.player.position).normalized;
                    Vector3 bestEscapeDir = Vector3.zero;
                    float maxDistance = 0f;

                    Vector3[] directions = {
                new Vector3(0, 0, tileSize),  // Up
                new Vector3(0, 0, -tileSize), // Down
                new Vector3(tileSize, 0, 0),  // Right
                new Vector3(-tileSize, 0, 0)  // Left
            };

                    foreach (var dir in directions)
                    {
                        Vector3 testPos = CurrentGridPosition + dir * tileSize;
                        if (/*IsPositionValid(testPos) &&*/ !IsWallBlocking(dir))
                        {
                            float testDist = Vector3.Distance(testPos, GameManager.instance.player.position);
                            if (testDist > maxDistance)
                            {
                                maxDistance = testDist;
                                bestEscapeDir = dir;
                            }
                        }
                    }

                    if (bestEscapeDir != Vector3.zero)
                    {
                        CurrentGridPosition += bestEscapeDir * tileSize;
                        transform.position = CurrentGridPosition;
                    }
                }
                else
                {
                    // Gracz jest w zasięgu, ale nie ma pozycji do strzału – próbuj się ustawić
                    Vector3 moveDir = GenerateDirectionToRanger();
                    Vector3 potentialPosition = CurrentGridPosition + moveDir * tileSize;

                    if (/*IsPositionValid(potentialPosition) &&*/ !IsWallBlocking(moveDir))
                    {
                        CurrentGridPosition = potentialPosition;
                        transform.position = CurrentGridPosition;
                    }
                }


            }
        }


    }


    private bool IsAdjacentToPlayerWithRaycast()
    {
        Vector3 directionToPlayer = GameManager.instance.player.position - transform.position;


        if (Mathf.Abs(directionToPlayer.x) > Mathf.Abs(directionToPlayer.z))
        {
            directionToPlayer = new Vector3(Mathf.Sign(directionToPlayer.x), 0, 0); // Ruch w osi X
        }
        else
        {
            directionToPlayer = new Vector3(0, 0, Mathf.Sign(directionToPlayer.z)); // Ruch w osi Z
        }
        
        
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, ReachDistanceToAttack))
        {

            if (hit.collider != null && hit.collider.transform == Player && !hit.collider.isTrigger)
            {

                
                return true;

            }
        }

        return false;
    }


    private bool IsPositionValid(Vector3 position)
    {
        // Sprawdzenie, czy pozycja jest w granicach siatki centrowanej na gridCenter
        return Mathf.Abs(position.x - gridCenter.x) <= (gridSizeX / 2) * tileSize &&
               Mathf.Abs(position.z - gridCenter.z) <= (gridSizeZ / 2) * tileSize;
    }

    // Konwersja pozycji œwiata na siatkę
    private Vector3 WorldToGrid(Vector3 worldPosition)
    {
        return new Vector3(
            Mathf.Round(worldPosition.x / tileSize) * tileSize,
            0,
            Mathf.Round(worldPosition.z / tileSize) * tileSize
        );
    }


    private void OnDrawGizmosSelected()
    {

        if (!Application.isPlaying)
        {
            Gizmos.color = Color.green;
            gridCenter = transform.position;
            // Rysowanie siatki centrowanej na gridCenter
            for (int x = -gridSizeX / 2; x <= gridSizeX / 2; x++)
            {
                for (int z = -gridSizeZ / 2; z <= gridSizeZ / 2; z++)
                {
                    Vector3 position = gridCenter + new Vector3(x * tileSize, 0, z * tileSize);
                    Gizmos.DrawWireCube(position, new Vector3(tileSize, 0, tileSize));
                }
            }
            return;
        }
        else
        {
            Gizmos.color = Color.green;

            for (int x = -gridSizeX / 2; x <= gridSizeX / 2; x++)
            {
                for (int z = -gridSizeZ / 2; z <= gridSizeZ / 2; z++)
                {
                    Vector3 position = gridCenter + new Vector3(x * tileSize, 0, z * tileSize);
                    Gizmos.DrawWireCube(position, new Vector3(tileSize, 0, tileSize));
                }
            }
        }
    }
    private void EnemyAttack()
    {

        if (SceneManager.GetActiveScene().name != "Tutorial")
        {
            Player_Info.Instance.Player_HP -= 1;
            Player_Info.Instance.PlayerHpUpdate();

            Player_Info.Instance.CheckIfDead();
        }




    }
    private void UpdateMoveDelay()
    {
        if (isChasingPlayer)
        {

            if (attackCircle.End && !GameManager.instance.playerMoved && IsAdjacentToPlayerWithRaycast())
            {
                if (anim != null)
                {
                    anim.SetTrigger("EnemyAttack");

                }

                EnemyAttack();
                attackCircle.End = false;
            }
            else if (GameManager.instance.playerMoved || !IsAdjacentToPlayerWithRaycast())
            {
                attackCircle.End = false;
            }
        }

        BeatsCollection++;
        if (BeatsCollection == BeatsToMove)
        {
            if (isChasingPlayer)
            {
                
                if (GameManager.instance.playerMoved || !IsAdjacentToPlayerWithRaycast())
                {
                    anim.SetTrigger("EnemyWalk");
                    MoveTowardsPlayer();


                }
                

            }
            else
            {
                //anim.SetTrigger("EnemyWalk");
                NewEnemyPosition();
            }
            BeatsCollection = 0;
        }
    }

    private Vector3 SnapDirection(Vector3 direction)
    {
        
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
        {
            return new Vector3(Mathf.Sign(direction.x), 0, 0); // Wektor w osi X
        }
        else
        {
            return new Vector3(0, 0, Mathf.Sign(direction.z)); // Wektor w osi Z
        }
    }
    private void NewEnemyPosition()
    {

        Vector3 newPosition;
        Vector3 chosenDirection;

        do
        {
            chosenDirection = GenerateRandomDirection();
            newPosition = CurrentGridPosition + chosenDirection * tileSize;
        }
        while (!IsPositionValid(newPosition) || IsWallBlocking(chosenDirection));


        // Position Update
        UpdateMovementHistory(chosenDirection);
        CurrentGridPosition = newPosition;
        transform.position = CurrentGridPosition;

        //Vector3 snappedDirection = SnapDirection(EnemyFront);
        Quaternion targetRotation = Quaternion.LookRotation(chosenDirection, Vector3.up);
        transform.rotation = targetRotation;

    }

    private Vector3 GenerateDirectionToRanger()
    {
        Vector3[] directions = {
        new Vector3(0, 0, tileSize),  // Up (Z+)
        new Vector3(0, 0, -tileSize), // Down (Z-)
        new Vector3(tileSize, 0, 0),  // Right (X+)
        new Vector3(-tileSize, 0, 0)  // Left (X-)
    };

        // 1. Znajdź najbliższą pozycję z tablicy RangerPositionsToAttack
        Transform bestTarget = null;
        float shortestTargetDistance = float.MaxValue;

        foreach (Transform t in RangerPositionsToAttack)
        {
            float dist = Vector3.Distance(CurrentGridPosition, t.position);
            if (dist < shortestTargetDistance)
            {
                shortestTargetDistance = dist;
                bestTarget = t;
            }
        }

        // 2. Jeśli nie ma żadnej pozycji, zwróć zero
        if (bestTarget == null)
            return Vector3.zero;

        // 3. Znajdź kierunek, który najbardziej przybliża do najlepszej pozycji
        Vector3 bestDirection = Vector3.zero;
        float shortestStepDistance = float.MaxValue;

        foreach (Vector3 dir in directions)
        {
            Vector3 newPos = CurrentGridPosition + dir;
            float dist = Vector3.Distance(newPos, bestTarget.position);
            if (dist < shortestStepDistance)
            {
                shortestStepDistance = dist;
                bestDirection = dir;
            }
        }

        return bestDirection;
    }

    
}





