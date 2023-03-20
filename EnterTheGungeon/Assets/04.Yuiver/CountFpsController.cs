using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountFpsController : MonoBehaviour
{
    public int fontSize = 30;
    public Color color = new Color(1f,1f,1f,1f);
    public float width = default;
    public float height = default;

    private float waitOneSecond = default;
    private float fps = default;

    private void Awake()
    {
        SoundManager.Instance.Create();
        DontDestroyOnLoad(this.gameObject);
        //waitOneSecond의 초기값 설정
        waitOneSecond = 1;
    }
    private void Start()
    {
        SoundManager.Instance.Play("TestSound", Sound.SFX);
    }

    void OnGUI()
    {
        if (waitOneSecond >= 1)
        {
            //1초마다 fps값을 계산
            fps = 1.0f / Time.deltaTime;
            waitOneSecond = 0;
        }
        Rect position = new Rect(width, height, Screen.width, Screen.height);
        string text = string.Format("{0:N2} FPS", fps);
        GUIStyle style = new GUIStyle();
        style.fontSize = fontSize;
        style.normal.textColor = color;

        GUI.Label(position, text, style);
        waitOneSecond += Time.deltaTime;
    }
}
