using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjsManager : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


}
