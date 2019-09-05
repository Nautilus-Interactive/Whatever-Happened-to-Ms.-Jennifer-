using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CopCar : MonoBehaviour
{
    public GameObject UI;
    public HUD hud;
    public int evidenceNeeded;

    private bool canAccuse = false;
    private bool displayed = false;

    public void Update() {
        if (!hud) { return; }
        canAccuse = (hud._inventory.EvidenceAmount() + hud._notePad.NoteAmount()) >= evidenceNeeded;
        if (canAccuse && !displayed) {
            displayed = true;
            hud.ShowCanAccuse();
        }
    }

    private void OnMouseOver() {
        if (canAccuse) {
            UI.SetActive(true);
        }
    }

    private void OnMouseExit() {
        UI.SetActive(false);
    }

    private void OnMouseUp() {
        if (canAccuse) {
            hud.ShowAccusations();
        }
    }
}
