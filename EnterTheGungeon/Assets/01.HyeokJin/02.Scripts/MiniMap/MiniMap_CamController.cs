using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniMap_CamController : MoveCamera2D
{
    public override void Start()
    {
        base.Start();
    }

    public override void LateUpdate()
    {
        // 플레이어 위치 값
        Vector3 targetPos_ = new Vector3(0f, 0f, -20f);

        // [Junil, YHJ] 미니맵의 위치는 플레이어의 윗 쪽에 위치한다.
        gameObject.transform.position = Vector3.Lerp(transform.position,
            targetPos_, SPEED_CAMERA * Time.deltaTime);
    }
}
