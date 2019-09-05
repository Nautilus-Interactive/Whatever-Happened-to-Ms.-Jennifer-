using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableObject : MonoBehaviour {
    public string _prompt;
    public GameObject UI;

    // Functions for object UI
    public void Start() {
        UI.SetActive(false);
        UI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _prompt;
    }

    // Called by the mouse ray when inside range
    public void Over() {
        UI.SetActive(true);
    }

    // Called by the mouse ray when outside range
    public void Out() {
        UI.SetActive(false);
    }

    // Called when the mouse moves outside the object
    public void OnMouseExit() {
        UI.SetActive(false);
    }
}
