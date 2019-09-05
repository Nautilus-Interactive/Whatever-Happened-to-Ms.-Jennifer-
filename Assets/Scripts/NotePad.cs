using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePad : MonoBehaviour
{
    public List<NoteItem> collectedNotes = new List<NoteItem>();

    public event EventHandler<NoteItemEventArgs> NoteAdded;

    public void AddNote(NoteItem note) {
        if (!collectedNotes.Contains(note)) {
            collectedNotes.Add(note);
            note.OnPickup();

            if (NoteAdded != null) {
                NoteAdded(this, new NoteItemEventArgs(note));
            }
        }
    }

    public int NoteAmount() {
        return collectedNotes.Count;
    }
}
