using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenuController : MonoBehaviour
{
    [BoxGroup("OptionButtons", LabelText = "OptionButtons")]
    [FoldoutGroup("OptionButton")]
    [ListDrawerSettings(Expanded = true)]
    public GameObject[] optionButton = new GameObject[(int)OptionSetting.MaxCount];

    [FoldoutGroup("OptionButton")]
    [ListDrawerSettings(Expanded = true)]
    public GameObject[] optionScrollView = new GameObject[(int)OptionSetting.MaxCount];

    [FoldoutGroup("OptionButton")]
    [ListDrawerSettings(Expanded = true)]
    public GameObject settingMenu = null;

    
    [BoxGroup("Option Setting", LabelText = "Option Setting")]
    [FoldoutGroup("Option Setting/Game Play")]
    [ListDrawerSettings(Expanded = true)]
    [InfoBox("This is GamePlaySetting Scroll", InfoMessageType.None)]
    public GameObject[] gamePlayOptionButton = new GameObject[(int)GamePlaySetting.MaxCount];
    [Space(20)]

    [BoxGroup("Option Setting", LabelText = "Option Setting")]
    [FoldoutGroup("Option Setting/Control")]
    [ListDrawerSettings(Expanded = true)]
    [InfoBox("This is ControlSetting Scroll", InfoMessageType.None)]
    public GameObject[] controlOptionButton = new GameObject[(int)ControlSetting.MaxCount];
    [Space(20)]

    [FoldoutGroup("Option Setting/Video")]
    [ListDrawerSettings(Expanded = true)]
    [InfoBox("This is VideoSetting Scroll", InfoMessageType.None)]
    public GameObject[] videoOptionButton = new GameObject[(int)VideoSetting.MaxCount];
    [Space(20)]

    [FoldoutGroup("Option Setting/Audio")]
    [ListDrawerSettings(Expanded = true)]
    [InfoBox("This is AudioSetting Scroll", InfoMessageType.None)]
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
