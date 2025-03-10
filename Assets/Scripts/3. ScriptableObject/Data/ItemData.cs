using System;
using UnityEngine;

public enum ItemType
{
    Thing,      // 상자들
    Equipable,  // 해머
    Consumable  // 병
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
    public GameObject prefab;

    [Header("InventoryIn")]
    public bool canInventoryIn;
}