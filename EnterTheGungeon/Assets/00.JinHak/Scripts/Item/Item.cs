using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", order = 0)]
public class Item : ScriptableObject
{
    public string itemName = string.Empty;
    public ItemTag tag = ItemTag.NONE;
    public Sprite itemSprite = default;
    public string itemScript = string.Empty;
    // [Junil] 아이템 정보를 담기 위해 추가한 string 값
    public string itemDescTxt = string.Empty;
    public string itemTypeTxt = string.Empty;
}
