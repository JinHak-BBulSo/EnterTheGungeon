using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveCamera2D : MonoBehaviour
{
    /// @param Transform target : 카메라가 쫒아 다닐 대상
    public Transform target = default;


    public float speedCamera = default;

    public Vector2 center = default;
    public Vector2 size = default;


    [SerializeField]
    private float cameraHeight = default;
    private float cameraWidth = default;


    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObjs_ = GFunc.GetRootObj("GameObjs");


        //Transform targetObjs_ = gameObjs_.FindChildObj("PlayerMarineObjs").transform.GetChild(0);
        target = gameObjs_.FindChildObj("PlayerMarineObjs").transform.GetChild(0);

        speedCamera = 10f;

        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Screen.width / Screen.height;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, speedCamera);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);



        /// @param float clampX : 카메라 이동 범위 제한
        float largeX = size.x * 0.5f - cameraWidth;
        float clampX = Mathf.Clamp(transform.position.x,
            -largeX + center.x, largeX + center.x);


        /// @param float clampY : 카메라 이동 범위 제한
        float largeY = size.y * 0.5f - cameraHeight;
        float clampY = Mathf.Clamp(transform.position.y,
            -largeY + center.y, largeY + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }



    /// @brief 카메라 범위를 보여줄, 기즈모를 보여주는 함수
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }




}
