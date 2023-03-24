using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static partial class GData
{
}

//! 지형의 속성을 정의하기 위한 타입
public enum TerrainType
{
    NONE = -1, 
    PLAIN_PASS,
    OCEAN_N_PASS
}       // TerrainType


/// [Yuiver] 2023-03-16
/// @brief 사운드의 속성 분류를 위해 반복해서 재생하는 BGM과 이펙트사운드의 타입을 따로 정의했다.
/// @Stats MaxCount 매직넘버를 피하기 위해서 만들었고, AudioSource[]를 만들때 들어가는 배열의 길이를 정의하기 위해 enum타입의 마지막에 넣었다.

//! 사운드의 속성을 정의하기 위한 타입
public enum Sound
{
    Bgm,        /// < Sound BackGround Audio Loop
    SFX,        /// < Sound Effect Audio Play Once
    UI_SFX,     /// < Sound UI Effect Audio Play Once
    MaxCount,   /// < Sound AudioSource Length
}


#region UIStateType

/// [Yuiver] 2023-03-17
/// @brief titleButton_ActiveStatus[(int)TitleSetting.type] 이런식으로 어떤 메뉴를 선택했는지를 보기 쉽게 만들기 위해 정의했다.
/// @Stats MaxCount 매직넘버를 피하기 위해서 만들었고, titleButton_ActiveStatus[]를 만들때 들어가는 배열의 길이를 정의하기 위해 enum타입의 마지막에 넣었다.

//! titleButton_ActiveStatus[]의 선택값을 정의하기 위한 타입
public enum TitleSetting
{
    NONE = -1,
    Start,      /// < TitleSetting Game Start Button state
    Option,     /// < TitleSetting Game Option Button state
    Exit,       /// < TitleSetting Game Exit Button 
    MaxCount,   /// < TitleSetting titleButtonActiveStatus Length
}

/// [Yuiver] 2023-03-17
/// @brief optionButton_ActiveStatus[(int)OptionSetting.type] 이런식으로 어떤 메뉴를 선택했는지를 보기 쉽게 만들기 위해 정의
/// @Stats MaxCount 매직넘버를 피하기 위해서 만들었고, optionButton_ActiveStatus[]를 만들때 들어가는 배열의 길이를 정의하기 위해 enum타입의 마지막에 넣었다.

//! optionButton_ActiveStatus[]의 선택값을 정의하기 위한 타입
public enum OptionSetting
{
    NONE = -1,
    GamePlay,   /// < OptionSetting GamePlay Button state
    Control,    /// < OptionSetting Control Button state
    Video,      /// < OptionSetting Video Button state
    Audio,      /// < OptionSetting Audio Button state
    MaxCount,   /// < OptionSetting optionButtonActiveStatus Length
}

/// [Yuiver] 2023-03-17
/// @brief gamePlayOptionButton_ActiveStatus[(int)GamePlaySetting.type] 이런식으로 어떤 메뉴를 선택했는지를 보기 쉽게 만들기 위해 정의
/// @Stats MaxCount 매직넘버를 피하기 위해서 만들었고, gamePlayOptionButton_ActiveStatus[]를 만들때 들어가는 배열의 길이를 정의하기 위해 enum타입의 마지막에 넣었다.

//! gamePlayOptionButton_ActiveStatus[]의 선택값을 정의하기 위한 타입
public enum GamePlaySetting
{
    NONE = -1,
    SpeedUpOutsideCombat,   /// < GamePlaySetting SpeedUpOutsideCombat Button state
    Language,               /// < GamePlaySetting Language Button state
    CameraShake,            /// < GamePlaySetting CameraShake Button state
    TextSpeed,              /// < GamePlaySetting TextSpeed Button state
    MouseCursor,            /// < GamePlaySetting MouseCursor Button state
    ShowMiniMap,            /// < GamePlaySetting ShowMiniMap Button state
    Credits,                /// < GamePlaySetting Credits Button state
    PrivacyStatement,       /// < GamePlaySetting PrivacyStatement Button state
    MaxCount,               /// < GamePlaySetting gamePlayOptionButton_ActiveStatus Length
}

