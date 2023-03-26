using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public class MapNode
    {
        public MapNode leftNode = default;
        public MapNode rightNode = default;
        public MapNode parentNode = default;
        public RectInt nodeRect = default; // 현재 본인의 RECT 크기
        public Vector2 nodePosition = default;

        public Vector2 leftCenter = default;
        public Vector2 rightCenter = default;
        public Vector2 topCenter = default;
        public Vector2 bottomCenter = default;

        public int nodeIndex = 1;
        public Room room = default;

        public Vector2Int Center
        {
            get
            {
                return new Vector2Int(nodeRect.x + nodeRect.width / 2, nodeRect.y + nodeRect.height / 2);
            }
            private set { return; }
        }


        public MapNode(RectInt nodeRect_, int index_)
        {
            this.nodeRect = nodeRect_;
            this.nodeIndex = index_;
        }
    }

    private MapNode startMap = new MapNode(new RectInt(0, 0, 22, 22), 0);
    private MapNode shopMap = default;
    private MapNode bossMap = default;
    private GameObject horizontalDoor = default;
    private GameObject verticalDoor = default;

    [SerializeField]
    private List<GameObject> mapPrefabs = new List<GameObject>();
    [SerializeField]
    private List<GameObject> specialMapPrefabs = new List<GameObject>();

    private GameObject mapAccessLine = default;
    private GameObject mapAccessWall = default;
    private GameObject allDoors = default;

    private const float MINIMUM_DIVIDE_RATE = 0.36f;
    private const float MAXIMUM_DIVIDE_RATE = 0.64f;
    private const int MAXIMUM_DEPTH = 4;

    private GameObject accessLine = default;
    private GameObject lineWallTop = default;
    private GameObject lineWallBottom = default;
    private GameObject maps = default;

    private MapNode[] mapNodeArray = new MapNode[MAXIMUM_DEPTH * MAXIMUM_DEPTH];

    // 전체 맵의 크기
    [SerializeField]
    Vector2Int mapSize = default;

    private void Awake()
    {
        DoorManager.Instance.Create();
        maps = transform.GetChild(0).gameObject;
        mapAccessLine = transform.GetChild(1).gameObject;
        mapAccessWall = transform.GetChild(2).gameObject;
        allDoors = transform.GetChild(3).gameObject;
        accessLine = Resources.Load<GameObject>("00.JinHak/Prefabs/MapGenerator/AccessLine");
        lineWallTop = Resources.Load<GameObject>("00.JinHak/Prefabs/MapGenerator/lineWallTop");
        lineWallBottom = Resources.Load<GameObject>("00.JinHak/Prefabs/MapGenerator/lineWallBottom");
        horizontalDoor = Resources.Load<GameObject>("00.JinHak/Prefabs/MapGenerator/HorizonDoor");
        verticalDoor = Resources.Load<GameObject>("00.JinHak/Prefabs/MapGenerator/VerticalDoor");

        MapNode root_ = new MapNode(new RectInt(0, 0, mapSize.x, mapSize.y), 0);
        DivideMap(root_, 0);
        RoomAccess(root_, 0);
    }
    void Start()
    {
        AccessZone();
        AccessZoneRoom();

        CompositeCollider2D lineComposite = mapAccessLine.AddComponent<CompositeCollider2D>();
        lineComposite.isTrigger = true;
        lineComposite.geometryType = CompositeCollider2D.GeometryType.Polygons;

        lineComposite.gameObject.SetActive(false);
        lineComposite.gameObject.SetActive(true); 

        ColliderSizeSet();
        StartCoroutine(AccessRoomLineColEnableFalse(lineComposite));
    }

    private void ColliderSizeSet()
    {
        for(int i = 0; i < mapAccessWall.transform.childCount; i++)
        {
            BoxCollider2D childCollider_ = mapAccessWall.transform.GetChild(i).GetComponent<BoxCollider2D>();
            if(childCollider_.size.x > 0.6)
            {
                childCollider_.size = new Vector2(childCollider_.size.x * 0.85f, 0.3f);
            }
            else
            {
                childCollider_.size = new Vector2(0.3f, childCollider_.size.y * 0.85f);
            }
            childCollider_.isTrigger = false;
        }
    }
    // [KJH] 2023-03-15
    // @brief 전체 맵을 랜덤하게 분할하는 함수
    // @param nowNode 나눌 대상이 대상이 되는 현재 노드
    // @param h 트리에서 나누어지는 최대 높이
    private void DivideMap(MapNode nowNode_, int height_)
    {
        if (height_ == MAXIMUM_DEPTH) return;

        // 가로와 세로 중에서 긴 값을 저장
        int maxLength_ = Mathf.Max(nowNode_.nodeRect.width, nowNode_.nodeRect.height);

        // 가로와 세로 중 긴 값을 기준으로 분할
        int split_ = Mathf.RoundToInt(Random.Range(maxLength_ * MINIMUM_DIVIDE_RATE, maxLength_ * MAXIMUM_DIVIDE_RATE));

        // 가로가 더 긴 경우
        if (nowNode_.nodeRect.width >= nowNode_.nodeRect.height)
        {
            nowNode_.leftNode = new MapNode(new RectInt(nowNode_.nodeRect.x, nowNode_.nodeRect.y, split_,
                nowNode_.nodeRect.height), nowNode_.nodeIndex * 2);

            nowNode_.rightNode = new MapNode(new RectInt(nowNode_.nodeRect.x + split_, nowNode_.nodeRect.y,
                nowNode_.nodeRect.width - split_, nowNode_.nodeRect.height), nowNode_.nodeIndex * 2 + 1);

            Vector2 start_ = new Vector2(nowNode_.nodeRect.x + split_, nowNode_.nodeRect.y);
            Vector2 end_ = new Vector2(nowNode_.nodeRect.x + split_, nowNode_.nodeRect.y + nowNode_.nodeRect.height);

            //DrawLine(start_, end_);
        }

        // 세로가 더 긴 경우
        else
        {
            nowNode_.leftNode = new MapNode(new RectInt(nowNode_.nodeRect.x, nowNode_.nodeRect.y, 
                nowNode_.nodeRect.width, split_), nowNode_.nodeIndex * 2);
            nowNode_.leftNode.nodePosition = new Vector2(nowNode_.nodeRect.x, nowNode_.nodeRect.y - (maxLength_ - split_));

            nowNode_.rightNode = new MapNode(new RectInt(nowNode_.nodeRect.x, nowNode_.nodeRect.y + split_,
                nowNode_.nodeRect.width, nowNode_.nodeRect.height - split_), nowNode_.nodeIndex * 2 + 1);
            nowNode_.rightNode.nodePosition = new Vector2(nowNode_.nodeRect.x, nowNode_.nodeRect.y + split_);


            Vector2 start_ = new Vector2(nowNode_.nodeRect.x, nowNode_.nodeRect.y + split_);
            Vector2 end_ = new Vector2(nowNode_.nodeRect.x + nowNode_.nodeRect.width, nowNode_.nodeRect.y + split_);

            //DrawLine(start_, end_);
        }

        nowNode_.leftNode.parentNode = nowNode_;
        nowNode_.rightNode.parentNode = nowNode_;

        DivideMap(nowNode_.leftNode, height_ + 1);
        DivideMap(nowNode_.rightNode, height_ + 1);
    }
    

    // [KJH] 2023-03-16
    // @brief 방 간의 연결을 보여주는 함수
    // @param nowNode 현재 노드
    // @param height 트리 구조에서의 높이
    private void RoomAccess(MapNode nowNode_, int height_)
    {
        if (height_ == MAXIMUM_DEPTH)
        {
            MapCreate(nowNode_);

            return;
        }

        Vector2Int leftNodeCenter = nowNode_.leftNode.Center;
        Vector2Int rightNodeCenter = nowNode_.rightNode.Center;

        if (height_ < MAXIMUM_DEPTH - 2)
        {
            RoomAccess(nowNode_.leftNode, height_ + 1);
            RoomAccess(nowNode_.rightNode, height_ + 1);
        }
        else
        {
            Vector2 start_;
            Vector2 end_;

            if(nowNode_.leftNode.Center.y == nowNode_.rightNode.Center.y)
            {
                start_ = new Vector2(leftNodeCenter.x, leftNodeCenter.y);
                end_ = new Vector2(rightNodeCenter.x, leftNodeCenter.y);
            }
            else
            {
                start_ = new Vector2(rightNodeCenter.x, leftNodeCenter.y);
                end_ = new Vector2(rightNodeCenter.x, rightNodeCenter.y);
            }
            
            nowNode_.leftNode.nodePosition = start_ - mapSize / 2;
            nowNode_.rightNode.nodePosition = end_ - mapSize / 2;

            RoomAccess(nowNode_.leftNode, height_ + 1);
            RoomAccess(nowNode_.rightNode, height_ + 1);
        }
    }
    private void SetStartRoom()
    {
        List<int> accessRoomIndex_ = new List<int>{ 0, 4, 8, 15};

        for (int i = 0; i < 4; i++)
        {
            int ran_ = Random.Range(0, accessRoomIndex_.Count);
            
            Vector2 mapCreatePoint_ = default;
            GameObject startMapObj = Instantiate(specialMapPrefabs[i], maps.transform);

            startMap.room = startMapObj.GetComponent<Room>();

            Vector2 start_ = default;
            Vector2 end_ = default;

            Vector2 startLeft_;
            Vector2 endLeft_;
            Vector2 startRight_;
            Vector2 endRight_;

            float offsetHorizontal = startMap.room.roomSize.x / 2 + 2;
            float offsetVertical = startMap.room.roomSize.y / 2 + 2;

            switch (accessRoomIndex_[ran_])
            {
                case 0:
                    mapCreatePoint_ = new Vector2(mapNodeArray[0].leftCenter.x - offsetHorizontal, mapNodeArray[0].nodePosition.y);
                    startMapObj.transform.position = mapCreatePoint_;
                    startMap.nodePosition = startMapObj.transform.position;
                    startMap.rightCenter = startMap.nodePosition + new Vector2(startMap.room.roomSize.x / 2, 0) * 0.65f;

                    start_ = startMap.rightCenter;
                    end_ = mapNodeArray[0].leftCenter;
                    Instantiate(verticalDoor, allDoors.transform).transform.position = start_ + new Vector2(2, 0);
                    break;
                case 4:
                    mapCreatePoint_ = new Vector2(mapNodeArray[4].leftCenter.x - offsetHorizontal, mapNodeArray[4].nodePosition.y);
                    startMapObj.transform.position = mapCreatePoint_;
                    startMap.nodePosition = startMapObj.transform.position;
                    startMap.rightCenter = startMap.nodePosition + new Vector2(startMap.room.roomSize.x / 2, 0) * 0.65f;

                    start_ = startMap.rightCenter;
                    end_ = mapNodeArray[4].leftCenter;
                    Instantiate(verticalDoor, allDoors.transform).transform.position = start_ + new Vector2(2, 0);
                    break;
                case 8:
                    mapCreatePoint_ = new Vector2(mapNodeArray[8].nodePosition.x, mapNodeArray[8].bottomCenter.y - offsetVertical);
                    startMapObj.transform.position = mapCreatePoint_;
                    startMap.nodePosition = startMapObj.transform.position;
                    startMap.topCenter = startMap.nodePosition + new Vector2(0, startMap.room.roomSize.y / 2) * 0.65f;

                    start_ = startMap.topCenter;
                    end_ = mapNodeArray[8].bottomCenter;
                    Instantiate(horizontalDoor, allDoors.transform).transform.position = start_ + new Vector2(0, 2);
                    break;
                case 15:
                    mapCreatePoint_ = new Vector2(mapNodeArray[15].nodePosition.x, mapNodeArray[15].topCenter.y + offsetVertical);
                    startMapObj.transform.position = mapCreatePoint_;
                    startMap.nodePosition = startMapObj.transform.position;
                    startMap.bottomCenter = startMap.nodePosition - new Vector2(0, startMap.room.roomSize.y / 2) * 0.65f;

                    start_ = mapNodeArray[15].topCenter;
                    end_ = startMap.bottomCenter;
                    Instantiate(horizontalDoor, allDoors.transform).transform.position = start_ + new Vector2(0, 2);
                    break;
            }

            if(i == 0) startMapObj.GetComponent<StartPoint>().SetPlayer();
            DrawAccessLine(start_, end_, accessLine);

            // x 포지션 값이 같은 경우
            if (accessRoomIndex_[ran_] == 0 || accessRoomIndex_[ran_] == 4)
            {
                startLeft_ = new Vector2(start_.x + 0.65f, start_.y + 1);
                startRight_ = new Vector2(start_.x + 0.65f, start_.y - 1);

                endLeft_ = new Vector2(end_.x - 0.65f, end_.y + 1);
                endRight_ = new Vector2(end_.x - 0.65f, end_.y - 1);

                DrawAccessLine(startLeft_, endLeft_, lineWallTop);
                DrawAccessLine(startRight_, endRight_, lineWallBottom);
            }
            // y 포지션 값이 같은 경우
            else
            {
                startLeft_ = new Vector2(start_.x - 1, start_.y + 0.65f);
                startRight_ = new Vector2(start_.x + 1, start_.y + 0.65f);

                endLeft_ = new Vector2(end_.x - 1, end_.y - 0.65f);
                endRight_ = new Vector2(end_.x + 1, end_.y - 0.65f);

                DrawAccessLine(startLeft_, endLeft_, lineWallTop);
                DrawAccessLine(startRight_, endRight_, lineWallBottom);
            }
            accessRoomIndex_.RemoveAt(ran_);
        }
    }
    
    private void MapCreate(MapNode nowNode_)
    {
        mapNodeArray[nowNode_.nodeIndex] = nowNode_;

        int index = 0;

        while (true)
        {
            int ran_ = Random.Range(2, mapPrefabs.Count);
            Room room_ = mapPrefabs[ran_].GetComponent<Room>();

            if (room_.roomSize.x > nowNode_.nodeRect.width || room_.roomSize.y > nowNode_.nodeRect.height)
            {
                if (index > 100)
                {
                    Debug.Log(index);
                    Debug.Log(nowNode_.nodeIndex);
                    GameObject nodeRoom_ = Instantiate(mapPrefabs[2], maps.transform);
                    room_ = nodeRoom_.GetComponent<Room>();
                    nodeRoom_.transform.position = nowNode_.nodePosition;
                    nowNode_.room = nodeRoom_.GetComponent<Room>();

                    nowNode_.leftCenter = nowNode_.nodePosition - new Vector2(room_.roomSize.x / 2, 0) * 0.65f;
                    nowNode_.rightCenter = nowNode_.nodePosition + new Vector2(room_.roomSize.x / 2, 0) * 0.65f;
                    nowNode_.topCenter = nowNode_.nodePosition + new Vector2(0, room_.roomSize.y / 2) * 0.65f;
                    nowNode_.bottomCenter = nowNode_.nodePosition - new Vector2(0, room_.roomSize.y / 2) * 0.65f;
                    break;
                }
                index++;
                continue;
            }
            else
            {
                GameObject nodeRoom_ = Instantiate(room_.gameObject, maps.transform);
                nodeRoom_.transform.position = nowNode_.nodePosition;
                nowNode_.room = room_;

                nowNode_.leftCenter = nowNode_.nodePosition - new Vector2(room_.roomSize.x / 2, 0) * 0.65f;
                nowNode_.rightCenter = nowNode_.nodePosition + new Vector2(room_.roomSize.x / 2, 0) * 0.65f;
                nowNode_.topCenter = nowNode_.nodePosition + new Vector2(0, room_.roomSize.y / 2) * 0.65f;
                nowNode_.bottomCenter = nowNode_.nodePosition - new Vector2(0, room_.roomSize.y / 2) * 0.65f;

                break;
            }
        }

        if (nowNode_.nodeIndex == MAXIMUM_DEPTH * MAXIMUM_DEPTH - 1)
        {
            for (int i = 0; i < mapNodeArray.Count(); i += 2)
            {
                Vector2 start_;
                Vector2 end_;
                Vector2 startLeft_;
                Vector2 endLeft_;
                Vector2 startRight_;
                Vector2 endRight_;

                // x 포지션 값이 같은 경우
                if (mapNodeArray[i].nodePosition.x == mapNodeArray[i + 1].nodePosition.x)
                {
                    start_ = mapNodeArray[i].topCenter;
                    end_ = mapNodeArray[i + 1].bottomCenter;

                    startLeft_ = new Vector2(start_.x - 1, start_.y);
                    startRight_ = new Vector2(start_.x + 1, start_.y);

                    endLeft_ = new Vector2(end_.x - 1, end_.y - 0.5f);
                    endRight_ = new Vector2(end_.x + 1, end_.y - 0.5f);

                    Instantiate(horizontalDoor, allDoors.transform).transform.position = start_ + new Vector2(0, 2);
                    Instantiate(horizontalDoor, allDoors.transform).transform.position = end_ - new Vector2(0, 2);
                }
                // y 포지션 값이 같은 경우
                else
                {
                    start_ = mapNodeArray[i].rightCenter;
                    end_ = mapNodeArray[i + 1].leftCenter;

                    startLeft_ = new Vector2(start_.x, start_.y - 1);
                    startRight_ = new Vector2(start_.x, start_.y + 1);

                    endLeft_ = new Vector2(end_.x, end_.y - 1);
                    endRight_ = new Vector2(end_.x, end_.y + 1);

                    Instantiate(verticalDoor, allDoors.transform).transform.position = start_ + new Vector2(2, 0);
                    Instantiate(verticalDoor, allDoors.transform).transform.position = end_ - new Vector2(2, 0);
                }

                DrawAccessLine(start_, end_, accessLine);
                DrawAccessLine(startLeft_, endLeft_, lineWallTop);
                DrawAccessLine(startRight_, endRight_, lineWallBottom);
            }
        }
    }
    private void AccessZone()
    {
        int[] zoneAccessRoomIndex_ = new int[4] { 3, 5, 9, 12 };

        if (mapNodeArray[4].nodePosition.y == mapNodeArray[5].nodePosition.y)
        {
            zoneAccessRoomIndex_[1] = 5;
            if (mapNodeArray[5].nodePosition.y > mapNodeArray[6].nodePosition.y)
            {
                zoneAccessRoomIndex_[1] = 6; 
            }
            else if (mapNodeArray[5].nodePosition.y == mapNodeArray[6].nodePosition.y)
            {
                zoneAccessRoomIndex_[1] = 7;
            }
        }
        else if (mapNodeArray[5].nodePosition.y > mapNodeArray[6].nodePosition.y)
        {
            zoneAccessRoomIndex_[1] = 6;
            if (mapNodeArray[6].nodePosition.y == mapNodeArray[7].nodePosition.y)
            {
                zoneAccessRoomIndex_[1] = 7;
            }
        }

        if (mapNodeArray[10].nodePosition.y > mapNodeArray[9].nodePosition.y)
        {
            zoneAccessRoomIndex_[2] = 10;
        }
        else if (mapNodeArray[8].nodePosition.y == mapNodeArray[9].nodePosition.y)
        {
            zoneAccessRoomIndex_[2] = 8;
            
        }

        // 존1, 존2 연결
        Vector2 start_;
        Vector2 middle_;
        Vector2 middle2_;
        Vector2 end_;

        // 존1, 존2 연결
        start_ = mapNodeArray[zoneAccessRoomIndex_[0]].topCenter;
        end_ = mapNodeArray[zoneAccessRoomIndex_[1]].bottomCenter;
        middle_ = new Vector2(start_.x, (start_.y + end_.y) / 2);
        middle2_ = new Vector2(end_.x, (start_.y + end_.y) / 2);

        DrawAccessLine(start_, end_ , middle_, middle2_, accessLine, 0);
        DrawAccessWallSide(start_, end_, middle_, middle2_, 0);
        Instantiate(horizontalDoor, allDoors.transform).transform.position = start_ + new Vector2(0, 2);
        Instantiate(horizontalDoor, allDoors.transform).transform.position = end_ - new Vector2(0, 2);

        // 존1, 존3 연결

        start_ = mapNodeArray[zoneAccessRoomIndex_[0]].rightCenter;
        end_ = mapNodeArray[zoneAccessRoomIndex_[2]].leftCenter;
        middle_ = new Vector2((start_.x + end_.x) / 2, start_.y);
        middle2_ = new Vector2((start_.x + end_.x) / 2, end_.y);

        DrawAccessLine(start_, end_, middle_, middle2_, accessLine, 1);
        DrawAccessWallSide(start_, end_, middle_, middle2_, 1);
        Instantiate(verticalDoor, allDoors.transform).transform.position = start_ + new Vector2(2, 0);
        Instantiate(verticalDoor, allDoors.transform).transform.position = end_ - new Vector2(2, 0);

        // 존2, 존4 연결

        start_ = mapNodeArray[zoneAccessRoomIndex_[1]].rightCenter;
        end_ = mapNodeArray[zoneAccessRoomIndex_[3]].leftCenter;
        middle_ = new Vector2((start_.x + end_.x) / 2, start_.y);
        middle2_ = new Vector2((start_.x + end_.x) / 2, end_.y);


        DrawAccessLine(start_, end_, middle_, middle2_, accessLine, 1);
        DrawAccessWallSide(start_, end_, middle_, middle2_, 1);
        Instantiate(verticalDoor, allDoors.transform).transform.position = start_ + new Vector2(2, 0);
        Instantiate(verticalDoor, allDoors.transform).transform.position = end_ - new Vector2(2, 0);

        // 존3, 존4 연결
        start_ = mapNodeArray[zoneAccessRoomIndex_[2]].topCenter;
        end_ = mapNodeArray[zoneAccessRoomIndex_[3]].bottomCenter;
        middle_ = new Vector2(start_.x, (start_.y + end_.y) / 2);
        middle2_ = new Vector2(end_.x, (start_.y + end_.y) / 2);

        DrawAccessLine(start_, end_, middle_, middle2_, accessLine, 0);
        DrawAccessWallSide(start_, end_, middle_, middle2_, 0);
        Instantiate(horizontalDoor, allDoors.transform).transform.position = start_ + new Vector2(0, 2);
        Instantiate(horizontalDoor, allDoors.transform).transform.position = end_ - new Vector2(0, 2);
    }

    private void AccessZoneRoom()
    {
        Vector2 start_ = default;
        Vector2 middle_ = default;
        Vector2 middle2_ = default;
        Vector2 end_ = default;

        for (int i = 0; i < MAXIMUM_DEPTH; i++)
        {
            MapNode middleNode = mapNodeArray[MAXIMUM_DEPTH * i + 1];

            if (middleNode.nodePosition.y > mapNodeArray[MAXIMUM_DEPTH * i].nodePosition.y)
            {
                start_ = middleNode.rightCenter;
                end_ = mapNodeArray[MAXIMUM_DEPTH * i + MAXIMUM_DEPTH - 1].leftCenter;
                middle_ = new Vector2((start_.x + end_.x) / 2, start_.y);
                middle2_ = new Vector2(middle_.x, end_.y);
                if (mapNodeArray[MAXIMUM_DEPTH * i + 2].nodePosition.y == mapNodeArray[MAXIMUM_DEPTH * i + 3].nodePosition.y)
                {
                    start_ = middleNode.rightCenter;
                    end_ = mapNodeArray[MAXIMUM_DEPTH * i + 2].leftCenter;
                    middle_ = new Vector2((start_.x + end_.x) / 2, start_.y);
                    middle2_ = new Vector2(middle_.x, end_.y);
                }
                DrawAccessWallSide(start_, end_, middle_, middle2_, 1);
                DrawAccessLine(start_, end_, middle_, middle2_, accessLine, 1);
                Instantiate(verticalDoor, allDoors.transform).transform.position = start_ + new Vector2(2, 0);
                Instantiate(verticalDoor, allDoors.transform).transform.position = end_ - new Vector2(2, 0);
            }
            else
            {
                start_ = middleNode.topCenter;
                end_ = mapNodeArray[MAXIMUM_DEPTH * i + MAXIMUM_DEPTH - 1].bottomCenter;
                middle_ = new Vector2(start_.x, (start_.y + end_.y) / 2);
                middle2_ = new Vector2(end_.x, middle_.y);

                if (middleNode.nodePosition.y >= mapNodeArray[MAXIMUM_DEPTH * i + 2].nodePosition.y)
                {
                    start_ = middleNode.rightCenter;
                    end_ = mapNodeArray[MAXIMUM_DEPTH * i + 2].leftCenter;
                    middle_ = new Vector2((start_.x + end_.x) / 2, start_.y);
                    middle2_ = new Vector2(middle_.x, end_.y);
                    DrawAccessWallSide(start_, end_, middle_, middle2_, 1);
                    DrawAccessLine(start_, end_, middle_, middle2_, accessLine, 1);
                    Instantiate(verticalDoor, allDoors.transform).transform.position = start_ + new Vector2(2, 0);
                    Instantiate(verticalDoor, allDoors.transform).transform.position = end_ - new Vector2(2, 0);
                }
                else
                {
                    DrawAccessWallSide(start_, end_, middle_, middle2_, 0);
                    DrawAccessLine(start_, end_, middle_, middle2_, accessLine, 0);
                    Instantiate(horizontalDoor, allDoors.transform).transform.position = start_ + new Vector2(0, 2);
                    Instantiate(horizontalDoor, allDoors.transform).transform.position = end_ - new Vector2(0, 2);
                }
            }  
        }
        SetStartRoom();
    }

    private void DrawAccessWallSide(Vector2 start_, Vector2 end_, Vector2 middle_, Vector2 middle2_, int distanceIndex_)
    {
        Vector2 startLeft_;
        Vector2 middleLeft_;
        Vector2 middleLeft2_;
        Vector2 endLeft_;

        Vector2 startRight_;
        Vector2 middleRight_;
        Vector2 middleRight2_;
        Vector2 endRight_;

        if (distanceIndex_ == 0)
        {
            startLeft_ = new Vector2(start_.x - 1, start_.y + 0.65f);
            startRight_ = new Vector2(start_.x + 1, start_.y + 0.65f);

            if (middle2_.x > middle_.x)
            {
                middleLeft_ = new Vector2(middle_.x - 1, middle_.y + 1);
                middleRight_ = new Vector2(middle_.x + 1, middle_.y - 1);


                middleLeft2_ = new Vector2(middle2_.x - 1, middle2_.y + 1);
                middleRight2_ = new Vector2(middle2_.x + 1, middle2_.y - 1);
            }
            else
            {
                middleLeft_ = new Vector2(middle_.x - 1, middle_.y - 1);
                middleRight_ = new Vector2(middle_.x + 1, middle_.y + 1);


                middleLeft2_ = new Vector2(middle2_.x - 1, middle2_.y - 1);
                middleRight2_ = new Vector2(middle2_.x + 1, middle2_.y + 1);
            }

            endLeft_ = new Vector2(end_.x - 1, end_.y - 0.65f);
            endRight_ = new Vector2(end_.x + 1, end_.y - 0.65f);
        }
        else
        {
            startLeft_ = new Vector2(start_.x + 0.65f, start_.y + 1);
            startRight_ = new Vector2(start_.x + 0.65f, start_.y - 1);

            if (middle2_.y > middle_.y)
            {
                middleLeft_ = new Vector2(middle_.x - 0.65f, middle_.y + 1f);
                middleRight_ = new Vector2(middle_.x + 0.65f, middle_.y - 1f);

                middleLeft2_ = new Vector2(middle2_.x - 0.65f, middle2_.y + 1f);
                middleRight2_ = new Vector2(middle2_.x + 0.65f, middle2_.y - 1f);
            }
            else
            {
                middleLeft_ = new Vector2(middle_.x + 0.65f, middle_.y + 1f);
                middleRight_ = new Vector2(middle_.x - 0.65f, middle_.y - 1f);

                middleLeft2_ = new Vector2(middle2_.x + 0.65f, middle2_.y + 1f);
                middleRight2_ = new Vector2(middle2_.x - 0.65f, middle2_.y - 1f);
            }

            endLeft_ = new Vector2(end_.x - 0.65f, end_.y + 1);
            endRight_ = new Vector2(end_.x - 0.65f, end_.y - 1);
        }

        DrawAccessLine(startLeft_, endLeft_, middleLeft_, middleLeft2_, lineWallTop, distanceIndex_);
        DrawAccessLine(startRight_, endRight_, middleRight_, middleRight2_, lineWallBottom, distanceIndex_);
    }

    IEnumerator AccessRoomLineColEnableFalse(CompositeCollider2D lineComposite_)
    {
        yield return null;
        lineComposite_.enabled = false;
    }
    private void DrawAccessLine(Vector2 start_, Vector2 end_, GameObject lineRenderObj_)
    {
        LineRenderer startLine_ = Instantiate(lineRenderObj_).GetComponent<LineRenderer>();
        startLine_.SetPosition(0, start_);
        startLine_.SetPosition(1, end_);

        startLine_.AddComponent<BoxCollider2D>().isTrigger = true;
        BoxCollider2D lineBoxCollider = startLine_.GetComponent<BoxCollider2D>();

        if (lineRenderObj_ == accessLine)
        {
            lineBoxCollider.usedByComposite = true;
            lineBoxCollider.transform.parent = mapAccessLine.transform;
        }
        else
        {
            startLine_.transform.parent = mapAccessWall.transform;
        }
    }

    private void DrawAccessLine(Vector2 start_, Vector2 end_, Vector2 middle_, Vector2 middle2_, GameObject lineRenderObj_, int distanceIndex_)
    {
        LineRenderer startLine_ = Instantiate(lineRenderObj_).GetComponent<LineRenderer>();
        LineRenderer middleLine_ = Instantiate(lineRenderObj_).GetComponent<LineRenderer>();
        LineRenderer endLine_ = Instantiate(lineRenderObj_).GetComponent<LineRenderer>();

        // 0 -> top to bottom distance
        if (distanceIndex_ == 0)
        {
            startLine_.SetPosition(0, start_);
            startLine_.SetPosition(1, middle_ + new Vector2(0, 0.5f));

            middleLine_.SetPosition(0, middle_);
            middleLine_.SetPosition(1, middle2_);

            endLine_.SetPosition(0, middle2_ - new Vector2(0, 0.5f));
            endLine_.SetPosition(1, end_);  
        }
        // 1 -> right to bottom distance
        else
        {
            startLine_.SetPosition(0, start_);
            startLine_.SetPosition(1, middle_ + new Vector2(0.5f, 0));

            middleLine_.SetPosition(0, middle_);
            middleLine_.SetPosition(1, middle2_);

            endLine_.SetPosition(0, middle2_ - new Vector2(0.5f, 0));
            endLine_.SetPosition(1, end_);
        }

        startLine_.AddComponent<BoxCollider2D>().isTrigger = true;
        middleLine_.AddComponent<BoxCollider2D>().isTrigger = true;
        endLine_.AddComponent<BoxCollider2D>().isTrigger = true;

        if (lineRenderObj_ == accessLine)
        {
            startLine_.GetComponent<BoxCollider2D>().usedByComposite = true;
            middleLine_.GetComponent<BoxCollider2D>().usedByComposite = true;
            endLine_.GetComponent<BoxCollider2D>().usedByComposite = true;
            startLine_.transform.parent = mapAccessLine.transform;
            middleLine_.transform.parent = mapAccessLine.transform;
            endLine_.transform.parent = mapAccessLine.transform;
        }
        else
        {
            startLine_.transform.parent = mapAccessWall.transform;
            middleLine_.transform.parent = mapAccessWall.transform;
            endLine_.transform.parent = mapAccessWall.transform;
        }
    }
}
