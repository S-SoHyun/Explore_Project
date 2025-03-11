using System;
using UnityEngine;

public enum ItemType
{
    Thing,       // ����
    Equipable,   // �ظ�
    Consumable   // ��
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
    public bool canInventoryIn;     // ���߿� ��� ���� �����ϸ�.
}