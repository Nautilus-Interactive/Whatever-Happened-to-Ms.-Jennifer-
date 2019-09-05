using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : InteractableObject, InventoryItem {
    public string _Name;
    public string Name {
        get {
            return _Name;
        }
    }

    [TextArea(3, 10)]
    public string _Description;
    public string Description {
        get {
            return _Description;
        }
    }

    public Sprite _Image;
    public Sprite Image {
        get {
            return _Image;
        }
    }

    public void OnPickup() {
        Destroy(this.gameObject);
    }
}
