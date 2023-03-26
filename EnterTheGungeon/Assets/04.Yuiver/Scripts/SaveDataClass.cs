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
    }

    [Serializable]
    public class OptionState
    {
        [OdinSerialize]
        public float MusicVolume;
        [OdinSerialize]
        public float SFXVolume;
        [OdinSerialize]
        public float UIVolume;
        [OdinSerialize]
        public int mouseCursor;
        // Other fields you want to save
    }
}
