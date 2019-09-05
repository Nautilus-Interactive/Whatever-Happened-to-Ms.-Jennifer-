using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour {
    public Inventory _inventory;
    public Dialogue _dialogue;
    public NotePad _notePad;

    public Queue<DialogueSentence> _sentences;
    public TextMeshProUGUI _dialogueName;
    public TextMeshProUGUI _dialogueText;
    public TextMeshProUGUI _notesText;
    public TextMeshProUGUI _itemDescriptionName;
    public TextMeshProUGUI _itemDescription;

    public GameObject _dialogPanel;
    public GameObject _playerName;
    public GameObject _otherName;
    public TextMeshProUGUI _continueButton;

    public GameObject _inventoryPanel;

    public GameObject _itemDescriptionPanel;

    public GameObject _notesPanel;

    public GameObject _pausePanel;

    public GameObject _accusationsPanel;
    public GameObject _canAccuse;

    private GameObject _talkingTo;
    private DialogueSentence _currentSentence;
    private bool _typeSentenceRunning;
    private DialogueItem _currentDialogue;

    void Start() {
        _inventory.ItemAdded += ItemAdded;
        _dialogue.DialogueStarted += DialogueStarted;
        _notePad.NoteAdded += NoteAdded;

        _typeSentenceRunning = false;
        _notesText.text = "";
        _sentences = new Queue<DialogueSentence>();
        StartTime();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            _pausePanel.SetActive(true);
            StopTime();
        }

        switch (_typeSentenceRunning) {
            case true:
                _continueButton.text = "Skip...";
                break;
            case false:
                _continueButton.text = "Continue...";
                break;
        }
    }

    // Methods used for Pause Menu
    public void Resume() {
        _pausePanel.gameObject.SetActive(false);
        StartTime();
    }

    public void MainMenu() {
        Debug.Log("Loading Menu Scene...");
        StartTime();
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame() {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    private void StopTime() {
        Time.timeScale = 0;
    }

    private void StartTime() {
        Time.timeScale = 1;
    }

    // Methods used for Inventory
    private void ItemAdded(object sender, InventoryItemEventArgs e) {
        foreach (Transform slot in _inventoryPanel.transform) {
            InventorySlot inventorySlot = slot.GetChild(0).GetComponent<InventorySlot>();
            if (!inventorySlot._filled) {
                inventorySlot._filled = true;
                inventorySlot._name = e.Item.Name;
                inventorySlot._description = e.Item.Description;
                Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
                image.enabled = true;
                image.sprite = e.Item.Image;
                ShowDescription(inventorySlot);
                break;
            }
        }
    }

    // Methods used for Item Description
    public void ShowDescription(InventorySlot slot) {
        if (!slot._filled) { return; }
        _itemDescriptionName.text = slot._name;
        _itemDescription.text = slot._description;
        _itemDescriptionPanel.SetActive(true);
    }

    public void HideDescription() {
        _itemDescriptionPanel.SetActive(false);
    }

    // Methods used for Dialog
    private void DialogueStarted(object sender, DialogueItemEventArgs e) {
        _currentDialogue = e.Dialogue;
        _sentences.Clear();

        foreach (DialogueSentence sentance in _currentDialogue.Sentences) {
            _sentences.Enqueue(sentance);
        }

        _dialogPanel.SetActive(true);
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (_typeSentenceRunning) {
            _typeSentenceRunning = false;
            StopAllCoroutines();
            _dialogueText.text = _currentSentence.Text;
        }
        else {
            if (_sentences.Count == 0) {
                EndDialogue();
                return;
            }
            _currentSentence = _sentences.Dequeue();
            SetOtherName();
            _otherName.SetActive(!_currentSentence.Player);
            _playerName.SetActive(_currentSentence.Player);
            StartCoroutine(TypeSentence(_currentSentence.Text));
        }
    }

    public void SetOtherName() {
        switch (_currentSentence.KnowName) {
            case true:
                _dialogueName.text = _currentDialogue.Name;
                break;
            case false:
                _dialogueName.text = _currentDialogue.TempName;
                break;
        }
    }

    IEnumerator TypeSentence(string sentence) {
        _typeSentenceRunning = true;
        _dialogueText.text = "";

        foreach (char c in sentence.ToCharArray()) {
            _dialogueText.text += c;
            yield return null;
        }
        _typeSentenceRunning = false;
    }

    public void SetCurrentInteraction(GameObject target) {
        _talkingTo = target;
    }

    public void EndDialogue() {
        _dialogPanel.SetActive(false);
        NoteItem note = _talkingTo.GetComponent<NoteItem>();
        if (note != null) {
            _notePad.AddNote(note);
        }
    }

    //Methods used for Notes
    private void NoteAdded(object sender, NoteItemEventArgs e) {
        string note = "<b><size=100%>" + e.Note.Name + ":</b>";
        note += "\n\t<size=80%>" + e.Note.Note;
        _notesText.text += note;
    }

    public void ShowNotes() {
        _notesPanel.SetActive(true);
    }

    public void HideNotes() {
        _notesPanel.SetActive(false);
    }

    // Methods used for Accusations
    public void ShowAccusations() {
        _accusationsPanel.SetActive(true);
    }

    public void HideAccusations() {
        _accusationsPanel.SetActive(false);
    }

    public void MakeChoice(GameObject button) {
        SceneManager.LoadScene("Ending");
    }

    public void ShowCanAccuse() {
        _canAccuse.SetActive(true);
    }

    public void HideCanAccuse() {
        _canAccuse.SetActive(false);
    }
}
