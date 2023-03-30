using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera2DMove : MoveCamera2D
{

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void LateUpdate()
    {
        // 플레이어 위치 값
        Vector3 targetPos_ = new Vector3(target.transform.position.x,
            target.transform.position.y, -20f);
            
        // [Junil, YHJ] 미니맵의 위치는 플레이어의 윗 쪽에 위치한다.
        gameObject.transform.position = Vector3.Lerp(transform.position,
            targetPos_, SPEED_CAMERA * Time.deltaTime);


    }
}