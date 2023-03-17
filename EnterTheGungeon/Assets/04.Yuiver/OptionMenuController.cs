using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenuController : MonoBehaviour
{

    public GameObject[] optionButton = new GameObject[(int)OptionSetting.MaxCount];
    public GameObject[] optionScrollView = new GameObject[(int)OptionSetting.MaxCount];
    public GameObject settingMenu = null;

    [Header("OptionSettingButton")]
    public GameObject[] gamePlayOptionButton = new GameObject[(int)GamePlaySetting.MaxCount];
    public GameObject[] controlOptionButton = new GameObject[(int)ControlSetting.MaxCount];
    public GameObject[] videoOptionButton = new GameObject[(int)VideoSetting.MaxCount];
    public GameObject[] audioOptionButton = new GameObject[(int)AudioSetting.MaxCount];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
