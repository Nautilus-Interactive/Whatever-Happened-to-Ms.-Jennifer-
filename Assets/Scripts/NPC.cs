using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : InteractableObject, DialogueItem, NoteItem {
    public string _Name;
    public string Name {
        get {
            return _Name;
        }
    }
    
    public string _TempName;
    public string TempName {
        get {
            return _TempName;
        }
    }
    
    public DialogueSentence[] _Senteces;
    public DialogueSentence[] Sentences {
        get {
            return _Senteces;
        }
    }

    [TextArea(3, 10)]
    public string _Note;
    public string Note {
        get {
            return _Note;
        }
    }

    public void OnPickup() {
        return;
    }

    public void OnStartDialogue() {
        GameObject.Find("HUD").GetComponent<HUD>().SetCurrentInteraction(this.gameObject);
    }
}
