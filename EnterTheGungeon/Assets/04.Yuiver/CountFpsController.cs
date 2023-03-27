using SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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
        DataManager.Instance.Create();
        SoundManager.Instance.Create();
        DontDestroyOnLoad(this.gameObject);
        //waitOneSecond의 초기값 설정
        waitOneSecond = 1;
    }
    private void Start()
    {
        OptionState firstLoadData = default;
        
        firstLoadData = DataManager.Instance.LoadOptionGameData();
        SoundManager.Instance.SetVolume(Sound.Bgm, firstLoadData.MusicVolume);
        SoundManager.Instance.SetVolume(Sound.SFX, firstLoadData.SFXVolume);
        SoundManager.Instance.SetVolume(Sound.UI_SFX, firstLoadData.UIVolume);


        DataManager.Instance.SetCursor(firstLoadData.mouseCursor);
        SoundManager.Instance.Play("TestSound", Sound.Bgm);
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
