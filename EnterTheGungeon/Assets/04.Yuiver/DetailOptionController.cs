using SaveData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailOptionController : MonoBehaviour
{
    public Toggle fullScreenToggle = default;
    public TMP_Dropdown resolutionSetting = default;

    private int currentResolutionIndex = 0;

    public struct ResolutionOption
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

    private ResolutionOption[] resolutionOptions = new ResolutionOption[]
{
        new ResolutionOption("1280x720 (HD)", 1280, 720),
        new ResolutionOption("1366x768 (HD+)", 1366, 768),
        new ResolutionOption("1600x900 (HD+)", 1600, 900),
        new ResolutionOption("1920x1080 (FHD)", 1920, 1080),
        new ResolutionOption("2560x1440 (QHD)", 2560, 1440),
        new ResolutionOption("3840x2160 (UHD 4K)", 3840, 2160),
};

    public int seclectMouse = default;

    OptionState loadOptionData = default;

    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider uiSfxSlider;

    private void Awake()
    {
        
    }
    private void Start()
    {
        fullScreenToggle.onValueChanged.AddListener(FullScreenIsOn);
    }
    private void OnEnable()
    {
        ResetDropdown();
        fullScreenToggle.isOn = Screen.fullScreen;
        loadOptionData = DataManager.Instance.LoadOptionGameData();
        seclectMouse = loadOptionData.mouseCursor;
        OnBgmVolumeChanged(loadOptionData.MusicVolume);
        OnSFXVolumeChanged(loadOptionData.SFXVolume);
        OnUIVolumeChanged(loadOptionData.UIVolume);
        InitializeSliders();
    }
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

    #region GraphicOption
    public void FullScreenIsOn(bool isOn)
    {
        Screen.fullScreen = isOn;
        loadOptionData.fullScreenOn = isOn;
    }

    public void ResetDropdown()
    {
        //드롭다운 초기화
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

    private void OnBgmVolumeChanged(float volume)
    {
        SoundManager.Instance.SetVolume(Sound.Bgm, volume);
        loadOptionData.MusicVolume = volume;
    }

    private void OnSFXVolumeChanged(float volume)
    {
        SoundManager.Instance.SetVolume(Sound.SFX, volume);
        loadOptionData.SFXVolume = volume;
    }

    private void OnUIVolumeChanged(float volume)
    {
        SoundManager.Instance.SetVolume(Sound.UI_SFX, volume);
        loadOptionData.UIVolume = volume;
    }
    #endregion

    #region underBarButton
    public void SaveOptionData()
    {
        DataManager.Instance.SaveOptionData(loadOptionData);
    }
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
