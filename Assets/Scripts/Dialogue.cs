using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour {
    public event EventHandler<DialogueItemEventArgs> DialogueStarted;

    public void StartDialogue(DialogueItem dialogue) {
        dialogue.OnStartDialogue();

        if (DialogueStarted != null) {
            DialogueStarted(this, new DialogueItemEventArgs(dialogue));
        }
    }
}
