using System.Collections;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Player_Info player_info;

    public float tileSize = 1f; // Rozmiar pojedynczego pola
    public int gridSizeX = 3; // Rozmiar siatki w osi X
    public int gridSizeZ = 3; // Rozmiar siatki w osi Z
    //public Vector2Int gridSize = new Vector2Int(3,3); // Rozmiar siatki (3x3)
    /*private float moveDelay;*/ // OpóŸnienie miêdzy ruchami
    public Transform Player; // Obiekt gracza
    public float detectionRadius = 3f; // Promieñ wykrywania gracza
    private int BeatsCollection;
    public int BeatsToMove;
    //private bool CanMove;
    public float ReachDistanceToAttack = 0.6f;

    private Vector3 gridCenter; // Sta³y œrodek siatki
    private Vector3 CurrentGridPosition; // Aktualna pozycja przeciwnika
    public bool isChasingPlayer; // Czy przeciwnik œciga gracza?
    private Vector3 lastPlayerPosition; // Ostatnia pozycja gracza
    /*private bool playerMoved = true;*/ // Czy gracz siê poruszy³?
    private Vector3 lastDirection; // Ostatni kierunek ruchu przeciwnika
    private Vector3 secondLastDirection; // Przedostatni kierunek ruchu przeciwnika



    private Animator anim;
    void Start()
    {
        // Ustaw aktualn¹ pozycjê przeciwnika jako pozycjê startow¹ siatki
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
            kó³ko_ataku_instance.instance.CircleActive(false);
        }
    }

    private void OnDestroy()
    {
        AudioManager.BeatUpdated -= UpdateMoveDelay;
        


    }
    private void Update()
    {

        if (isChasingPlayer)
        {


            if (IsAdjacentToPlayerWithRaycast() && !GameManager.instance.playerMoved)
            {



                kó³ko_ataku_instance.instance.CircleActive(true);
                Vector3 snappedDirection = SnapDirection(GameManager.instance.player.position - transform.position);
                Quaternion targetRotation = Quaternion.LookRotation(snappedDirection, Vector3.up);
                transform.rotation = targetRotation;

            }
            else if (GameManager.instance.playerMoved || !IsAdjacentToPlayerWithRaycast())
            {

                kó³ko_ataku_instance.instance.CircleActive(false);
            }
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

    }



    // Generowanie losowego kierunku z uwzglêdnieniem ostatnich ruchów
    private Vector3 GenerateRandomDirection()
    {
        Vector3[] directions = {
            new Vector3(0, 0, tileSize),  // Góra (Z+)
            new Vector3(0, 0, -tileSize), // Dó³ (Z-)
            new Vector3(tileSize, 0, 0),  // Prawo (X+)
            new Vector3(-tileSize, 0, 0) // Lewo (X-)
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

    // Sprawdzenie, czy w danym kierunku znajduje siê œciana
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

    //Ruch w kierunku gracza
    private void MoveTowardsPlayer()
    {
        
        Vector3 playerGridPosition = WorldToGrid(Player.position);
        Vector3 direction = playerGridPosition - CurrentGridPosition;

        // Normalizacja kierunku ruchu
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
        {
            direction = new Vector3(Mathf.Sign(direction.x), 0, 0);
        }
        else
        {
            direction = new Vector3(0, 0, Mathf.Sign(direction.z));
        }

        Vector3 newPosition = CurrentGridPosition + direction * tileSize;


        


        if (IsPositionValid(newPosition) && !IsWallBlocking(direction))
        {
            CurrentGridPosition = newPosition;
            transform.position = CurrentGridPosition;
            if(!IsAdjacentToPlayerWithRaycast())
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = targetRotation;
            }
            
        }


    }


    private bool IsAdjacentToPlayerWithRaycast()
    {
        Vector3 directionToPlayer = GameManager.instance.player.position - transform.position;

        // Sprawdzenie dominuj¹cego kierunku: X lub Z
        if (Mathf.Abs(directionToPlayer.x) > Mathf.Abs(directionToPlayer.z))
        {
            directionToPlayer = new Vector3(Mathf.Sign(directionToPlayer.x), 0, 0); // Ruch w osi X
        }
        else
        {
            directionToPlayer = new Vector3(0, 0, Mathf.Sign(directionToPlayer.z)); // Ruch w osi Z
        }

        // Raycast w kierunku gracza w wybranej osi
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

    // Konwersja pozycji œwiata na siatkê
    private Vector3 WorldToGrid(Vector3 worldPosition)
    {
        return new Vector3(
            Mathf.Round(worldPosition.x / tileSize) * tileSize,
            0,
            Mathf.Round(worldPosition.z / tileSize) * tileSize
        );
    }


    private void OnDrawGizmos()// Rysuje linie poruszania siê
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

        if(SceneManager.GetActiveScene().name != "Tutorial")
        {
            Player_Info.Instance.Player_HP -= 1;
            Player_Info.Instance.PlayerHpUpdate();

            Player_Info.Instance.CheckIfDead();
        }
        

        

    }
    private void UpdateMoveDelay()
    {

        BeatsCollection++;
        if (BeatsCollection == BeatsToMove)
        {
            if (isChasingPlayer)
            {
                // Pod¹¿anie za graczem
                if (GameManager.instance.playerMoved || !IsAdjacentToPlayerWithRaycast())
                {
                    anim.SetTrigger("EnemyWalk");
                    MoveTowardsPlayer();


                }
                else if (!GameManager.instance.playerMoved && IsAdjacentToPlayerWithRaycast())
                {
                    if(anim != null)
                    {
                        anim.SetTrigger("EnemyAttack");
                    }
                    
                    EnemyAttack();
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
        // Zaokr¹glanie kierunku do osi g³ównych (X lub Z)
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


        // Aktualizacja pozycji przeciwnika
        UpdateMovementHistory(chosenDirection);
        CurrentGridPosition = newPosition;
        transform.position = CurrentGridPosition;

        //Vector3 snappedDirection = SnapDirection(EnemyFront);
        Quaternion targetRotation = Quaternion.LookRotation(chosenDirection, Vector3.up);
        transform.rotation = targetRotation;
        // Losowy ruch
    }

}





