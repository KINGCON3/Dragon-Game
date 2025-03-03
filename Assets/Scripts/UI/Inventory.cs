using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using Unity.Services.Matchmaker.Models;
using UnityEngine.Assertions.Must;
using UnityEditor;
using UnityEngine.UI;
using Unity.Mathematics;

public class Inventory : MonoBehaviour
{
    [SerializeReference] public List<ItemSlotInfo> items = new List<ItemSlotInfo>();

    [Space]
    [Header("inventory Menu Components")]
    public GameObject inventoryMenu;
    public GameObject itemPanel;
    public GameObject itemPanelGrid;
    public GameObject autoSortButton;

    public Mouse mouse;
    public GameObject player;

    private List<ItemPanel> existingPanels = new List<ItemPanel>();

    Dictionary<string, Item> allItemsDictionary = new Dictionary<string, Item>();

    [Space]
    public int inventorySize = 44;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            items.Add(new ItemSlotInfo(null, 0));
        }

        List<Item> allItems = GetAllItems().ToList();
        string itemsInDictionary = " Items in Dictionary: ";
        foreach (Item i in allItems)
        {
            if (!allItemsDictionary.ContainsKey(i.GiveName()))
            {
                allItemsDictionary.Add(i.GiveName(), i);
                itemsInDictionary += ", " + i.GiveName();
            }
            else
            {
                Debug.Log("" + " already exists in Dictionary - shares name with " + allItemsDictionary[i.GiveName()]);
            }
        }
        itemsInDictionary += ".";
        Debug.Log(itemsInDictionary);

        //Add Items for testing
        //AddItem("Wood", 44);
        //AddItem("Stone", 20);
        AddItem("OneGreenDragon", 1);
        AddItem("OneYellowDragon", 1);
        AddItem("OneOrangeDragon", 1);
        AddItem("OneRedDragon", 1);
        AddItem("OneBlackDragon", 1);
        AddItem("OneWhiteDragon", 1);

        //AddItem("TwoGreenDragon", 3);
        //AddItem("ThreeGreenDragon", 3);
        //AddItem("FourGreenDragon", 3);
        //AddItem("FiveGreenDragon", 3);
        AddItem("Egg", 100);

        Button button = autoSortButton.GetComponent<Button>();
   
        if (button != null)
        {
            button.onClick.AddListener(buttonClick);
        }
        else
        {
            Debug.LogWarning("No Button component found.");
        }
    }

    void buttonClick()
    {
        Debug.Log("Yes");
        //string oldlist = "";
        //string newlist = "";
        //foreach (ItemSlotInfo i in items)
        //{
        //    if (i.item != null)
        //    {
        //        oldlist = oldlist + i.item.GiveName();
        //    }
        //}

        // star then colour
        items.Sort((item1, item2) =>
        {
            // Handle null cases first
            if (item1.item == null && item2.item == null)
            {
                return 0; // Both items are null, consider them equal
            }
            if (item1.item == null)
            {
                return 1; // Null items go to the end
            }
            if (item2.item == null)
            {
                return -1; // Null items go to the end
            }

            // Proceed with comparisons if both items are non-null
            int starComparison = item2.item.GiveStar().CompareTo(item1.item.GiveStar());
            if (starComparison == 0)
            {
                Debug.Log(item1.item.GiveColour());
                Debug.Log(item2.item.GiveColour());

                return item2.item.GiveColour().CompareTo(item1.item.GiveColour());
            }

            return starComparison; // Return the star comparison if not equal
        });


        //foreach (ItemSlotInfo i in items)
        //{
        //    if (i.item != null)
        //    {
        //        newlist = newlist + i.item.GiveName();
        //    }
        //}

        //Debug.Log(oldlist);
        //Debug.Log(newlist);

        RefreshInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
               if (inventoryMenu.activeSelf)
            {
                inventoryMenu.SetActive(false);
                mouse.EmptySlot();
                autoSortButton.SetActive(false);
                mouse.gameObject.SetActive(false);
                //Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                inventoryMenu.SetActive(true);
                autoSortButton.SetActive(true);
                mouse.gameObject.SetActive(true);
                //Cursor.lockState = CursorLockMode.Confined;
                RefreshInventory();
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && mouse.itemSlot.item != null)
        {
            RefreshInventory();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && mouse.itemSlot.item != null && !EventSystem.current.IsPointerOverGameObject()) {
            DropItem(mouse.itemSlot.item.GiveName());
        }
        if (Input.GetKeyDown(KeyCode.Q) && mouse.itemSlot.item != null)
        {
            DropItem(mouse.itemSlot.item.GiveName());
        }
    }

    public void RefreshInventory()
    {
        int eggPos = -1;
        int count = -1;
        ItemSlotInfo egg = null;
        ItemSlotInfo itemToSwap = null;
        foreach (ItemSlotInfo i in items)
        {
            if (i.item != null)
            {
                count += 1;
                if (i.item.GiveName().Equals("Egg"))
                {
                    eggPos = count;
                    egg = i;
                }
                itemToSwap = i;
            }
        }
        //Debug.Log(eggPos);
        //Debug.Log(count + 1);

        if (eggPos > -1 && eggPos != count)
        {
            ItemSlotInfo tempItem = new ItemSlotInfo(egg.item, egg.stacks);
            egg.item = itemToSwap.item;
            egg.stacks = itemToSwap.stacks;

            itemToSwap.item = tempItem.item;
            itemToSwap.stacks = tempItem.stacks;
        }


        existingPanels = itemPanelGrid.GetComponentsInChildren<ItemPanel>().ToList();
        //Create Panels if needed
        if (existingPanels.Count < inventorySize)
        {
            int amountToCreate = inventorySize - existingPanels.Count;
            for (int i = 0; i < amountToCreate; i++)
            {
                GameObject newPanel = Instantiate(itemPanel, itemPanelGrid.transform);
                existingPanels.Add(newPanel.GetComponent<ItemPanel>());
            }
        }


        int index = 0;
        foreach (ItemSlotInfo i in items)
        {
            //name List Elements
            i.name = "" + (index + 1);
            if (i.item != null) i.name += ": " + i.item.GiveName();
            else i.name += ": -";

            //Update Panels
            ItemPanel panel = existingPanels[index];
            if (panel != null)
            {
                panel.name = i.name + " Panel";
                panel.inventory = this;
                panel.itemSlot = i;
                //panel.slotInfo = i;
                if (i.item != null)
                {
                    panel.itemImage.gameObject.SetActive(true);
                    panel.itemImage.sprite = i.item.GiveItemImage();
                    panel.itemImage.CrossFadeAlpha(1, 0.05f, true);
                    if (panel.name.Contains("Dragon"))
                    {
                        //Debug.Log("dragon");
                        //Set star text
                        panel.starText.text = i.item.GiveStar().ToString();
                        //set confidence text
                        panel.confidenceText.text = i.item.GiveConfidence().ToString();

                        panel.starImage.gameObject.SetActive(true);
                        panel.starText.gameObject.SetActive(true);
                        panel.confidenceImage.gameObject.SetActive(true);
                        panel.confidenceText.gameObject.SetActive(true);
                        panel.stacksText.gameObject.SetActive(false);
                    } else
                    {
                        panel.stacksText.gameObject.SetActive(true);
                        panel.stacksText.text = "" + i.stacks;

                        panel.starImage.gameObject.SetActive(false);
                        panel.starText.gameObject.SetActive(false);
                        panel.confidenceImage.gameObject.SetActive(false);
                        panel.confidenceText.gameObject.SetActive(false);
                    }
                }
                else
                {
                    panel.itemImage.gameObject.SetActive(false);
                    panel.stacksText.gameObject.SetActive(false);
                    panel.starImage.gameObject.SetActive(false);
                    panel.starText.gameObject.SetActive(false);
                    panel.confidenceImage.gameObject.SetActive(false);
                    panel.confidenceText.gameObject.SetActive(false);
                }
            }
            index++;
        }
        mouse.EmptySlot();
    }

    public int AddItem(string itemName, int amount)
    {
        //Find item to add
        Item item = null;
        allItemsDictionary.TryGetValue(itemName, out item);
        //Exit method if no Item was found
        if (item == null)
        {
            Debug.Log("Could not find Item in Dictionary to add to Inventory");
            return amount;
        }

        //Check for open spaces in existing slots
        foreach(ItemSlotInfo i in items)
        {
            if (i.item != null)
            {
                if (i.item.GiveName() == item.GiveName())
                {
                    if (amount > i.item.MaxStacks() - i.stacks)
                    {
                        amount -= i.item.MaxStacks() - i.stacks;
                        i.stacks = i.item.MaxStacks();
                    }
                    else
                    {
                        i.stacks += amount;
                        if (inventoryMenu.activeSelf) RefreshInventory();
                        return 0;
                    }
                }
            }
        }
        //Fill empty slots with leftover items
        foreach(ItemSlotInfo i in items)
        {
            if (i.item == null)
            {
                if (amount > item.MaxStacks())
                {
                    i.item = item;
                    i.stacks = item.MaxStacks();
                    amount -= item.MaxStacks();
                }
                else
                {
                    i.item = item;
                    i.stacks = amount;
                    if (inventoryMenu.activeSelf) RefreshInventory();
                    return 0;
                }
            }
        }
        //No space in Inventory, return remainder items
        Debug.Log("No space in Inventory for: " + item.GiveName());
        Debug.Log(amount);
        if (inventoryMenu.activeSelf) RefreshInventory();
        return amount;
    }

    public void DropItem(string itemName)
    {
        //Find item to add
        Item item = null;
        allItemsDictionary.TryGetValue(itemName, out item);
        //Exit method if no Item was found
        if (item == null)
        {
            Debug.Log("Could not find Item in Dictionary to drop");
        }

        Transform camTransform = player.transform;

        GameObject droppedItem = Instantiate(item.DropObject(),
            camTransform.transform.position + new Vector3(0, 0, 3f) + camTransform.forward,
            Quaternion.Euler(Vector3.zero));

        Rigidbody rb = droppedItem.GetComponent<Rigidbody>();
        if (rb != null) rb.velocity = camTransform.forward * 5;

        ItemPickup ip = droppedItem.GetComponentInChildren<ItemPickup>();
        if (ip != null)
        {
            ip.itemToDrop = itemName;
            ip.amount = mouse.splitSize;
            mouse.itemSlot.stacks -= mouse.splitSize;
        }

        if (mouse.itemSlot.stacks < 1) ClearSlot(mouse.itemSlot);
        mouse.EmptySlot();
        RefreshInventory();
    }

    public void ClearSlot(ItemSlotInfo slot)
    {
        slot.item = null;
        slot.stacks = 0;
    }

    public List<ItemSlotInfo> getItems()
    {
        return items;
    }

    IEnumerable<Item> GetAllItems()
    {
        return System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes()).Where(type => type.IsSubclassOf(typeof(Item)))
            .Select(type => System.Activator.CreateInstance(type) as Item);
    }
}
