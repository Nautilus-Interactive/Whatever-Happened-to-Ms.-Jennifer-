using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface NoteItem {
    string Name { get; }
    string Note { get; }
    void OnPickup();
}

public class NoteItemEventArgs : EventArgs {
    public NoteItem Note;

    public NoteItemEventArgs(NoteItem note) {
        Note = note;
    }
}