/// [Yuiver] 2023-03-17
/// @brief controlOptionButton_ActiveStatus[(int)ControlSetting.type] 이런식으로 어떤 메뉴를 선택했는지를 보기 쉽게 만들기 위해 정의
/// @Stats MaxCount 매직넘버를 피하기 위해서 만들었고, controlOptionButton_ActiveStatus[]를 만들때 들어가는 배열의 길이를 정의하기 위해 enum타입의 마지막에 넣었다.

//! controlOptionButton_ActiveStatus[]의 선택값을 정의하기 위한 타입
public enum ControlSetting
{
    NONE = -1,
    Shooting,       /// < ControlSetting Shooting Button state
    RollToDodge,    /// < ControlSetting RollToDodge Button state
    Interaction,    /// < ControlSetting Interaction Button state
    Reload,         /// < ControlSetting Reload Button state
    UseItem,        /// < ControlSetting UseItem Button state
    BlankBullet,    /// < ControlSetting BlankBullet Button state
    Map,            /// < ControlSetting Map Button state
    NextGun,        /// < ControlSetting NextGun Button state
    PreviousGun,    /// < ControlSetting PreviousGun Button state
    NextItem,       /// < ControlSetting NextItem Button state
    PreviousItem,   /// < ControlSetting PreviousItem Button state
    ShowAllGun,     /// < ControlSetting ShowAllGun Button state
    ThrowGun,       /// < ControlSetting ThrowGun Button state
    Pause,          /// < ControlSetting Pause Button state
    Inventory,      /// < ControlSetting Inventory Button state
    MaxCount,       /// < ControlSetting controlOptionButton_ActiveStatus Length
}

/// [Yuiver] 2023-03-17
/// @brief videoOptionButton_ActiveStatus[(int)VideoSetting.type] 이런식으로 어떤 메뉴를 선택했는지를 보기 쉽게 만들기 위해 정의
/// @Stats MaxCount 매직넘버를 피하기 위해서 만들었고, videoOptionButton_ActiveStatus[]를 만들때 들어가는 배열의 길이를 정의하기 위해 enum타입의 마지막에 넣었다.

//! videoOptionButton_ActiveStatus[]의 선택값을 정의하기 위한 타입
public enum VideoSetting
{
    NONE = -1,
    GraphicQuality,         /// < VideoSetting GraphicQuality Button state
    Resolution,             /// < VideoSetting Resolution Button state
    ScreenMode,             /// < VideoSetting ScreenMode Button state
    SelectMoniter,          /// < VideoSetting SelectMoniter Button state
    LightQuality,           /// < VideoSetting LightQuality Button state
    RealTime_Reflection,    /// < VideoSetting RealTime_Reflection Button state
    Scale,                  /// < VideoSetting Scale Button state
    MaxCount,               /// < VideoSetting videoOptionButton_ActiveStatus Length
}

/// [Yuiver] 2023-03-17
/// @brief audioOptionButton_ActiveStatus[(int)AudioSetting.type] 이런식으로 어떤 메뉴를 선택했는지를 보기 쉽게 만들기 위해 정의
/// @Stats MaxCount 매직넘버를 피하기 위해서 만들었고, audioOptionButton_ActiveStatus[]를 만들때 들어가는 배열의 길이를 정의하기 위해 enum타입의 마지막에 넣었다.

//! audioOptionButton_ActiveStatus[]의 선택값을 정의하기 위한 타입
public enum AudioSetting
{
    NONE = -1,
    BGM_Volume,     /// < AudioSetting BGM Button state
    SFX_Volume,     /// < AudioSetting SFX Button state
    UI_SFX_Volume,  /// < AudioSetting UI_SFX Button state
    MaxCount,       /// < AudioSetting audioOptionButton_ActiveStatus Length
}

#endregion

/// [KJH] 2023-03-23
///  @brief item을 tag를 통해 카테고리 별로 분류하기 위해 정의
public enum ItemTag
{
    NONE = -1,
    GUN = 0,
    ACTIVE,
    PASSIVE,
    ETC
}