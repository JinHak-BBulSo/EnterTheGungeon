using Sirenix.Serialization;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using SaveData;

public class DataManager : GSingleton<DataManager>
{

    private string AES_SavePath = default;
    private string OptionSave = default;
    private string SavePath = default;

    public Texture2D[] cursorImg = default;

    protected override void Init()
    {
        base.Init();
        cursorImg = Resources.LoadAll<Texture2D>("MOUSE");
        AES_SavePath = Path.Combine(Application.persistentDataPath, "GungeonKey.bytes");
        SavePath = Path.Combine(Application.persistentDataPath, "GungeonSaveData.bytes");
        OptionSave = Path.Combine(Application.persistentDataPath, "GungeonOptionData.bytes");
    }
    public void SetCursor(int i)
    {
        Cursor.SetCursor(cursorImg[i], new UnityEngine.Vector2(6.5f, 6.5f), CursorMode.Auto);
    }

    #region PlayerData
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
            PlayerState defaultData = new PlayerState();

            // 기본 데이터 저장
            SaveGameData(defaultData);

            // 기본 데이터 반환
            return defaultData;
        }
    }
    #endregion

    #region OptionData
    public void SaveOptionData(OptionState data)
    {
        AESKey AESKey = CreatOrLoadAESkey();

        byte[] bytes = SerializationUtility.SerializeValue(data, DataFormat.Binary);
        bytes = AESHelper.Encrypt(bytes, AESKey.AES_Key, AESKey.AES_Iv);
        System.Text.Encoding.Default.GetBytes(OptionSave);
        File.WriteAllBytes(OptionSave, bytes);
        Debug.Log($"Game data saved to: {OptionSave}");
    }
    public OptionState LoadOptionGameData()
    {
        AESKey AESKey = CreatOrLoadAESkey();

        if (File.Exists(OptionSave))
        {
            byte[] bytes = File.ReadAllBytes(OptionSave);
            bytes = AESHelper.Decrypt(bytes, AESKey.AES_Key, AESKey.AES_Iv);
            OptionState data = SerializationUtility.DeserializeValue<OptionState>(bytes, DataFormat.Binary);
            Debug.Log($"Game data loaded to: {OptionSave}");
            return data;
        }
        else
        {
            Debug.LogWarning($"No save file found at: {OptionSave}");

            // 기본값을 가진 GameSaveData 객체 생성
            OptionState defaultData = new OptionState();

            // 기본 데이터 저장
            SaveOptionData(defaultData);

            // 기본 데이터 반환
            return defaultData;
        }
    }
    #endregion

    #region [yuiver]Save&Load Data AES256Key Don't touch
    //! AES-256 키가 생성될때 기본 경로에 저장해주는 함수
    private void SaveAESData(AESKey data)
    {
        byte[] bytes = SerializationUtility.SerializeValue(data, DataFormat.Binary);
        System.Text.Encoding.Default.GetBytes(AES_SavePath);
        File.WriteAllBytes(AES_SavePath, bytes);
    }
    //! AES-256 키가 저장된 파일이 있다면 불러오고 없다면 새로 생성해서 저장해주는 함수
    private AESKey CreatOrLoadAESkey()
    {
        if (File.Exists(AES_SavePath))
        {
            byte[] bytes = File.ReadAllBytes(AES_SavePath);
            AESKey data = SerializationUtility.DeserializeValue<AESKey>(bytes, DataFormat.Binary);
            return data;
        }
        else
        {
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
