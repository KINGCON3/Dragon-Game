using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemPanel : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler, IDropHandler
{
    public Inventory inventory;
    private Mouse mouse;
    //public ItemSlotInfo slotInfo;
    public ItemSlotInfo itemSlot;
    public Image itemImage;
    public TextMeshProUGUI stacksText;

    private bool click;

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.pointerPress = this.gameObject;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        click = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (click)
        {
            OnClick();
            click = false;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnClick();
        click = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (click)
        {
            OnClick();
            click = false;
        }
    }

    public void PickupItem()
    {
        mouse.itemSlot = itemSlot;
        mouse.SetUI();
    }

    public void fadeOut()
    {
        itemImage.CrossFadeAlpha(0.3f, 0.05f, true);
    }

    public void DropItem()
    {
        itemSlot.item = mouse.itemSlot.item;
        itemSlot.stacks = mouse.itemSlot.stacks;
        inventory.ClearSlot(mouse.itemSlot);
    }

    public void SwapItem(ItemSlotInfo slotA, ItemSlotInfo slotB)
    {
        ItemSlotInfo tempItem = new ItemSlotInfo(slotA.item, slotA.stacks);

        slotA.item = slotB.item;
        slotA.stacks = slotB.stacks;

        slotB.item = tempItem.item;
        slotB.stacks = tempItem.stacks;
    }

    public void OnClick()
    {
        if (inventory != null)
        {
            mouse = inventory.mouse;

            //Grab item if mouse slot is empty
            if (mouse.itemSlot.item == null)
            {
                if (itemSlot.item != null)
                {
                    PickupItem();
                    fadeOut();
                }
            }
            else
            {
                //Clicked on original slot
                if (itemSlot == mouse.itemSlot)
                {
                    inventory.RefreshInventory();
                }
                //Clicked on empty slot
                else if (itemSlot.item == null)
                {
                    DropItem();
                    inventory.RefreshInventory();
                }
                //Clicked on occupied slot of different item type
                else if (itemSlot.item.GiveName() != mouse.itemSlot.item.GiveName())
                {
                    SwapItem(itemSlot, mouse.itemSlot);
                    inventory.RefreshInventory();
                }
            }
        }
    }
}
