using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlankController : MonoBehaviour
{
    public const int PLAYER_Display_MAX_Blank = 22;

    [ShowInInspector]
    public List<BlankElement> blankObjList = new List<BlankElement>();

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            blankObjList.Add(transform.GetChild(i).GetComponent<BlankElement>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerBlank(int playerBlank_)
    {
        int playerBlank= playerBlank_;

        for (int i = 0; i < playerBlank; i++)
        {
            blankObjList[i].GetComponent<Image>().enabled = true;
        }
        for (int i = 0; i < transform.childCount - playerBlank; i++)
        {
            blankObjList[playerBlank + i].GetComponent<Image>().enabled = false;
        }
    }
}
