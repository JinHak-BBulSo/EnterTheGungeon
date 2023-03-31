using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCamController : MoveCamera2D
{
    private Camera miniCam = default;

    private void Awake()
    {
        miniCam = GetComponent<Camera>();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void LateUpdate()
    {
        if (!PlayerManager.Instance.playerCamera.isBossIntro)
        {
            // 플레이어 위치 값
            Vector3 targetPos_ = new Vector3(target.transform.position.x,
                target.transform.position.y, -20f);

            // [Junil, YHJ] 미니맵의 위치는 플레이어의 윗 쪽에 위치한다.
            gameObject.transform.position = Vector3.Lerp(transform.position,
                targetPos_, SPEED_CAMERA * Time.deltaTime);
        }
    }

    private void Update()
    {
        DisplayMiniCam();
    }

    private void DisplayMiniCam()
    {
        if (Input.GetKey(KeyCode.Tab) && UIController.boolGamePause == false)
        {
            miniCam.enabled = false;
        }
        else
        {
            miniCam.enabled = true;
        }
    }
}
