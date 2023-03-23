using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using SaveData;
using Sirenix.Serialization;

public class AESExample : MonoBehaviour
{
    private void Start()
    {
        // 원본 데이터
        // 이건 내가 저장할 데이터

        // 256비트(32바이트) 길이의 키 생성
        byte[] key = new byte[32];
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(key);
        }
        // 128비트(16바이트) 길이의 초기화 벡터 생성
        byte[] iv = new byte[16];
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(iv);
        }

        // 데이터 암호화
        //byte[] encryptedData = AESTestHelper.Encrypt(내가 세이브할 데이터, key, iv);

        // 데이터 복호화
        //byte[] decryptedData = AESTestHelper.Decrypt(encryptedData, key, iv);

        // 결과 확인
        //Debug.Log("Encrypted data: " + BitConverter.ToString(encryptedData));
        //Debug.Log("Decrypted data: " + BitConverter.ToString(decryptedData));

    }

}
