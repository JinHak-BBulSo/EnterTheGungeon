using System.Collections;
using System.Collections.Generic;
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
        public int nodeIndex = 0;

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
            Debug.Log(nodeIndex);
        }
    }

    [SerializeField]
    private List<GameObject> mapPrefabs = new List<GameObject>();

    private const float MINIMUM_DIVIDE_RATE = 0.36f;
    private const float MAXIMUM_DIVIDE_RATE = 0.64f;
    private const int MAXIMUM_DEPTH = 4;

    [SerializeField]
    private GameObject line = default; // 나누어진 공간을 보여주기 위한 라인렌더러를 가진 오브젝트
    [SerializeField]
    private GameObject roomLine = default;
    [SerializeField]
    private GameObject roomTIle1 = default;
    [SerializeField]
    private GameObject roomTIle2 = default;

    // 전체 사이즈 맵 -> 나누기 전 Root Map
    [SerializeField]
    private GameObject maxSizeMap = default;

    // 전체 맵의 크기
    [SerializeField]
    Vector2Int mapSize = default;

    void Start()
    {
        MapNode root_ = new MapNode(new RectInt(0, 0, mapSize.x, mapSize.y), 0);
        DrawMap(0, 0);
        DivideMap(root_, 0);
        RoomAccess(root_, 0);
    }

    // [KJH] 2023-03-15
    // @brief 전체 맵의 윤곽선 그리는 함수
    // @param sizeX : 맵의 X 크기
    // @param sizeY : 맵의 Y 크기
    private void DrawMap(int sizeX_, int sizeY_)
    {
#if DEBUG_MODE
        LineRenderer lineRenderer_ = Instantiate(maxSizeMap).GetComponent<LineRenderer>();
        lineRenderer_.SetPosition(0, new Vector2(sizeX_, sizeY_) - mapSize / 2); // 좌측하단
        lineRenderer_.SetPosition(1, new Vector2(sizeX_ + mapSize.x, sizeY_) - mapSize / 2); // 우측하단
        lineRenderer_.SetPosition(2, new Vector2(sizeX_ + mapSize.x, sizeY_ + mapSize.y) - mapSize / 2); // 우측 상단
        lineRenderer_.SetPosition(3, new Vector2(sizeX_, sizeY_ + mapSize.y) - mapSize / 2); // 좌측 상단
#endif 
    } // DEBUG_MODE

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
                nowNode_.nodeRect.height), nowNode_.nodeIndex * 2 + 1);
            nowNode_.rightNode = new MapNode(new RectInt(nowNode_.nodeRect.x + split_, nowNode_.nodeRect.y,
                nowNode_.nodeRect.width - split_, nowNode_.nodeRect.height), nowNode_.nodeIndex * 2 + 2);

            Vector2 start_ = new Vector2(nowNode_.nodeRect.x + split_, nowNode_.nodeRect.y);
            Vector2 end_ = new Vector2(nowNode_.nodeRect.x + split_, nowNode_.nodeRect.y + nowNode_.nodeRect.height);

            DrawLine(start_, end_);
        }

        // 세로가 더 긴 경우
        else
        {
            nowNode_.leftNode = new MapNode(new RectInt(nowNode_.nodeRect.x, nowNode_.nodeRect.y, 
                nowNode_.nodeRect.width, split_), nowNode_.nodeIndex * 2 + 1);
            nowNode_.rightNode = new MapNode(new RectInt(nowNode_.nodeRect.x, nowNode_.nodeRect.y + split_,
                nowNode_.nodeRect.width, nowNode_.nodeRect.height - split_), nowNode_.nodeIndex * 2 + 2);

            Vector2 start_ = new Vector2(nowNode_.nodeRect.x, nowNode_.nodeRect.y + split_);
            Vector2 end_ = new Vector2(nowNode_.nodeRect.x + nowNode_.nodeRect.width, nowNode_.nodeRect.y + split_);

            DrawLine(start_, end_);
        }

        nowNode_.leftNode.parentNode = nowNode_;
        nowNode_.rightNode.parentNode = nowNode_;

        DivideMap(nowNode_.leftNode, height_ + 1);
        DivideMap(nowNode_.rightNode, height_ + 1);
    }

    // [KJH] 2023-03-15
    // @brief 방의 윤곽선을 그리는 함수
    // @param start 시작점
    // @param end 끝점
    private void DrawLine(Vector2 start_, Vector2 end_)
    {
#if DEBUG_MODE
        LineRenderer lineRenderer_ = Instantiate(line).GetComponent<LineRenderer>();
        lineRenderer_.SetPosition(0, start_ - mapSize / 2);
        lineRenderer_.SetPosition(1, end_ - mapSize / 2);
#endif
    }

    private void DrawRoomLine(Vector2 start_, Vector2 end_)
    {
        LineRenderer lineRenderer_ = Instantiate(roomLine).GetComponent<LineRenderer>();
        lineRenderer_.SetPosition(0, start_ - mapSize / 2);
        lineRenderer_.SetPosition(1, end_ - mapSize / 2);
    }

    private void DrawRoomLine(Vector2 start_, Vector2 end_, GameObject lineRenderObj_)
    {
        LineRenderer lineRenderer_ = Instantiate(lineRenderObj_).GetComponent<LineRenderer>();
        lineRenderer_.SetPosition(0, start_ - mapSize / 2);
        lineRenderer_.SetPosition(1, end_ - mapSize / 2);
    }

    // [KJH] 2023-03-16
    // @brief 방 간의 연결을 보여주는 함수
    // @param nowNode 현재 노드
    // @param h 트리 구조에서의 높이
    private void RoomAccess(MapNode nowNode_, int height_)
    {
        if (height_ == MAXIMUM_DEPTH) return; // 트리의 최대 높이와 같아 리프 노드인 경우

        Vector2Int leftNodeCenter = nowNode_.leftNode.Center;
        Vector2Int rightNodeCenter = nowNode_.rightNode.Center;

        DrawRoomLine(new Vector2(leftNodeCenter.x, leftNodeCenter.y), new Vector2(rightNodeCenter.x, leftNodeCenter.y));
        DrawRoomLine(new Vector2(rightNodeCenter.x, leftNodeCenter.y), new Vector2(rightNodeCenter.x, rightNodeCenter.y));

        DrawRoomLine(new Vector2(leftNodeCenter.x, leftNodeCenter.y + 1), new Vector2(rightNodeCenter.x, leftNodeCenter.y + 1), roomTIle1);
        DrawRoomLine(new Vector2(leftNodeCenter.x, leftNodeCenter.y - 1), new Vector2(rightNodeCenter.x, leftNodeCenter.y - 1), roomTIle2);

        DrawRoomLine(new Vector2(rightNodeCenter.x + 1, leftNodeCenter.y), new Vector2(rightNodeCenter.x + 1, rightNodeCenter.y), roomTIle1);
        DrawRoomLine(new Vector2(rightNodeCenter.x - 1, leftNodeCenter.y), new Vector2(rightNodeCenter.x - 1, rightNodeCenter.y), roomTIle2);

        RoomAccess(nowNode_.leftNode, height_ + 1);
        RoomAccess(nowNode_.rightNode, height_ + 1);
    }
}
