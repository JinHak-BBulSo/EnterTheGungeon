using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathFinder : MonoBehaviour
{
    int gridNumber;
    RectTransform rectTransform;

    float cellSizeX;
    float cellSizeY;

    public GameObject[,] gridArray;

    //start pos
    public GameObject enemy;
    int startX;
    int startY;

    //end pos
    GameObject player;
    int endX;
    int endY;


    public float minDistToEnemy;
    public float minDistToPlayer;

    public GameObject startGrid;
    public GameObject endGrid;

    public bool isSetGridStatus;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        rectTransform = GetComponent<RectTransform>();

        cellSizeX = GetComponent<GridLayoutGroup>().cellSize.x;
        cellSizeY = GetComponent<GridLayoutGroup>().cellSize.y;

        gridNumber = (int)(rectTransform.sizeDelta.x / cellSizeX) * (int)(rectTransform.sizeDelta.y / cellSizeY);

        gridArray = new GameObject[(int)(rectTransform.sizeDelta.x / cellSizeX), (int)(rectTransform.sizeDelta.y / cellSizeY)];

        for (int y = 0; y < gridArray.GetLength(1); y++)
        {
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {

                GameObject grid_ = Instantiate(Resources.Load<GameObject>("02.HT/Prefabs/PathFinder/PathFinderGrid"), transform.position, transform.rotation);
                grid_.transform.SetParent(this.transform);
                grid_.GetComponent<RectTransform>().localScale = Vector3.one;

                gridArray[x, y] = grid_;
            }
        }



    }

    // Update is called once per frame
    void Update()
    {
        CheckMinDistance();
        SetStartAndEnd();

        if (isSetGridStatus)
        {
            if (!isFinPathFind)
            {
                PathFind();
            }
        }

        FindNextCloseListObject();

        if (isFinPathFind && !isCompletePath)
        {
            CompletePath(completePathNode);
        }
    }

    // @brief set starting and end point of pathfinder
    void CheckMinDistance()
    {
        minDistToEnemy = 0;
        minDistToPlayer = 0;

        for (int y = 0; y < gridArray.GetLength(1); y++)
        {
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                if (minDistToEnemy == 0 || minDistToEnemy >= gridArray[x, y].GetComponent<PathFinderGrid>().distanceToEnemy)
                {
                    minDistToEnemy = gridArray[x, y].GetComponent<PathFinderGrid>().distanceToEnemy;
                }
                if (minDistToPlayer == 0 || minDistToPlayer > gridArray[x, y].GetComponent<PathFinderGrid>().distanceToPlayer)
                {
                    minDistToPlayer = gridArray[x, y].GetComponent<PathFinderGrid>().distanceToPlayer;
                }
            }
        }
    }

    void SetStartAndEnd()
    {
        for (int y = 0; y < gridArray.GetLength(1); y++)
        {
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                if (minDistToEnemy == gridArray[x, y].GetComponent<PathFinderGrid>().distanceToEnemy)
                {
                    gridArray[x, y].GetComponent<PathFinderGrid>().isStartPosition = true;
                    startX = x;
                    startY = y;
                }
                else
                {
                    gridArray[x, y].GetComponent<PathFinderGrid>().isStartPosition = false;
                }

                if (minDistToPlayer == gridArray[x, y].GetComponent<PathFinderGrid>().distanceToPlayer)
                {
                    gridArray[x, y].GetComponent<PathFinderGrid>().isEndPosition = true;
                    endX = x;
                    endY = y;
                }
                else
                {
                    gridArray[x, y].GetComponent<PathFinderGrid>().isEndPosition = false;
                }
            }
        }
    }

    public List<GameObject> openList;
    public List<GameObject> closeList;

    public GameObject checkGrid;
    //8방향 체크
    bool isInitPathFind;

    bool isFinPathFind;
    void PathFind()
    {
        StartCoroutine(InitPathFind());
        if (nextCloseListObject != null && closeList.IndexOf(nextCloseListObject) == -1)
        {
            closeList.Add(nextCloseListObject);
            checkGrid = nextCloseListObject;
            checkGrid.GetComponent<PathFinderGrid>().isAddedCloseList = true;

            //test
            completePathNode = checkGrid;
            //test

            openList.Remove(checkGrid);
            if (checkGrid.name == "End")
            {
                isFinPathFind = true;
            }
        }
    }
    IEnumerator InitPathFind()
    {
        yield return null;
        if (closeList.Count == 0)
        {
            closeList.Add(startGrid);
            checkGrid = startGrid;
            checkGrid.GetComponent<PathFinderGrid>().isAddedCloseList = true;
        }

    }
    public GameObject nextCloseListObject;
    void FindNextCloseListObject()
    {
        float minScoreF = default;
        for (int i = 0; i < openList.Count; i++)
        {
            if (openList[i].name == "End")
            {
                nextCloseListObject = openList[i];
            }
            else
            {
                if (minScoreF == default)
                {
                    minScoreF = openList[0].GetComponent<PathFinderGrid>().scoreF;
                }
                if (minScoreF != default && minScoreF > openList[i].GetComponent<PathFinderGrid>().scoreF)
                {
                    minScoreF = openList[i].GetComponent<PathFinderGrid>().scoreF;
                    nextCloseListObject = openList[i];
                }
            }
        }
    }

    public List<Vector2> completePath;
    GameObject completePathNode;
    bool isCompletePath;
    void CompletePath(GameObject completePathNode_)
    {
        if (completePathNode_ == null)
        {

            isCompletePath = true;
            if (enemy.GetComponent<TestEnemy>().completePath != completePath)
            {
                enemy.GetComponent<TestEnemy>().completePath = completePath;
            }
            else { }
            enemy.GetComponent<TestEnemy>().isPathFind = false;
            Destroy(this.gameObject);

            

        }
        else
        {
            completePath.Add(completePathNode_.transform.position);
            completePathNode_ = completePathNode_.GetComponent<PathFinderGrid>().parentNode;
            CompletePath(completePathNode_);
        }
    }
}
