using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Item itemData;

    [SerializeField] private Image itemImage;

    public void PrepareUI(Item data)
    {
        itemData = data;

        if (itemData)
            itemImage.sprite = itemData.GetImage();
    }
}
