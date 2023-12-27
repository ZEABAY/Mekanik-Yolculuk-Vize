using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private ItemData itemData; // Reference of the data
    [SerializeField] private int stackSize; // Current stack size - how many of the data do we have

    public ItemData ItemData => itemData;
    public int StackSize => stackSize;

    public InventorySlot(ItemData source, int amount) // Constructer to make a occupied inventory slot
    {
        itemData = source;
        stackSize = amount;
    }

    public InventorySlot() // Constructer to make a empty inventory slot
    {
        ClearSlot();
    }

    public void ClearSlot() // Clears the slot
    {
        itemData = null;
        stackSize = -1;
    }

    public void AssignItem(InventorySlot invSlot) // Assign an item to the slot
    {
        if (itemData == invSlot.ItemData) // Does the slot contain same item
        {
            AddToStack(invSlot.stackSize); // Add to stack to
        }
        else
        {
            // Overwrite slot with the inventory slot that we're passing in
            itemData = invSlot.itemData;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }

    public void UpdateInventorySlot(ItemData data, int amount) // Updates slot directly
    {
        itemData = data;
        stackSize = amount;
    }

    public bool EnoughRoomLeftInStack(int amountToAdd, out int amountRemaining) // Would the be enuff room in the stack for the amount we're trying to add
    {
        amountRemaining = ItemData.MaxStackSize - stackSize;
        return EnoughRoomLeftInStack(amountToAdd);
    }

    public bool EnoughRoomLeftInStack(int amountToAdd)
    {
        return itemData == null || stackSize + amountToAdd <= ItemData.MaxStackSize;
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }

    public bool SplitStack(out InventorySlot splitStack)
    {
        if (StackSize <= 1) // Is there enough to split
        {
            // Return false
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(stackSize / 2); // Get the half the stack
        RemoveFromStack(halfStack);

        splitStack = new InventorySlot(itemData, halfStack); // Creates a copy of this slot with half the stack size
        return true;
    }
}
