using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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

    [SerializeField]
    private List<GameObject> mapPrefabs = new List<GameObject>();
    public GameObject indexCheckObj = default;
    public GameObject mapAccessLine = default;
    public GameObject mapAccessWall = default;

    private const float MINIMUM_DIVIDE_RATE = 0.36f;
    private const float MAXIMUM_DIVIDE_RATE = 0.64f;
    private const int MAXIMUM_DEPTH = 4;

    [SerializeField]
    private GameObject roomLine = default;
    [SerializeField]
    private GameObject roomWall2 = default;
    [SerializeField]
    private GameObject roomWall4 = default;

    private MapNode[] mapNodeArray = new MapNode[MAXIMUM_DEPTH * MAXIMUM_DEPTH];

    // 전체 맵의 크기
    [SerializeField]
    Vector2Int mapSize = default;

    private void Awake()
    {
        MapNode root_ = new MapNode(new RectInt(0, 0, mapSize.x, mapSize.y), 0);
        DivideMap(root_, 0);
        RoomAccess(root_, 0);
    }
    void Start()
    {
        /*MapNode root_ = new MapNode(new RectInt(0, 0, mapSize.x, mapSize.y), 0);
        DivideMap(root_, 0);*/

        AccessZone();
        AccessZoneRoom();

        CompositeCollider2D lineComposite = mapAccessLine.AddComponent<CompositeCollider2D>();
        lineComposite.isTrigger = true;
        lineComposite.geometryType = CompositeCollider2D.GeometryType.Polygons;

        lineComposite.gameObject.SetActive(false);
        lineComposite.gameObject.SetActive(true); 

        /*mapAccessWall.AddComponent<CompositeCollider2D>().isTrigger = true;
        CompositeCollider2D wallComposite = mapAccessWall.GetComponent<CompositeCollider2D>();
        wallComposite.geometryType = CompositeCollider2D.GeometryType.Polygons;

        wallComposite.gameObject.SetActive(false);
        wallComposite.gameObject.SetActive(true);

        ColliderSizeSet();
        wallComposite.isTrigger = false;*/
        ColliderSizeSet();
    }

    private void ColliderSizeSet()
    {
        for(int i = 0; i < mapAccessWall.transform.childCount; i++)
        {
            BoxCollider2D childCollider_ = mapAccessWall.transform.GetChild(i).GetComponent<BoxCollider2D>();
            if(childCollider_.size.x > 0.6)
            {
                childCollider_.size = new Vector2(childCollider_.size.x * 0.8f, 0.3f);
            }
            else
            {
                childCollider_.size = new Vector2(0.3f, childCollider_.size.y * 0.8f);
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
            GameObject indexCheck = Instantiate(indexCheckObj, transform.parent);
            indexCheck.transform.position = (Vector2)nowNode_.nodePosition;
            indexCheck.name = nowNode_.nodeIndex.ToString();
            mapNodeArray[nowNode_.nodeIndex] = nowNode_;

            int index = 0;

            while (true)
            {
                int ran_ = Random.Range(2, mapPrefabs.Count);
                Room room_ = mapPrefabs[ran_].GetComponent<Room>();
                
                if(room_.roomSize.x > nowNode_.nodeRect.width || room_.roomSize.y > nowNode_.nodeRect.height)
                {
                    if (index > 100)
                    {
                        GameObject nodeRoom_ = Instantiate(mapPrefabs[2], transform.parent);
                        nodeRoom_.transform.position = nowNode_.nodePosition;
                        nowNode_.room = room_;

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
                    GameObject nodeRoom_ = Instantiate(room_.gameObject, transform.parent);
                    nodeRoom_.transform.position = nowNode_.nodePosition;
                    nowNode_.room = room_;

                    nowNode_.leftCenter = nowNode_.nodePosition - new Vector2(room_.roomSize.x / 2, 0) * 0.65f;
                    nowNode_.rightCenter = nowNode_.nodePosition + new Vector2(room_.roomSize.x / 2, 0) * 0.65f;
                    nowNode_.topCenter = nowNode_.nodePosition + new Vector2(0, room_.roomSize.y / 2) * 0.65f;
                    nowNode_.bottomCenter = nowNode_.nodePosition - new Vector2(0, room_.roomSize.y / 2) * 0.65f;

                    break;
                }
            }

            if(nowNode_.nodeIndex == MAXIMUM_DEPTH * MAXIMUM_DEPTH - 1)
            {
                for(int i = 0; i < mapNodeArray.Count(); i += 2)
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

                        startLeft_ = new Vector2(start_.x - 1, start_.y + 1.3f);
                        startRight_ = new Vector2(start_.x + 1, start_.y + 1.3f);

                        endLeft_ = new Vector2(end_.x - 1, end_.y - 1.3f);
                        endRight_ = new Vector2(end_.x + 1, end_.y - 1.3f);

                        DrawAccessLine(startLeft_, endLeft_, roomWall2);
                        DrawAccessLine(startRight_, endRight_, roomWall2);
                    }
                    // y 포지션 값이 같은 경우
                    else
                    {
                        start_ = mapNodeArray[i].rightCenter;
                        end_ = mapNodeArray[i + 1].leftCenter;

                        startLeft_ = new Vector2(start_.x + 1.3f, start_.y - 1);
                        startRight_ = new Vector2(start_.x + 1.3f, start_.y + 1);

                        endLeft_ = new Vector2(end_.x - 1.3f, end_.y - 1);
                        endRight_ = new Vector2(end_.x - 1.3f, end_.y + 1);

                        DrawAccessLine(startLeft_, endLeft_, roomWall2);
                        DrawAccessLine(startRight_, endRight_, roomWall2);
                    }

                    DrawAccessLine(start_, end_);
                }


            }

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
    private void SetSpecialRoom()
    {
        List<int> accessRoomIndex_ = new List<int>{ 0, 4, 8, 15};

        int ran_ = Random.Range(0, accessRoomIndex_.Count);
        Vector2 mapCreatePoint_ = default;
        GameObject startMapObj = Instantiate(mapPrefabs[0], transform.parent);
        
        startMap.room = startMapObj.GetComponent<Room>();

        Vector2 start_ = default;
        Vector2 end_ = default;

        Vector2 startLeft_;
        Vector2 endLeft_;
        Vector2 startRight_;
        Vector2 endRight_;

        switch (accessRoomIndex_[ran_])
        {
            case 0:
                mapCreatePoint_ = new Vector2(mapNodeArray[0].leftCenter.x - 26, mapNodeArray[0].nodePosition.y);
                startMapObj.transform.position = mapCreatePoint_;
                startMap.nodePosition = startMapObj.transform.position;
                startMap.rightCenter = startMap.nodePosition + new Vector2(startMap.room.roomSize.x / 2, 0) * 0.65f;
                
                start_ = startMap.rightCenter;
                end_ = mapNodeArray[0].leftCenter;
                break;
            case 4:
                mapCreatePoint_ = new Vector2(mapNodeArray[4].leftCenter.x - 26, mapNodeArray[4].nodePosition.y);
                startMapObj.transform.position = mapCreatePoint_;
                startMap.nodePosition = startMapObj.transform.position;
                startMap.rightCenter = startMap.nodePosition + new Vector2(startMap.room.roomSize.x / 2, 0) * 0.65f;
                
                start_ = mapNodeArray[4].leftCenter;
                end_ = startMap.rightCenter;
                break;
            case 8:
                mapCreatePoint_ = new Vector2(mapNodeArray[8].nodePosition.x, mapNodeArray[8].bottomCenter.y - 26);
                startMapObj.transform.position = mapCreatePoint_;
                startMap.nodePosition = startMapObj.transform.position;
                startMap.topCenter = startMap.nodePosition + new Vector2(0, startMap.room.roomSize.y / 2) * 0.65f;
                 
                start_ = startMap.topCenter;
                end_ = mapNodeArray[8].bottomCenter;
                break;
            case 15:
                mapCreatePoint_ = new Vector2(mapNodeArray[15].nodePosition.x, mapNodeArray[15].topCenter.y + 26);
                startMapObj.transform.position = mapCreatePoint_;
                startMap.nodePosition = startMapObj.transform.position;
                startMap.bottomCenter = startMap.nodePosition - new Vector2(0, startMap.room.roomSize.y / 2) * 0.65f;
                
                start_ = mapNodeArray[15].topCenter;
                end_ = startMap.bottomCenter;
                break;
        }

        startMapObj.GetComponent<StartPoint>().SetPlayer();
        DrawAccessLine(start_, end_);

        // x 포지션 값이 같은 경우
        if (accessRoomIndex_[ran_] == 0 || accessRoomIndex_[ran_] == 4)
        {
            startLeft_ = new Vector2(start_.x + 1.3f, start_.y + 1);
            startRight_ = new Vector2(start_.x + 1.3f, start_.y - 1);

            endLeft_ = new Vector2(end_.x - 1.3f, end_.y + 1);
            endRight_ = new Vector2(end_.x - 1.3f, end_.y - 1);

            DrawAccessLine(startLeft_, endLeft_, roomWall2);
            DrawAccessLine(startRight_, endRight_, roomWall2);
        }
        // y 포지션 값이 같은 경우
        else
        {
            startLeft_ = new Vector2(start_.x - 1, start_.y + 0.65f);
            startRight_ = new Vector2(start_.x + 1, start_.y + 0.65f);

            endLeft_ = new Vector2(end_.x - 1, end_.y - 0.65f);
            endRight_ = new Vector2(end_.x + 1, end_.y - 0.65f);

            Debug.Log(startLeft_);
            Debug.Log(startRight_);
            Debug.Log(endLeft_);
            Debug.Log(endRight_);

            DrawAccessLine(startLeft_, endLeft_, roomWall2);
            DrawAccessLine(startRight_, endRight_, roomWall2);
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
        middle2_ = new Vector2(end_.x, middle_.y);

        DrawAccessLine(start_, end_, middle_, middle2_);
        DrawAccessLineSide(start_, end_, middle_, middle2_, 0);

        // 존1, 존3 연결

        start_ = mapNodeArray[zoneAccessRoomIndex_[0]].rightCenter;
        end_ = mapNodeArray[zoneAccessRoomIndex_[2]].leftCenter;
        middle_ = new Vector2((start_.x + end_.x) / 2, start_.y);
        middle2_ = new Vector2(middle_.x, end_.y);

        

        DrawAccessLine(start_, end_, middle_, middle2_);
        DrawAccessLineSide(start_, end_, middle_, middle2_, 1);

        // 존2, 존4 연결

        start_ = mapNodeArray[zoneAccessRoomIndex_[1]].rightCenter;
        end_ = mapNodeArray[zoneAccessRoomIndex_[3]].leftCenter;
        middle_ = new Vector2((start_.x + end_.x) / 2, start_.y);
        middle2_ = new Vector2(middle_.x, end_.y);


        DrawAccessLine(start_, end_, middle_, middle2_);
        DrawAccessLineSide(start_, end_, middle_, middle2_, 1);

        // 존3, 존4 연결
        start_ = mapNodeArray[zoneAccessRoomIndex_[2]].topCenter;
        end_ = mapNodeArray[zoneAccessRoomIndex_[3]].bottomCenter;
        middle_ = new Vector2(start_.x, (start_.y + end_.y) / 2);
        middle2_ = new Vector2(end_.x, middle_.y);

        DrawAccessLine(start_, end_, middle_, middle2_);
        DrawAccessLineSide(start_, end_, middle_, middle2_, 0);
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
                DrawAccessLineSide(start_, end_, middle_, middle2_, 1);
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
                    DrawAccessLineSide(start_, end_, middle_, middle2_, 1);
                }
                else
                {
                    DrawAccessLineSide(start_, end_, middle_, middle2_, 0);
                }
                
            }

            DrawAccessLine(start_, end_, middle_, middle2_);
        }

        SetSpecialRoom();
    }
    private void DrawAccessLine(Vector2 start_, Vector2 end_)
    {
        LineRenderer lineRenderer_ = Instantiate(roomLine).GetComponent<LineRenderer>();
        lineRenderer_.SetPosition(0, start_);
        lineRenderer_.SetPosition(1, end_);

        lineRenderer_.AddComponent<BoxCollider2D>();
        BoxCollider2D lineBoxCollider = lineRenderer_.GetComponent<BoxCollider2D>();

        lineBoxCollider.isTrigger = true;
        lineBoxCollider.usedByComposite = true;
        lineBoxCollider.transform.parent = mapAccessLine.transform;
    }

    private void DrawAccessLine(Vector2 start_, Vector2 end_, GameObject lineRenderObj_)
    {
        LineRenderer lineRenderer_ = Instantiate(lineRenderObj_).GetComponent<LineRenderer>();
        lineRenderer_.SetPosition(0, start_);
        lineRenderer_.SetPosition(1, end_);

        lineRenderer_.transform.parent = mapAccessWall.transform;

        lineRenderer_.AddComponent<BoxCollider2D>().isTrigger = true;
        BoxCollider2D lineBoxCollider = lineRenderer_.GetComponent<BoxCollider2D>();
        lineBoxCollider.usedByComposite = true;
    }
    private void DrawAccessLine(Vector2 start_, Vector2 end_, Vector2 middle_, Vector2 middle2_)
    {
        LineRenderer lineRenderer_ = Instantiate(roomLine).GetComponent<LineRenderer>();
        LineRenderer lineRenderer2_ = Instantiate(roomLine).GetComponent<LineRenderer>();
        LineRenderer lineRenderer3_ = Instantiate(roomLine).GetComponent<LineRenderer>();

        lineRenderer_.SetPosition(0, start_);
        lineRenderer_.SetPosition(1, middle_);

        lineRenderer2_.SetPosition(0, middle_);
        lineRenderer2_.SetPosition(1, middle2_);

        lineRenderer3_.SetPosition(0, middle2_);
        lineRenderer3_.SetPosition(1, end_);

        lineRenderer_.AddComponent<BoxCollider2D>().isTrigger = true;
        lineRenderer_.transform.parent = mapAccessLine.transform;
        lineRenderer2_.AddComponent<BoxCollider2D>().isTrigger = true;
        lineRenderer2_.transform.parent = mapAccessLine.transform;
        lineRenderer3_.AddComponent<BoxCollider2D>().isTrigger = true;
        lineRenderer3_.transform.parent = mapAccessLine.transform;
    }
    private void DrawAccessLine(Vector2 start_, Vector2 end_, Vector2 middle_, Vector2 middle2_, GameObject lineRenderObj_)
    {
        LineRenderer lineRenderer_ = Instantiate(lineRenderObj_).GetComponent<LineRenderer>();
        LineRenderer lineRenderer2_ = Instantiate(lineRenderObj_).GetComponent<LineRenderer>();
        LineRenderer lineRenderer3_ = Instantiate(lineRenderObj_).GetComponent<LineRenderer>();

        lineRenderer_.SetPosition(0, start_);
        lineRenderer_.SetPosition(1, middle_);

        lineRenderer2_.SetPosition(0, middle_);
        lineRenderer2_.SetPosition(1, middle2_);

        lineRenderer3_.SetPosition(0, middle2_);
        lineRenderer3_.SetPosition(1, end_);

        lineRenderer_.AddComponent<BoxCollider2D>().isTrigger = true;
        lineRenderer_.GetComponent<BoxCollider2D>().usedByComposite = true;
        lineRenderer_.transform.parent = mapAccessWall.transform;

        lineRenderer2_.AddComponent<BoxCollider2D>().isTrigger = true;
        lineRenderer2_.GetComponent<BoxCollider2D>().usedByComposite = true;
        lineRenderer2_.transform.parent = mapAccessWall.transform;

        lineRenderer3_.AddComponent<BoxCollider2D>().isTrigger = true;
        lineRenderer3_.GetComponent<BoxCollider2D>().usedByComposite = true;
        lineRenderer3_.transform.parent = mapAccessWall.transform;
    }

    private void DrawAccessLineSide(Vector2 start_, Vector2 end_, Vector2 middle_, Vector2 middle2_, int distanceIndex_)
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
            startLeft_ = new Vector2(start_.x - 1, start_.y + 1.3f);
            startRight_ = new Vector2(start_.x + 1, start_.y + 1.3f);

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

            endLeft_ = new Vector2(end_.x - 1, end_.y - 1.3f);
            endRight_ = new Vector2(end_.x + 1, end_.y - 1.3f);
        }
        else
        {
            startLeft_ = new Vector2(start_.x + 1.2f, start_.y + 1);
            startRight_ = new Vector2(start_.x + 1.2f, start_.y - 1);

            if (middle2_.y > middle_.y)
            {
                middleLeft_ = new Vector2(middle_.x - 1.3f, middle_.y + 1f);
                middleRight_ = new Vector2(middle_.x + 1.3f, middle_.y - 1f);

                middleLeft2_ = new Vector2(middle2_.x - 1.3f, middle2_.y + 1f);
                middleRight2_ = new Vector2(middle2_.x + 1.3f, middle2_.y - 1f);
            }
            else
            {
                middleLeft_ = new Vector2(middle_.x + 1.3f, middle_.y + 1f);
                middleRight_ = new Vector2(middle_.x - 1.3f, middle_.y - 1f);

                middleLeft2_ = new Vector2(middle2_.x + 1.3f, middle2_.y + 1f);
                middleRight2_ = new Vector2(middle2_.x - 1.3f, middle2_.y - 1f);
            }

            endLeft_ = new Vector2(end_.x - 1.2f, end_.y + 1);
            endRight_ = new Vector2(end_.x - 1.2f, end_.y - 1);
        }

        DrawAccessLine(startLeft_, endLeft_, middleLeft_, middleLeft2_, roomWall2);
        DrawAccessLine(startRight_, endRight_, middleRight_, middleRight2_, roomWall2);
    }
}
