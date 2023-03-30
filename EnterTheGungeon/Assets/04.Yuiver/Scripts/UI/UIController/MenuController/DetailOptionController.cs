using SaveData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 세부옵션을 컨트롤 하는 스크립트입니다.
/// </summary>
public class DetailOptionController : MonoBehaviour
{
    // Audio 옵션안의 슬라이더 인스펙터에서 비어있다면 넣어주세요.
    [SerializeField]
    private Slider bgmSlider;
    [SerializeField]
    private Slider sfxSlider;
    [SerializeField]
    private Slider uiSfxSlider;

    private OptionState loadOptionData = default;   //현재 옵션값을 메뉴에 적용하기 위한 커스텀 클래스, 이 클래스는 옵션 저장정보를 가지고 있습니다.
    private int currentResolutionIndex = 0; //해상도 설정의 인덱스값입니다.

    public Toggle fullScreenToggle = default;   //전체화면 토글입니다. 비어있다면 인스펙터에서 넣어주세요
    public TMP_Dropdown resolutionSetting = default;    //해상도를 선택하는 Dropdown입니다. 비어있다면 인스펙터에서 넣어주세요.
    public int seclectMouse = default;  //현재 선택중인 마우스 커서를 결정하는 인덱스

    public struct ResolutionOption  //해상도 옵션을 한번에 결정하기위한 구조체입니다.
    {
        public string name;
        public int width;
        public int height;

        public ResolutionOption(string name, int width, int height)
        {
            this.name = name;
            this.width = width;
            this.height = height;
        }
    }

    private ResolutionOption[] resolutionOptions = new ResolutionOption[]   //16:9의 비율로 해상도를 고정하기 위한 구조체 배열
{
        new ResolutionOption("1280x720 (HD)", 1280, 720),
        new ResolutionOption("1366x768 (HD+)", 1366, 768),
        new ResolutionOption("1600x900 (HD+)", 1600, 900),
        new ResolutionOption("1920x1080 (FHD)", 1920, 1080),
        new ResolutionOption("2560x1440 (QHD)", 2560, 1440),
        new ResolutionOption("3840x2160 (UHD 4K)", 3840, 2160),
};

    private void Start()
    {
        //전체 화면인지 확인하기 위한
        fullScreenToggle.onValueChanged.AddListener(FullScreenIsOn);
    }
    //세부 옵션UI를 SetActive할때 마다 설정값을 초기화하기 위해 OnEnable에서 실행하는 코드
    private void OnEnable()
    {
        ResetDropdown();    //Dropdown을 리셋해준다.
        fullScreenToggle.isOn = Screen.fullScreen;  //현재 전체화면인지 유니티로 확인해서 토글의 상태를 변경해준다. 사용자의 Alt+Enter에 대응하기 위해서 따로 만들었다.
        loadOptionData = DataManager.Instance.LoadOptionGameData(); //저장된 옵션을 불러온다.
        seclectMouse = loadOptionData.mouseCursor;  //현재 선택중인 마우스를 int로 가져온다.
        OnBgmVolumeChanged(loadOptionData.MusicVolume); 
        OnSFXVolumeChanged(loadOptionData.SFXVolume);
        OnUIVolumeChanged(loadOptionData.UIVolume); //소리 설정을 업데이트 해준다.
        InitializeSliders();    //소리 설정을 UI스크롤바에 적용해준다.
    }
    #region GamePlayOption
    //! 오른쪽 화살표 모양의 커서 변경 버튼을 누른다.
    public void CursorChangeRight()
    {
        if (seclectMouse == 5)
        {
            seclectMouse = 0;
            DataManager.Instance.SetCursor(seclectMouse);
            loadOptionData.mouseCursor = seclectMouse;
        }
        else 
        {
            seclectMouse++;
            DataManager.Instance.SetCursor(seclectMouse);
            loadOptionData.mouseCursor = seclectMouse;
        }
    }
    //! 왼쪽 화살표 모양의 커서 변경 버튼을 누른다.
    public void CursorChangeLeft()
    {
        if (seclectMouse == 0)
        {
            seclectMouse = 5;
            DataManager.Instance.SetCursor(seclectMouse);
            loadOptionData.mouseCursor = seclectMouse;
        }
        else
        {
            seclectMouse--;
            DataManager.Instance.SetCursor(seclectMouse);
            loadOptionData.mouseCursor = seclectMouse;
        }
    }
    #endregion

