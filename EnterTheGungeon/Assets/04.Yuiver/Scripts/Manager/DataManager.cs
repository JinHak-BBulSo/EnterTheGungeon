using Sirenix.Serialization;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using SaveData;
using System.Numerics;
using Unity.VisualScripting;

public class DataManager : GSingleton<DataManager>
{

    private string AES_SavePath = default;
    private string SavePath = default;

    protected override void Init()
    {
        base.Init();
        AES_SavePath = Path.Combine(Application.persistentDataPath, "GungeonKey.bytes");
        SavePath = Path.Combine(Application.persistentDataPath, "GungeonSaveData.bytes");
    }

    public void SaveGameData(PlayerState data)
    {
        AESKey AESKey = CreatOrLoadAESkey();

        byte[] bytes = SerializationUtility.SerializeValue(data, DataFormat.Binary);
        bytes = AESHelper.Encrypt(bytes, AESKey.AES_Key, AESKey.AES_Iv);
        System.Text.Encoding.Default.GetBytes(SavePath);
        File.WriteAllBytes(SavePath, bytes);
        Debug.Log($"Game data saved to: {SavePath}");
    }
    public PlayerState LoadGameData()
    {
        AESKey AESKey = CreatOrLoadAESkey();

        if (File.Exists(SavePath))
        {
            byte[] bytes = File.ReadAllBytes(SavePath);
            bytes = AESHelper.Decrypt(bytes, AESKey.AES_Key, AESKey.AES_Iv);
            PlayerState data = SerializationUtility.DeserializeValue<PlayerState>(bytes, DataFormat.Binary);
            Debug.Log($"Game data loaded to: {SavePath}");
            return data;
        }
        else
        {
            Debug.LogWarning($"No save file found at: {SavePath}");

            // 기본값을 가진 GameSaveData 객체 생성
            PlayerState defaultData = new PlayerState
            {
                hp = 6,
                maxHp = 6,
                shield = 0,
                blank = 2,
                money = 0,
                key = 1
                // 다른 필드에 대한 기본값 설정                
            };

            // 기본 데이터 저장
            SaveGameData(defaultData);

            // 기본 데이터 반환
            return defaultData;
        }
    }

    #region [yuiver]Save&Load Data AES256Key Don't touch
    //! AES-256 키가 생성될때 기본 경로에 저장해주는 함수
    private void SaveAESData(AESKey data)
    {
        byte[] bytes = SerializationUtility.SerializeValue(data, DataFormat.Binary);
        System.Text.Encoding.Default.GetBytes(AES_SavePath);
        File.WriteAllBytes(AES_SavePath, bytes);
        Debug.Log($"Game data saved to: {AES_SavePath}");
    }
    //! AES-256 키가 저장된 파일이 있다면 불러오고 없다면 새로 생성해서 저장해주는 함수
    private AESKey CreatOrLoadAESkey()
    {
        if (File.Exists(AES_SavePath))
        {
            byte[] bytes = File.ReadAllBytes(AES_SavePath);
            AESKey data = SerializationUtility.DeserializeValue<AESKey>(bytes, DataFormat.Binary);
            Debug.Log($"Game data loaded to: {AES_SavePath}");
            return data;
        }
        else
        {
            Debug.LogWarning($"No save file found at: {AES_SavePath}");

            byte[] makeKey = new byte[32];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(makeKey);
            }

            // 128비트(16바이트) 길이의 초기화 벡터 생성
            byte[] makeIv = new byte[16];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(makeIv);
            }

            // 기본값을 가진 GameSaveData 객체 생성
            AESKey defaultData = new AESKey
            {
                AES_Key = makeKey,
                AES_Iv = makeIv
            };

            // 기본 데이터 저장
            SaveAESData(defaultData);

            // 기본 데이터 반환
            return defaultData;
        }
    }
    #endregion

}
