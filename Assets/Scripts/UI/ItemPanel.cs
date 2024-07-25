using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;
using System.Linq;
using System.Text;
using System;
using System.Linq.Expressions;

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
        mouse.sourceItemPanel = this;
        if (Input.GetKey(KeyCode.Mouse1) && itemSlot.stacks > 1) mouse.splitSize = itemSlot.stacks / 2;
        else mouse.splitSize = itemSlot.stacks;
        mouse.SetUI();
    }

    public void fadeOut()
    {
        itemImage.CrossFadeAlpha(0.3f, 0.05f, true);
    }

    public void DropItem()
    {
        itemSlot.item = mouse.itemSlot.item;
        if (mouse.splitSize < mouse.itemSlot.stacks)
        {
            itemSlot.stacks = mouse.splitSize;
            mouse.itemSlot.stacks -= mouse.splitSize;
            mouse.EmptySlot();
        }
        else
        {
            itemSlot.stacks = mouse.itemSlot.stacks;
            inventory.ClearSlot(mouse.itemSlot);
        }
    }

    public void SwapItem(ItemSlotInfo slotA, ItemSlotInfo slotB)
    {
        ItemSlotInfo tempItem = new ItemSlotInfo(slotA.item, slotA.stacks);

        slotA.item = slotB.item;
        slotA.stacks = slotB.stacks;

        slotB.item = tempItem.item;
        slotB.stacks = tempItem.stacks;
    }

    public void StackItem(ItemSlotInfo source, ItemSlotInfo destination, int amount)
    {
        int slotsAvailable = destination.item.MaxStacks() - destination.stacks;
        if (slotsAvailable == 0) return;

        if (amount > slotsAvailable)
        {
            source.stacks -= slotsAvailable;
            destination.stacks = destination.item.MaxStacks();
        }
        if (amount <= slotsAvailable)
        {
            destination.stacks += amount;
            if (source.stacks == amount) inventory.ClearSlot(source);
            else source.stacks -= amount;
        }
    }

    public ItemSlotInfo getNextNonMax(Item item)
    {
        //Debug.Log(item.GiveName());
        List<ItemSlotInfo> items = inventory.getItems();
            foreach (ItemSlotInfo i in items)
            {
                if (i.item != null)
                {
                    if (i.item.GiveName().Equals(item.GiveName()) && i.stacks < i.item.MaxStacks())
                    {
                    return i;
                    }
                }
                else
                {
                return i;
                }



            }
        return null;
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
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        Debug.Log("Tired to shift");
                        //while (true)
                        //{

                        //try
                        //{

                        ItemSlotInfo destination = getNextNonMax(itemSlot.item);
                        Debug.Log(destination);

                        if (destination.item != null)
                        {
                            StackItem(itemSlot, destination, itemSlot.stacks);
                        }
                        else
                        {
                            destination.item = itemSlot.item;
                            destination.stacks = itemSlot.stacks;
                            inventory.ClearSlot(itemSlot);
                        }
                        inventory.RefreshInventory();
                    }
                    else
                    {
                        PickupItem();
                        fadeOut();
                    }
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
                //Clicked on occupied slot of same type
                else if (itemSlot.stacks < itemSlot.item.MaxStacks())
                {
                    StackItem(mouse.itemSlot, itemSlot, mouse.splitSize);
                    inventory.RefreshInventory();
                }
            }
        }
    }
}
