using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyController : MonoBehaviour
{
    public GameObject keyTxt = default;
    // Start is called before the first frame update
    void Start()
    {
        keyTxt = gameObject.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerKey(int key_)
    {
        keyTxt.GetComponent<TMP_Text>().text = $"{key_}";
    }
}