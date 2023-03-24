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
}
