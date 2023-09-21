using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int inventorySize = 10;

    [SerializeField] private List<Item> inventoryItems = new List<Item>();

    [Header("Player Weapons")]
    public GameObject weaponHolder;
    [SerializeField] private GameObject equipedWeapon;

    [Header("UI Icon Spawns")]
    public GameObject weaponSlotSpawn;
    public GameObject itemSlotSpawn;

    public GameObject EquipedWeapon { 
        get { return equipedWeapon; } 
        set { equipedWeapon = value; } 
    }

    public void AddItem(Item item)
    {
        Item newInstance = new Item(item);

        inventoryItems.Add(newInstance);

        foreach (var i in inventoryItems)
        {
            //Debug.Log($"Item Name: {i.GetItemName()}");
        }
    }
}
