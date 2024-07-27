using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemToDrop;
    public int amount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Touched");
            Inventory playerInventory = other.GetComponentInChildren<Inventory>();
            Debug.Log(playerInventory);

            if (playerInventory != null) PickUpItem(playerInventory);
        }
    }

    public void PickUpItem(Inventory inventory)
    {
        Debug.Log("Touched again");
        amount = inventory.AddItem(itemToDrop, amount);
        if (amount < 1) Destroy(this.gameObject);
    }
}
