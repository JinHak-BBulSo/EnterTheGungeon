using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private const float MINIMUM_DIVIDE_RATE = 0.3f;
    private const float MAXIMUM_DIVIDE_RATE = 0.7f;
    private const int MAXIMUM_DEPTH = 3;

    [SerializeField]
    private GameObject line = default; // 나누어진 공간을 보여주기 위한 라인렌더러를 가진 오브젝트
    // 전체 사이즈 맵 -> 나누기 전 Root Map
    [SerializeField]
    private GameObject maxSizeMap = default;
    private GameObject roomLine = default; // 라인렌더러를 통해 방의 크기를 보여주는 선
    
    // 전체 맵의 크기
    [SerializeField]
    Vector2Int mapSize = default;   

    void Start()
    {
        MapNode root_ = new MapNode(new RectInt(0, 0, mapSize.x, mapSize.y));
        DrawMap(0, 0);
        DivideMap(root_, 0);
    }

    // [KJH] 2023-03-15
    // @brief 전체 맵의 윤곽선 그리는 함수
    // @param sizeX : 맵의 X 크기
    // @param sizeY : 맵의 Y 크기
    private void DrawMap(int sizeX_, int sizeY_)
    {
#if DEBUG_MODE
        LineRenderer lineRenderer = Instantiate(maxSizeMap).GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, new Vector2(sizeX_, sizeY_) - mapSize / 2); // 좌측하단
        lineRenderer.SetPosition(1, new Vector2(sizeX_ + mapSize.x, sizeY_) - mapSize / 2); // 우측하단
        lineRenderer.SetPosition(2, new Vector2(sizeX_ + mapSize.x, sizeY_ + mapSize.y) - mapSize / 2); // 우측 상단
        lineRenderer.SetPosition(3, new Vector2(sizeX_, sizeY_ + mapSize.y) - mapSize / 2); // 좌측 상단
#endif 
    } // DEBUG_MODE

    // [KJH] 2023-03-15
    // @brief 전체 맵을 랜덤하게 분할하는 함수
    // @param nowNode_ 나눌 대상이 대상이 되는 현재 노드
    // @param h_ 나누어지는 최대 높이
    private void DivideMap(MapNode nowNode_, int h_)
    {
        if (h_ == MAXIMUM_DEPTH) return;

        // 가로와 세로 중에서 긴 값을 저장
        int maxLength_ = Mathf.Max(nowNode_.nodeRect.width, nowNode_.nodeRect.height);

        // 가로와 세로 중 긴 값을 기준으로 분할
        int split_ = Mathf.RoundToInt(Random.Range(maxLength_ * MINIMUM_DIVIDE_RATE, maxLength_ * MAXIMUM_DIVIDE_RATE));

        // 가로가 더 긴 경우
        if (nowNode_.nodeRect.width >= nowNode_.nodeRect.height)
        {
            nowNode_.leftNode = new MapNode(new RectInt(nowNode_.nodeRect.x, nowNode_.nodeRect.y, split_, nowNode_.nodeRect.height));
            nowNode_.rightNode = new MapNode(new RectInt(nowNode_.nodeRect.x + split_, nowNode_.nodeRect.y, nowNode_.nodeRect.width - split_, nowNode_.nodeRect.height));

            Vector2 start_ = new Vector2(nowNode_.nodeRect.x + split_, nowNode_.nodeRect.y);
            Vector2 end_ = new Vector2(nowNode_.nodeRect.x + split_, nowNode_.nodeRect.y + nowNode_.nodeRect.height);

            DrawLine(start_, end_);
        }

        // 세로가 더 긴 경우
        else
        {
            nowNode_.leftNode = new MapNode(new RectInt(nowNode_.nodeRect.x, nowNode_.nodeRect.y, nowNode_.nodeRect.width, split_));
            nowNode_.rightNode = new MapNode(new RectInt(nowNode_.nodeRect.x, nowNode_.nodeRect.y + split_, nowNode_.nodeRect.width, nowNode_.nodeRect.height - split_));

            Vector2 start_ = new Vector2(nowNode_.nodeRect.x, nowNode_.nodeRect.y + split_);
            Vector2 end_ = new Vector2(nowNode_.nodeRect.x + nowNode_.nodeRect.width, nowNode_.nodeRect.y + split_);

            DrawLine(start_, end_);
        }

        nowNode_.leftNode.parentNode = nowNode_;
        nowNode_.rightNode.parentNode = nowNode_;

        DivideMap(nowNode_.leftNode, h_ + 1);
        DivideMap(nowNode_.rightNode, h_ + 1);
    }

    // [KJH] 2023-03-15
    // @brief 방의 윤곽선을 그리는 함수
    // @param start 시작점
    // @param end 끝점
    private void DrawLine(Vector2 start_, Vector2 end_)
    {
#if DEBUG_MODE
        LineRenderer lineRenderer = Instantiate(line).GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, start_ - mapSize / 2);
        lineRenderer.SetPosition(1, end_ - mapSize / 2);
#endif
    }
}
