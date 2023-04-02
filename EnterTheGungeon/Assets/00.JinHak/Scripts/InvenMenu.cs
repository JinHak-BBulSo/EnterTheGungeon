using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenMenu : MonoBehaviour
{
    public int menuIndex = -1;

    public void OnClickMenuTab()
    {
        InventoryManager.Instance.inventoryDatas.invenListData.TabInvenMenuVal(menuIndex);
        Debug.Log(InventoryManager.Instance.inventoryDatas.invenListData.nowTabInvenCnt);
    }
}
