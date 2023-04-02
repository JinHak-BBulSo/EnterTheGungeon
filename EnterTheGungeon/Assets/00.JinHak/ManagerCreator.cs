using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCreator : MonoBehaviour
{
    void Awake()
    {
        SoundManager.Instance.Create();
        DataManager.Instance.Create();
        EnemyManager.Instance.Create();
        LoadingManager.Instance.Create();
        PlayerManager.Instance.Create();
        InventoryManager.Instance.Create();
        ObjectManager.Instance.Create();
        DoorManager.Instance.Create();
        DropManager.Instance.Create();

        SoundManager.Instance.Play("BGM/01 ENTER THE GUN", Sound.Bgm);
        GFunc.LoadScene("01.TitleScene");
    }
}
