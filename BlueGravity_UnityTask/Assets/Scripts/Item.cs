using UnityEngine;
using System;

public enum ItemType
{
    NONE = 0,
    BOW = 1,
    SWORD = 2
}

[Serializable]
public class Item
{
    public ItemType type;
    public Sprite icon;
    public string name;
    public int price;
    public int stock;
}