using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Canvas rotateSort = default;

    // Start is called before the first frame update
    void Start()
    {
        rotateSort = gameObject.GetComponentMust<Canvas>();

        rotateSort.sortingLayerName = "Player";

        // 플레이어 본체는 Player 의 1 값 고정이다
        rotateSort.sortingOrder = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
