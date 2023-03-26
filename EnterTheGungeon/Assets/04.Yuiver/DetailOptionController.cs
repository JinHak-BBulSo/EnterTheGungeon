using SaveData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DetailOptionController : MonoBehaviour
{
    OptionState loadOptionData = default;
    int seclectMouse = default;

    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider uiSfxSlider;

    private void Awake()
    {
        
    }
    private void Start()
    {

    }
    private void OnEnable()
    {
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
        OptionState defaultData = new OptionState
        {
            MusicVolume = 1f,
            SFXVolume = 1f,
            UIVolume = 1f,
            mouseCursor = 0,
            // 다른 필드에 대한 기본값 설정                
        };
        loadOptionData = defaultData;
        DataManager.Instance.SaveOptionData(loadOptionData);
        DataManager.Instance.SetCursor(loadOptionData.mouseCursor);
        InitializeSliders();
    }
    public void CancleChangeOption()
    {
        loadOptionData = DataManager.Instance.LoadOptionGameData();
        OnBgmVolumeChanged(loadOptionData.MusicVolume);
        OnSFXVolumeChanged(loadOptionData.SFXVolume);
        OnUIVolumeChanged(loadOptionData.UIVolume);
        DataManager.Instance.SetCursor(loadOptionData.mouseCursor);
    }
    #endregion


    // Update is called once per frame
    void Update()
    {
        
    }
}
