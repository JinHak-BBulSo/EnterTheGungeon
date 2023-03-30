using SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이 클래스는 UI의 동작체크를 위해 추가한 임시 코드입니다.
/// 혹시 이 코드를 컴포넌트로 가지고 있어서 오류가 발생한다면 코드를 삭제해주세요.
/// </summary>
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
        //옵션 세이브를 체크하기위해서 데이터 로드 관련코드를 테스트 했습니다.
        OptionState firstLoadData = default;
        
        firstLoadData = DataManager.Instance.LoadOptionGameData();
        SoundManager.Instance.SetVolume(Sound.Bgm, firstLoadData.MusicVolume);
        SoundManager.Instance.SetVolume(Sound.SFX, firstLoadData.SFXVolume);
        SoundManager.Instance.SetVolume(Sound.UI_SFX, firstLoadData.UIVolume);

        Screen.fullScreen = firstLoadData.fullScreenOn;
        DataManager.Instance.SetCursor(firstLoadData.mouseCursor);
        SoundManager.Instance.Play("TestSound", Sound.Bgm);
    }

    //ONGUI를 사용해서 프레임을 그냥 화면에 강제로 띄움
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
