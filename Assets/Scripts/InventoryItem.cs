using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InventoryItem {
    string Name { get; }
    string Description { get; }
    Sprite Image { get; }
    void OnPickup();
}

public class InventoryItemEventArgs : EventArgs {

    public InventoryItem Item;

    public InventoryItemEventArgs(InventoryItem item) {
        Item = item;
    }

}
