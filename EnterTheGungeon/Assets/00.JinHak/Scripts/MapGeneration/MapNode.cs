using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    public MapNode leftNode = default;
    public MapNode rightNode = default;
    public MapNode parentNode = default;
    public RectInt nodeRect = default; // 현재 본인의 RECT 크기
    public Vector2 Center
    {
        get
        {
            return new Vector2Int(nodeRect.x + nodeRect.width / 2, nodeRect.y + nodeRect.height / 2);
        }
        private set { return; }
    }


    public MapNode(RectInt nodeRect_)
    {
        this.nodeRect = nodeRect_;
    }
}
