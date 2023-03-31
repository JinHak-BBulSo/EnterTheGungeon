using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveCamera2D : MonoBehaviour
{
    // { [Junil] fix PlayerCamera

    public const float SPEED_CAMERA = 4f;
    
    public float cameraHeight = default;
    public float cameraWidth = default;
    
    // 카메라가 쫒아 다닐 대상
    public static GameObject target = default;


    public float exceptionRangeVal = default;

    public bool isPlayerDie = false;
    public bool isFocus = false;

    // [KJH] ADD
    public bool isBossIntro = false;

    // Start is called before the first frame update
    public virtual void Start()
    {
        PlayerManager.Instance.playerCamera = this;
        GameObject gameObjs_ = GameObject.Find("GameObjs");

        gameObject.transform.parent = gameObjs_.transform;

        exceptionRangeVal = 0.35f;

        
        target = gameObjs_.FindChildObj("PlayerObjs").GetChildrenObjs()[0];

        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Screen.width / Screen.height;

    }


    public virtual void LateUpdate()
    {
        if(isPlayerDie)
        {
            transform.position = target.transform.position;
            isFocus = true;
        }
        else if (isBossIntro)
        {
            gameObject.transform.position = Vector3.Lerp(transform.position, target.transform.position, SPEED_CAMERA * Time.deltaTime);
        }
        else
        {

            // 플레이어 위치 값
            Vector3 targetPos_ = target.transform.position;
            // 마우스 위치 값
            Vector3 mousePos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);


            // 마우스 위치에 대한 최대, 최소 값을 구한 값
            float clampMinX = Mathf.Clamp(mousePos_.x,
                targetPos_.x - (cameraWidth * 0.5f), targetPos_.x + (cameraWidth * 0.5f));

            float clampMinY = Mathf.Clamp(mousePos_.y,
                targetPos_.y - (cameraHeight * 0.5f), targetPos_.y + (cameraHeight * 0.5f));


            // 새로운 카메라 위치를 잡아주기 위한 값들
            Vector3 testCamera_ = new Vector3(clampMinX, clampMinY, -10f);
            Vector3 targetCameraPos_ = new Vector3(targetPos_.x, targetPos_.y, -10f);


            // 예외 범위를 지정하여 카메라 위치를 잡는 조건
            if ((targetPos_.x - (cameraWidth * exceptionRangeVal) <= clampMinX && clampMinX <= targetPos_.x + (cameraWidth * exceptionRangeVal)) &&
                (targetPos_.y - (cameraHeight * exceptionRangeVal) <= clampMinY && clampMinY <= targetPos_.y + (cameraHeight * exceptionRangeVal)))
            {
                // 부드러운 움직임을 위해서 Vector3.Lerp 사용
                gameObject.transform.position = Vector3.Lerp(transform.position, targetCameraPos_, SPEED_CAMERA * Time.deltaTime);
            }   // if : 예외 범위일 때는 플레이어 위치를 잡는 조건
            else
            {
                gameObject.transform.position = Vector3.Lerp(transform.position, testCamera_, SPEED_CAMERA * Time.deltaTime);

            }   // else : 그 외는 마우스 위치를 잡는 조건
        }



    }

    // } [Junil] fix PlayerCamera



}
