using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathFinderGrid : MonoBehaviour
{
    RectTransform rectTransform;
    BoxCollider2D boxCollider2D;
    Image image;
    GameObject[,] gridArray;
    string defaultName;
    bool isColliderSizeSet;
    public int gridArrayX;
    public int gridArrayY;
    PathFinder pathFinder;
    //start pos
    public GameObject enemy;
    //target pos
    GameObject player;

    public float distanceToEnemy;
    public float distanceToPlayer;
    public bool isStartPosition;
    public bool isEndPosition;
    public bool isPassable;

    // { Var for PathFind
    public float scoreF;
    float scoreG;
    float scoreH;
    public GameObject parentNode;

    public bool isAddedCloseList;
    // } Var for PathFind

    public List<GameObject> connectedList;
    public bool isInOpenList;

    bool isCreated;
    ObjectPool objectPool;

    void Start()
    {
        isPassable = true;

        defaultName = this.name;

        pathFinder = transform.parent.GetComponent<PathFinder>();
        objectPool = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();


        player = GameObject.FindWithTag("Player");
        enemy = pathFinder.enemy;


        rectTransform = GetComponent<RectTransform>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        image = GetComponent<Image>();

        if (!isColliderSizeSet)
        {
            StartCoroutine(ColliderSizeSet());
        }

        gridArray = pathFinder.gridArray;
        for (int y = 0; y < gridArray.GetLength(1); y++)
        {
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                if (gridArray[x, y] == this.gameObject)
                {
                    gridArrayX = x;
                    gridArrayY = y;
                }
            }
        }
    }
    private void OnEnable()
    {
        Debug.Log("Enable");
        if (isCreated)
        {
            objectPool = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();
            pathFinder = transform.parent.GetComponent<PathFinder>();
            enemy = pathFinder.enemy;

            if (!isColliderSizeSet)
            {
                StartCoroutine(ColliderSizeSet());
            }

            gridArray = pathFinder.gridArray;
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                for (int x = 0; x < gridArray.GetLength(0); x++)
                {
                    if (gridArray[x, y] == this.gameObject)
                    {
                        gridArrayX = x;
                        gridArrayY = y;
                    }
                }
            }
        }
        isCreated = true;
    }


    bool isFirstActiveFalse;
    private void OnDisable()
    {
        Debug.Log("Disable");
        if (isFirstActiveFalse)
        {
            objectPool.ReturnObject(this.gameObject, 1);
        }
        isFirstActiveFalse = true;


    }


    void Update()
    {

        CheckDistance();
        SetGridStatus();

        //find scoreH
        if (pathFinder.endGrid != null)
        {
            scoreH = Vector2.Distance(transform.position, pathFinder.endGrid.transform.position);
        }
        //find scoreF
        //scoreF = scoreG + scoreH;


        if (pathFinder.checkGrid == this.gameObject)
        {
            AddGridToOpenList();
            scoreG = 0;
            scoreF = scoreH;
        }

    }

    void SetGridStatus()
    {
        if (isStartPosition)
        {
            this.name = "Start";
            image.color = new Color32(0, 0, 255, 100);
            pathFinder.startGrid = this.gameObject;
        }
        else if (isEndPosition)
        {
            this.name = "End";
            image.color = new Color32(0, 0, 255, 100);
            pathFinder.endGrid = this.gameObject;
        }
        else if (!isPassable)
        {
            this.name = "Obstacle";
            image.color = new Color32(0, 0, 0, 100);
        }
        else if (isAddedCloseList)
        {
            image.color = new Color32(255, 0, 0, 100);
        }
        else
        {
            if (!isInOpenList)
            {
                this.name = defaultName;
                image.color = new Color32(255, 255, 255, 0);
            }
        }
        pathFinder.isSetGridStatus = true;
    }

    void CheckDistance()
    {
        distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
    }

    IEnumerator ColliderSizeSet()
    {
        isColliderSizeSet = true;
        yield return null;
        boxCollider2D.size = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y);
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (isPassable && other.gameObject == enemy)
        {
            isPassable = true;
        }
        else if (other.tag == "Player" || other.tag == "Monster" || !other.gameObject.GetComponent<TestObstacle>().isPassable)
        {
            isPassable = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Monster" || !other.gameObject.GetComponent<TestObstacle>().isPassable)
        {
            isPassable = true;
        }
    }

    void CheckConnectedGrid(int x, int y)
    {
        if (x < gridArray.GetLength(0) && y < gridArray.GetLength(1))
        {
            if (gridArray[x, y] != null && gridArray[x, y].name == "End")
            {
                pathFinder.openList.Add(gridArray[x, y]);
                gridArray[x, y].GetComponent<PathFinderGrid>().isInOpenList = true;
                gridArray[x, y].GetComponent<PathFinderGrid>().parentNode = pathFinder.checkGrid;
            }
            else if (gridArray[x, y] != null && !gridArray[x, y].GetComponent<PathFinderGrid>().isAddedCloseList && gridArray[x, y].GetComponent<PathFinderGrid>().isPassable)
            {
                if (pathFinder.openList.IndexOf(gridArray[x, y]) == -1)
                {
                    gridArray[x, y].GetComponent<PathFinderGrid>().scoreG = Vector2.Distance(gridArray[x, y].transform.position, pathFinder.checkGrid.transform.position);
                    gridArray[x, y].GetComponent<PathFinderGrid>().scoreF = gridArray[x, y].GetComponent<PathFinderGrid>().scoreG + gridArray[x, y].GetComponent<PathFinderGrid>().scoreH;

                    pathFinder.openList.Add(gridArray[x, y]);
                    gridArray[x, y].GetComponent<PathFinderGrid>().isInOpenList = true;
                    gridArray[x, y].GetComponent<PathFinderGrid>().parentNode = pathFinder.checkGrid;
                    gridArray[x, y].GetComponent<Image>().color = new Color32(0, 255, 0, 100);
                }
            }
        }
    }

    void AddGridToOpenList()
    {
        CheckConnectedGrid(gridArrayX - 1, gridArrayY - 1);
        CheckConnectedGrid(gridArrayX, gridArrayY - 1);
        CheckConnectedGrid(gridArrayX + 1, gridArrayY - 1);
        CheckConnectedGrid(gridArrayX - 1, gridArrayY);
        CheckConnectedGrid(gridArrayX + 1, gridArrayY);
        CheckConnectedGrid(gridArrayX - 1, gridArrayY + 1);
        CheckConnectedGrid(gridArrayX, gridArrayY + 1);
        CheckConnectedGrid(gridArrayX + 1, gridArrayY + 1);
    }
    public void Reset()
    {
        rectTransform = default;
        boxCollider2D = default;
        image = default;
        gridArray = default;
        defaultName = default;
        isColliderSizeSet = default;
        gridArrayX = default;
        gridArrayY = default;
        pathFinder = default;
        enemy = default;
        player = default;
        distanceToEnemy = default;
        distanceToPlayer = default;
        isStartPosition = default;
        isEndPosition = default;
        isPassable = default;
        scoreF = default;
        scoreG = default;
        scoreH = default;
        parentNode = default;
        isAddedCloseList = default;
        connectedList = default;
        isInOpenList = default;
    }
}
