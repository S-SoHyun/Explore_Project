using System;
using UnityEngine;

public enum ItemType
{
    Resource,
    Equipable,
    Consumable
}


[Serializable]
public class ItemDataConsumable
{
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject prefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;
}