using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ItemInteract : Interactable
{
    private Item interactItem;

    private void Start()
    {
        interactItem = GetComponent<Item>();
    }

    protected override void Interact(Inventory inventory)
    {
        //Debug.Log("ITEM INTERACT!");
        //Debug.Log($"INVENTORY: {inventory}");

        inventory.AddItem(interactItem);

        CreateItemUI(interactItem.GetItemType(), inventory);

        switch (interactItem.GetItemType())
        {
            case Item.ItemType.Weapon:
                if (inventory.EquipedWeapon) inventory.EquipedWeapon.SetActive(false);

                GameObject spawnedItem = Instantiate(interactItem.GetWorldItem(), inventory.weaponHolder.transform);
                inventory.EquipedWeapon = spawnedItem;
                break;
            case Item.ItemType.Armour:
                break;
            case Item.ItemType.Potion:
                break;
            case Item.ItemType.Food:
                break;
            case Item.ItemType.QuestItem:
                break;
            case Item.ItemType.Generic:
                break;
            default:
                break;
        }

        Destroy(gameObject);
    }

    private void CreateItemUI(Item.ItemType type, Inventory inventory)
    {
        GameObject uiItem = null;

        if (type == Item.ItemType.Weapon)
            uiItem = Instantiate(interactItem.GetUIObject(), inventory.weaponSlotSpawn.transform);
        else
            uiItem = Instantiate(interactItem.GetUIObject(), inventory.itemSlotSpawn.transform);

        uiItem.GetComponent<ItemUI>().PrepareUI(interactItem);
    }
}
