using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public SCInventory playerInventory;
    InventoryUIController inventoryUIController;
    bool isSwapping;
    int tempIndex;
    Slot tempSlot;
    private void Start()
    {
        inventoryUIController = gameObject.GetComponent<InventoryUIController>();
    }
    public void DeleteItem()
    {
        if(isSwapping == true)
        {
            playerInventory.DeleteItem(tempIndex);
            isSwapping = false;
            inventoryUIController.UpdateUI();
        }
    }
    public void DropItem()
    {
        if(isSwapping == true)
        {
            playerInventory.DropItem(tempIndex,gameObject.transform.position + Vector3.forward*2);
            isSwapping = false;
            inventoryUIController.UpdateUI();
        }
    }
    public void SwapItem(int index)
    {
        if(isSwapping == false)
        {
            tempIndex = index;
            tempSlot = playerInventory.inventorySlots[tempIndex];
            isSwapping = true;
        }
        else if(isSwapping == true)
        {
            playerInventory.inventorySlots[tempIndex] = playerInventory.inventorySlots[index];
            playerInventory.inventorySlots[index] = tempSlot;
            isSwapping=false;
        }
        inventoryUIController.UpdateUI();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            if (playerInventory.AddItem(other.gameObject.GetComponent<Item>().item))
            {
                Destroy(other.gameObject);
                inventoryUIController.UpdateUI();
            }
            
        }
    }
}
