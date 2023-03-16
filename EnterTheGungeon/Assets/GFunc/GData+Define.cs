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
    Bgm,    /// < Sound BackGround Audio Loop
    SE,     /// < Sound Effect Audio Play Once
    MaxCount,   /// < Sound AudioSource Length
}
