using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BaseGrid : MonoBehaviour
    {
    public GameObject cellPrefab;
    public GameObject cakePrefab;
    public GameObject giftBoxPrefab;
    public GameObject obstaclePrefab;

    public Image winUI; 
    public Image loseUI;
    public GameObject overlay;
    

    protected int rows ;
    protected int columns;
    protected float cellSpacing ;
    protected float moveSpeed;
    protected float winDelay; 

    protected Transform[,] gridCells;
    protected GameObject[,] gridObjects;
    protected GameObject cake, giftBox;
    protected List<GameObject> obstacles = new List<GameObject>();

    protected Vector2Int cakePosition;
    protected Vector2Int giftBoxPosition;
    protected List<Vector2Int> obstaclePositions = new List<Vector2Int>();

    protected Vector3 cakeTargetPosition;
    protected Vector3 giftBoxTargetPosition;
    protected Vector2Int cakeDirection = Vector2Int.zero;
    protected Vector2Int giftBoxDirection = Vector2Int.zero;

    protected bool hasWon = false;

    public Timer checktime;


    protected virtual void Start()
    {
        CreateGrid();
        PlaceObstacles();
        PlaceMovingObjects();

        
        
        if (winUI != null) winUI.gameObject.SetActive(false);
        if (loseUI != null) loseUI.gameObject.SetActive(false);
        if(overlay != null) overlay.gameObject.SetActive(false);
    }

    protected void CreateGrid()
    {
        gridCells = new Transform[rows, columns];
        gridObjects = new GameObject[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3 position = new Vector3(j * cellSpacing, i * cellSpacing, 0);
                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                gridCells[i, j] = cell.transform;
                gridObjects[i, j] = cell;
            }
        }
    }

    protected virtual void PlaceObstacles()
    {
        // Các lớp con sẽ ghi đè phương thức này để đặt các vật cản riêng
    }

    protected void PlaceObstacle(int row, int column)
    {
        Vector3 obstaclePosition = new Vector3(column * cellSpacing, row * cellSpacing, 0);
        GameObject obstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity, transform);
        obstacles.Add(obstacle);
        obstaclePositions.Add(new Vector2Int(column, row));
    }

    protected void PlaceMovingObjects()
    {
        cakePosition = new Vector2Int(0, 0);
        cakeTargetPosition = new Vector3(cakePosition.x * cellSpacing, cakePosition.y * cellSpacing, 0);
        cake = Instantiate(cakePrefab, cakeTargetPosition, Quaternion.identity, transform);

        giftBoxPosition = new Vector2Int(columns - 1, rows - 1);
        giftBoxTargetPosition = new Vector3(giftBoxPosition.x * cellSpacing, giftBoxPosition.y * cellSpacing, 0);
        giftBox = Instantiate(giftBoxPrefab, giftBoxTargetPosition, Quaternion.identity, transform);
    }

    protected virtual void Update()
    {
        if (hasWon) return;

        HandleInput();
        MoveObject(cake, ref cakePosition, ref cakeTargetPosition, cakeDirection);
        MoveObject(giftBox, ref giftBoxPosition, ref giftBoxTargetPosition, giftBoxDirection);

        cake.transform.position = Vector3.Lerp(cake.transform.position, cakeTargetPosition,   moveSpeed);
        giftBox.transform.position = Vector3.Lerp(giftBox.transform.position, giftBoxTargetPosition, moveSpeed);

        CheckForWinCondition();
    }

    protected void HandleInput()
    {
        Vector2Int direction = Vector2Int.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector2Int.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Vector2Int.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector2Int.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector2Int.right;
        }

        if (direction != Vector2Int.zero)
        {
            cakeDirection = direction;
            giftBoxDirection = direction;
        }
    }

    protected void MoveObject(GameObject obj, ref Vector2Int objPosition, ref Vector3 targetPosition, Vector2Int direction)
    {
        if (direction == Vector2Int.zero)
            return;

        Vector2Int nextPosition = objPosition + direction;

        if (nextPosition.x < 0 || nextPosition.x >= columns || nextPosition.y < 0 || nextPosition.y >= rows)
        {
            direction = Vector2Int.zero;
            return;
        }

        if (obstaclePositions.Contains(nextPosition))
        {
            direction = Vector2Int.zero;
            return;
        }

        if (obj == cake && nextPosition == giftBoxPosition)
        {
            direction = Vector2Int.zero;
            return;
        }
        if (obj == giftBox && nextPosition == cakePosition)
        {
            direction = Vector2Int.zero;
            return;
        }

        objPosition = nextPosition;
        targetPosition = new Vector3(objPosition.x * cellSpacing, objPosition.y * cellSpacing, 0);
    }

    protected void CheckForWinCondition()
    {
        if (cakePosition == giftBoxPosition + Vector2Int.up)
        {
            hasWon = true;
            StartCoroutine(MoveCakeToGiftBox());
        }
        if (checktime.Check() == true && hasWon == false)
        {
            Debug.Log("You Lose!");
            if (loseUI != null) loseUI.gameObject.SetActive(true);
            if (overlay != null) overlay.gameObject.SetActive(true);
            SetGameElementsActive(false);
            Time.timeScale = 0f;
            
        }
    }

    private IEnumerator MoveCakeToGiftBox()
    {
        // Di chuyển bánh từng bước về phía hộp quà
        Vector3 startPosition = cake.transform.position;
        Vector3 endPosition = giftBox.transform.position + Vector3.up * cellSpacing;
        float duration = 0.5f; // Thời gian để di chuyển đến vị trí cuối cùng
        float elapsed = 0f;

        while (elapsed < duration)
        {
            cake.transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cake.transform.position = endPosition;

        yield return new WaitForSeconds(winDelay);

        cake.SetActive(false);  
        Debug.Log("You Win!");
        if (winUI != null) winUI.gameObject.SetActive(true);
        if (overlay != null) overlay.gameObject.SetActive(true);
        SetGameElementsActive(false);
        Time.timeScale = 0f;
        
        // Hiển thị thông báo trong console
    }
    private void SetGameElementsActive(bool isActive)
    {
        // Ẩn/Hiện các yếu tố trong lưới
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (gridObjects[i, j] != null)
                    gridObjects[i, j].SetActive(isActive);
            }
        }

        // Ẩn/Hiện các vật thể di chuyển
        if (cake != null) cake.SetActive(isActive);
        if (giftBox != null) giftBox.SetActive(isActive);

        // Ẩn/Hiện các vật cản
        foreach (var obstacle in obstacles)
        {
            if (obstacle != null) obstacle.SetActive(isActive);
        }
    }

}



