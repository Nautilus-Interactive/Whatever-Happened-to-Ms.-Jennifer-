using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour {

    public float _maxDistance = 1.0f;
    public Inventory inventory;
    public Dialogue dialogue;

    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            if (hit.transform.gameObject.tag == "Interactable" || hit.transform.gameObject.tag == "NPC") {
                if (Vector3.Distance(hit.transform.position, transform.position) < _maxDistance) {
                    MouseOver(hit.transform.gameObject);
                }
                else {
                    hit.transform.gameObject.SendMessage("Out");
                }
            }
        }
    }

    private void MouseOver(GameObject gameObject) {
        switch(gameObject.tag) {
            case "Interactable":
                gameObject.SendMessage("Over");
                if (Input.GetMouseButtonUp(0)) {
                    InventoryItem item = gameObject.GetComponent<InventoryItem>();
                    if (item != null) {
                        inventory.AddItem(item);
                    }
                }
                break;
            case "NPC":
                gameObject.SendMessage("Over");
                if (Input.GetMouseButtonUp(0)) {
                    DialogueItem item = gameObject.GetComponent<DialogueItem>();
                    if (item != null) {
                        dialogue.StartDialogue(item);
                    }
                }
                break;
        }
    }
}
