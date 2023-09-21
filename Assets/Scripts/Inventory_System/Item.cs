using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType { Weapon, Armour, Potion, Food, QuestItem, Generic  }

    [SerializeField] private ItemType itemType = ItemType.Generic;
    [SerializeField] private string itemName;
    [SerializeField] private int itemValue;
    [SerializeField] private Sprite itemImage;
    [SerializeField] private GameObject uiObject;

    [SerializeField] private GameObject worldItem;

    public string GetItemName () { return itemName; }
    public int GetItemValue () { return itemValue; }
    public Sprite GetImage () { return itemImage; }
    public ItemType GetItemType () { return itemType; }
    public GameObject GetUIObject () { return uiObject; }
    public GameObject GetWorldItem () { return worldItem; }

    public Item(Item data)
    {
        itemType = data.itemType;
        itemName = data.itemName;
        itemValue = data.itemValue;
        itemImage = data.itemImage;

        //Debug.Log($"New Item - Name: {itemName} - Type: {itemType}");
    }
}
