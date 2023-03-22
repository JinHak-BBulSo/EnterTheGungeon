using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveData;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DataManager.Instance.Create();
        PlayerState testState = DataManager.Instance.LoadGameData();

        Debug.Log($"실행할 때마다 1이 늘어난다 : {testState.hp}");

        testState.hp += 1;

        DataManager.Instance.SaveGameData(testState);
        testState = DataManager.Instance.LoadGameData();

        Debug.Log($"앞의 로그보다 1이 높게 찍히면 정상 : {testState.hp}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
