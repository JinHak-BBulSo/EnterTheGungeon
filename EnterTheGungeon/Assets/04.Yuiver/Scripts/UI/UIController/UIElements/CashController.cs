using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CashController : MonoBehaviour
{
    GameObject cashTxt = default;
    // Start is called before the first frame update
    void Start()
    {
        cashTxt = gameObject.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPlayerCash(int cash_)
    {
        cashTxt.GetComponent<TMP_Text>().text = $"{cash_}";
    }
}
