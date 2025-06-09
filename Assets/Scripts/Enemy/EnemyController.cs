using System.Collections;
//using Unity.Mathematics;
using Unity.VisualScripting;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor.Experimental.GraphView;

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

    private Vector3 LastEnemyChasingPosition;

    public Transform[] RangerPositionsToAttack;

    public Attack_Circle attackCircle;

    private Animator anim;
    private Enemy enemy;

    private static EnemyController currentCollidingEnemy = null;
    [HideInInspector]public bool IsColliding = false;
    private int BossBehaviourAfterAttack = 2;
    private int BossBehaviourIndex = 0;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
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
            Attack_Circle.instance.CircleBossActive(false);
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        AudioManager.BeatUpdated -= UpdateMoveDelay;

    }

    private void OnTriggerEnter(Collider other)
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");

        if (other.gameObject.layer == enemyLayer)
        {
            EnemyController enemy = other.GetComponent<EnemyController>();

            if (enemy != null && EnemyController.currentCollidingEnemy == null)
            {
                enemy.IsColliding = true;
                EnemyController.currentCollidingEnemy = enemy;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");

        if (other.gameObject.layer == enemyLayer)
        {
            EnemyController enemy = other.GetComponent<EnemyController>();

            if (enemy != null && enemy == EnemyController.currentCollidingEnemy)
            {
                enemy.IsColliding = false;
                EnemyController.currentCollidingEnemy = null;
            }
        }
    }


    private void FixedUpdate()
    {
        
        float distance = Vector3.Distance(gameObject.transform.position, Player_Info.instance.player.position);
        
        if (distance <= detectionRadius /*&& IsPositionValid(Player.position)*/)
        {
            isChasingPlayer = true;
            AgrroStatus.instance.SetAggroState(true);
        }


        if (isChasingPlayer && distance > detectionRadius /*&& !IsPositionValid(Player.position)*/)
        {
            isChasingPlayer = false;
            AgrroStatus.instance.SetAggroState(false);
        }



        if (!IsPositionValid(CurrentGridPosition))
        {
            gridCenter = transform.position;
        }

        

        if (isChasingPlayer)
        {
            if (distance < 0.1f || (currentCollidingEnemy != null && currentCollidingEnemy.IsColliding))
            {
                gameObject.transform.position = LastEnemyChasingPosition;
                CurrentGridPosition = LastEnemyChasingPosition;
                if (currentCollidingEnemy != null)
                {
                    currentCollidingEnemy.IsColliding = false;
                    //currentCollidingEnemy = null;
                }
                    

            }

            if (IsAdjacentToPlayerWithRaycast() && !Player_Info.instance.playerMoved && BossBehaviourIndex <= 0)
            {


                if (gameObject.CompareTag("Common"))
                {

                    attackCircle.NewCircleAnimationLength = attackCircle.CommonCircleLenght;
                    Attack_Circle.instance.CircleCommonActive(true);
                }
                else if (gameObject.CompareTag("Ranger"))
                {
                    attackCircle.NewCircleAnimationLength = attackCircle.RangerCircleLenght;
                    Attack_Circle.instance.CircleRangerActive(true);
                }
                else if (gameObject.CompareTag("Tank"))
                {
                    attackCircle.NewCircleAnimationLength = attackCircle.TankCircleLenght;
                    Attack_Circle.instance.CircleTankActive(true);
                }
                else if (gameObject.CompareTag("Boss"))
                {
                    attackCircle.NewCircleAnimationLength = attackCircle.BossCircleLenght;
                    Attack_Circle.instance.CircleBossActive(true);
                }







                if (attackCircle.End && !Player_Info.instance.playerMoved && IsAdjacentToPlayerWithRaycast() && BossBehaviourIndex <= 0)
                {
                    if (anim != null)
                    {

                        anim.SetTrigger("EnemyAttack");

                    }

                    EnemyAttack();
                    if(gameObject.CompareTag("Boss"))
                    {
                        BossBehaviourIndex = BossBehaviourAfterAttack;
                    }
                    attackCircle.End = false;

                }


                Vector3 snappedDirection = SnapDirection(Player_Info.instance.player.position - transform.position);
                Quaternion targetRotation = Quaternion.LookRotation(snappedDirection, Vector3.up);
                
                transform.DORotateQuaternion(targetRotation.normalized, .3f);
                BeatsCollection = 0;
            }
            else if (/*Player_Info.instance.playerMoved &&*/ !IsAdjacentToPlayerWithRaycast())
            {
                Attack_Circle.instance.CircleCommonActive(false);
                Attack_Circle.instance.CircleRangerActive(false);
                Attack_Circle.instance.CircleTankActive(false);
                Attack_Circle.instance.CircleBossActive(false);
                attackCircle.End = false;
                


            }
            
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
            if (hit.collider != null && hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Common") || hit.collider.CompareTag("Tank") || hit.collider.CompareTag("Ranger"))
            {
                return true;
            }
        }
        return false;
    }

    //Move in Player Direction
    private void MoveTowardsPlayer()
    {
        LastEnemyChasingPosition = gameObject.transform.position * tileSize;
        
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
        

        Vector3 BossNewPostion = CurrentGridPosition - direction * tileSize;


        
        

        if (gameObject.CompareTag("Common") || gameObject.CompareTag("Tank"))
        {
            if (!IsWallBlocking(direction))
            {



                CurrentGridPosition = newPosition;
                transform.DOMove(newPosition, .2f);


                if (!IsAdjacentToPlayerWithRaycast())
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                    
                    transform.DORotateQuaternion(targetRotation.normalized, .3f);
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
                while (Vector3.Distance(positionNew, Player_Info.instance.player.position) > detectionRadius
                 || IsWallBlocking(NewChasingPosition));

                CurrentGridPosition = positionNew;
                transform.DOMove(positionNew, .6f);



            }
            
        }
        else if(gameObject.CompareTag("Ranger"))
        {
            float distance = Vector3.Distance(transform.position, Player_Info.instance.player.position);

            
            
                bool hasLineOfSight = IsAdjacentToPlayerWithRaycast();

                // Ranger ma możliwość strzału – nie rusza się
                if (hasLineOfSight)
                    return;

                
                // Gracz jest za blisko – Ranger ucieka
                if (distance < 2.9f)
                {
                    Vector3 directionAway = (transform.position - Player_Info.instance.player.position).normalized;
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
                            float testDist = Vector3.Distance(testPos, Player_Info.instance.player.position);
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
                    
                    
                        Quaternion targetRotation = Quaternion.LookRotation(bestEscapeDir, Vector3.up);

                        transform.DORotateQuaternion(targetRotation.normalized, .3f);
                    
                    transform.DOMove(CurrentGridPosition, .6f);
                    }
                }
                else
                {
                    
                    Vector3 moveDir = GenerateDirectionToRanger();
                    Vector3 potentialPosition = CurrentGridPosition + moveDir * tileSize;

                    if (/*IsPositionValid(potentialPosition) &&*/ !IsWallBlocking(moveDir))
                    {
                        CurrentGridPosition = potentialPosition;
                    Quaternion targetRotation = Quaternion.LookRotation(potentialPosition, Vector3.up);

                    transform.DORotateQuaternion(targetRotation.normalized, .3f);
                    transform.DOMove(CurrentGridPosition, .6f);

                    }
                }


            
        }
        else if(gameObject.CompareTag("Boss"))
        {
            if (!IsWallBlocking(direction) && BossBehaviourIndex <= 0)
            {



                CurrentGridPosition = newPosition;
                transform.DOMove(CurrentGridPosition, .6f);


                if (!IsAdjacentToPlayerWithRaycast())
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                    transform.DORotateQuaternion(targetRotation.normalized, .3f);

                }

            }
            else if(BossBehaviourIndex <= 0)
            {



                Vector3 NewChasingPosition;
                Vector3 positionNew;

                do
                {
                    NewChasingPosition = GenerateRandomDirection();

                    positionNew = CurrentGridPosition + NewChasingPosition * tileSize;
                }
                while (Vector3.Distance(positionNew, Player_Info.instance.player.position) > detectionRadius
                 || IsWallBlocking(NewChasingPosition));

                CurrentGridPosition = positionNew;
                transform.DOMove(CurrentGridPosition, .6f);
                if (!IsAdjacentToPlayerWithRaycast())
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                    transform.DORotateQuaternion(targetRotation.normalized, .3f);

                }


            }
            else
            {
                BossTactic();
            }
         
        }
        
    }


    private bool IsAdjacentToPlayerWithRaycast()
    {
        Vector3 directionToPlayer = Player_Info.instance.player.position - transform.position;


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
            Player_Info.instance.Player_HP -= enemy.EnemyAttackDamage;
            Player_Info.instance.PlayerHpUpdate();

            Player_Info.instance.CheckIfDead();
        }




    }
    private void UpdateMoveDelay()
    {
        
        

        BeatsCollection++;
        if (BeatsCollection == BeatsToMove)
        {
            if (isChasingPlayer)
            {

                if (Player_Info.instance.playerMoved || !IsAdjacentToPlayerWithRaycast())
                {

                    anim.SetTrigger("EnemyWalk");
                    MoveTowardsPlayer();

                    
                }

                if(gameObject.CompareTag("Boss") && BossBehaviourIndex > 0 && IsAdjacentToPlayerWithRaycast())
                {
                    BossTactic();
                    
                }

            }
            else
            {
                anim.SetTrigger("EnemyWalk");
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
        //transform.position = CurrentGridPosition;
        transform.DOMove(CurrentGridPosition, .6f);
        //Vector3 snappedDirection = SnapDirection(EnemyFront);
        Quaternion targetRotation = Quaternion.LookRotation(chosenDirection, Vector3.up);
        transform.DORotateQuaternion(targetRotation.normalized, .3f);

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

    private void BossTactic()
    {

        if(BossBehaviourIndex > 0)
        {
            Vector3 moveDir = GenerateDirectionToRanger();
            Vector3 potentialPosition = CurrentGridPosition + moveDir * tileSize;

            if (/*IsPositionValid(potentialPosition) &&*/ !IsWallBlocking(moveDir))
            {
                CurrentGridPosition = potentialPosition;
                Quaternion targetRotation = Quaternion.LookRotation(potentialPosition, Vector3.up);

                transform.DORotateQuaternion(targetRotation.normalized, .3f);
                transform.DOMove(CurrentGridPosition, .6f);

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
                while (Vector3.Distance(positionNew, Player_Info.instance.player.position) > detectionRadius
                 || IsWallBlocking(NewChasingPosition));

                CurrentGridPosition = positionNew;
                transform.DOMove(CurrentGridPosition, .6f);
            }
            BossBehaviourIndex -= 1;
        }
            

        
    }
}





