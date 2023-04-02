using SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이 클래스는 게임이 시작하는 순간 사용자가 저장해둔 UI세팅을 불러오는 역할을 합니다.
/// 혹시 로드되지 않은 데이터가 있다면 로드해주면 됩니다.
/// </summary>
public class UIDataInstance : MonoBehaviour
{
    private void Start()
    {
        OptionState firstLoadData = default;    //지역변수로 한번 사용하고 버릴 세이브 데이터를 로드합니다.
        
        firstLoadData = DataManager.Instance.LoadOptionGameData();
        SoundManager.Instance.SetVolume(Sound.Bgm, firstLoadData.MusicVolume);  //사용자가 저장해둔 볼륨으로 BGM볼륨을 변경
        SoundManager.Instance.SetVolume(Sound.SFX, firstLoadData.SFXVolume);    //사용자가 저장해둔 볼륨으로 SFX볼륨을 변경
        SoundManager.Instance.SetVolume(Sound.UI_SFX, firstLoadData.UIVolume);  //사용자가 저장해둔 볼륨으로 UI볼륨을 변경

        Screen.fullScreen = firstLoadData.fullScreenOn; //사용자 세팅에 맞게 화면이 전체화면인지 창모드인지 결정해줌
        DataManager.Instance.SetCursor(firstLoadData.mouseCursor);  //사용자가 지정해둔 마우스 커서로 마우스 커서를 변경해줌

        //이곳에 BGM을 넣어주면 됩니다.
        //SoundManager.Instance.Play("TestSound", Sound.Bgm);
    }
}