    #region GraphicOption
    //! 매개변수를 받아서 화면을 전체 화면으로 만들어 주는 함수
    public void FullScreenIsOn(bool isOn)
    {
        Screen.fullScreen = isOn;
        loadOptionData.fullScreenOn = isOn;
    }
    //! 해상도 변경 Dropdown을 초기화와 동시에 자동으로 지정해둔 구조체의 개수만큼 생성해주는 함수
    public void ResetDropdown()
    {
        //Dropdown 초기화
        resolutionSetting.ClearOptions();
        foreach (ResolutionOption option in resolutionOptions)
        {
            resolutionSetting.options.Add(new TMP_Dropdown.OptionData(option.name));
        }

        // Dropdown의 초기 선택값 설정
        for (int i = 0; i < resolutionOptions.Length; i++)
        {
            if (Screen.width == resolutionOptions[i].width &&
                Screen.height == resolutionOptions[i].height)
            {
                currentResolutionIndex = i;
                break;
            }
        }

        // Dropdown의 OnValueChanged 이벤트에 메서드 연결
        resolutionSetting.onValueChanged.AddListener(OnResolutionChanged);
        resolutionSetting.value = currentResolutionIndex;
    }

    // Dropdown의 값이 변경되면 호출되는 메서드
    private void OnResolutionChanged(int index)
    {
        ResolutionOption resolution = resolutionOptions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    #endregion

    #region SoundOption
    //! Audio 설정안에 존재하는 슬라이더 바를 현재 사운드 볼륨으로 변경해주는 함수
    private void InitializeSliders()
    {
        // BGM 슬라이더 초기화
        bgmSlider.value = loadOptionData.MusicVolume;
        bgmSlider.onValueChanged.AddListener(OnBgmVolumeChanged);

        // SFX 슬라이더 초기화
        sfxSlider.value = loadOptionData.SFXVolume;
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);

        // UI SFX 슬라이더 초기화
        uiSfxSlider.value = loadOptionData.UIVolume;
        uiSfxSlider.onValueChanged.AddListener(OnUIVolumeChanged);
    }
    // BGM볼륨을 바꿔주는 함수
    private void OnBgmVolumeChanged(float volume)
    {
        SoundManager.Instance.SetVolume(Sound.Bgm, volume);
        loadOptionData.MusicVolume = volume;
    }
    // SFX볼륨을 바꿔주는 함수
    private void OnSFXVolumeChanged(float volume)
    {
        SoundManager.Instance.SetVolume(Sound.SFX, volume);
        loadOptionData.SFXVolume = volume;
    }
    // UI_SFX볼륨을 바꿔주는 함수
    private void OnUIVolumeChanged(float volume)
    {
        SoundManager.Instance.SetVolume(Sound.UI_SFX, volume);
        loadOptionData.UIVolume = volume;
    }
    #endregion

    #region underBarButton
    //! Confirm버튼을 누르면 실행되는 함수 현재 옵션데이터를 저장한다.
    public void SaveOptionData()
    {
        DataManager.Instance.SaveOptionData(loadOptionData);
    }
    //! Reset Default 버튼을 누르면 실행되는 함수 DataClass안에 정의된 기본 설정으로 옵션을 초기화해준다.
    public void ResetOptionData()
    {
        OptionState defaultData = new OptionState();

        loadOptionData = defaultData;
        DataManager.Instance.SaveOptionData(loadOptionData);
        Screen.fullScreen = loadOptionData.fullScreenOn;
        fullScreenToggle.isOn = loadOptionData.fullScreenOn;
        DataManager.Instance.SetCursor(loadOptionData.mouseCursor);
        seclectMouse = loadOptionData.mouseCursor;
        InitializeSliders();
    }
    //! Cancel 버튼을 누르면 실행되는 함수, 메모리에 옵션 데이터 변수를 저장하지 않고 다시 로드해서 덮어쓴다.
    public void CancleChangeOption()
    {
        loadOptionData = DataManager.Instance.LoadOptionGameData();
        Screen.fullScreen = loadOptionData.fullScreenOn;
        fullScreenToggle.isOn = Screen.fullScreen;
        OnBgmVolumeChanged(loadOptionData.MusicVolume);
        OnSFXVolumeChanged(loadOptionData.SFXVolume);
        OnUIVolumeChanged(loadOptionData.UIVolume);
        DataManager.Instance.SetCursor(loadOptionData.mouseCursor);
    }
    #endregion
}
