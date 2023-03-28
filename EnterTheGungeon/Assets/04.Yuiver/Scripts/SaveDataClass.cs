using Sirenix.Serialization;
using System;

namespace SaveData
{

    /********************************************************************************************
     *  [Yuiver]                                                                                *
     *  This class is the code that manages the save data class.                                *
     *  If you want to add other data add class or variable using below method                  *
     ********************************************************************************************/
    [Serializable]
    public class AESKey
    {
        [OdinSerialize]
        public byte[] AES_Key;
        [OdinSerialize]
        public byte[] AES_Iv;
    }


    [Serializable]
    public class PlayerState
    {
        [OdinSerialize]
        public int hp;

        [OdinSerialize]
        public int maxHp;

        [OdinSerialize]
        public int shield;

        [OdinSerialize]
        public int blank;

        [OdinSerialize]
        public int money;

        [OdinSerialize]
        public int key;

        [OdinSerialize]
        public int deathCount;
        // Other fields you want to save

        public PlayerState()
        {
            hp = 6;
            maxHp = 6;
            shield = 0;
            blank = 2;
            money = 0;
            key = 1;
            deathCount = 0;
            // 다른 필드에 대한 기본값 설정                
        }
        /// <summary>
        /// 오버로딩된 플레이어의 설정을 하는 함수입니다.
        /// </summary>
        /// <param name="hp_">플레이어의 현재 체력</param>
        /// <param name="maxHp_">플레이어의 최대 체력</param>
        /// <param name="shield_">플레이어가 가지고 있는 방어막수</param>
        /// <param name="blank_">플레이어가 가지고 있는 공포탄수</param>
        /// <param name="money_">플레이어가 가지고 있는 돈의 수</param>
        /// <param name="key_">플레이어가 가지고 있는 열쇠의 수</param>
        /// <param name="deathCount_">플레이어가 지금까지 죽은 수 이값은 무조건 로드해서 설정해야합니다.</param>
        public PlayerState(int hp_,int maxHp_,int shield_,int blank_,int money_,int key_,int deathCount_)
        {
            hp = hp_;
            maxHp = maxHp_;
            shield = shield_;
            blank = blank_;
            money = money_;
            key = key_;
            deathCount = deathCount_;
            // 다른 필드에 대한 기본값 설정     
        }
    }

    [Serializable]
    public class OptionState
    {
        //게임 플레이 옵션
        [OdinSerialize]
        public int mouseCursor;

        //그래픽 옵션
        [OdinSerialize]
        public bool fullScreenOn;


        //사운드 옵션
        [OdinSerialize]
        public float MusicVolume;
        [OdinSerialize]
        public float SFXVolume;
        [OdinSerialize]
        public float UIVolume;
        // Other fields you want to save

        public OptionState()
        {
            mouseCursor = 0;

            fullScreenOn = true;

            MusicVolume = 1f;
            SFXVolume = 1f;
            UIVolume = 1f;
        }

        /// <summary>
        /// 오버로딩된 옵션의 설정을 하는 함수입니다.
        /// </summary>
        /// <param name="fullScreenOn_">전체화면인지 아닌지 bool값 입력하세요.</param>
        /// <param name="musicVolume_">0~1사이의 값만 입력하세요.</param>
        /// <param name="SFXVolume_">0~1사이의 값만 입력하세요.</param>
        /// <param name="UIVolume_">0~1사이의 값만 입력하세요.</param>
        /// <param name="mouseCursor_">마우스 커서는 0~5번까지 존재합니다.</param>
        public OptionState(bool fullScreenOn_, float musicVolume_,float SFXVolume_,float UIVolume_,int mouseCursor_ = 0)
        {
            fullScreenOn = fullScreenOn_;
            MusicVolume = musicVolume_;
            SFXVolume = SFXVolume_;
            UIVolume = UIVolume_;
            mouseCursor = mouseCursor_;
        }

    }
}
