using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Typer : MonoBehaviour
{
    public TextMeshProUGUI text;

    [TextArea(3, 10)]
    public string[] sentences;

    private int i = 0;

    public string nextScene;

    private bool typing = false;
    private string sentence;

    public void Start() {
        DisplayNextSentence();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
            if (i >= sentences.Length && !typing) {
                SceneManager.LoadScene(nextScene);
                return;
            }
            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence() {
        if (typing) {
            text.text = sentence;
            StopAllCoroutines();
            typing = false;
            return;
        }

        if (i >= sentences.Length) {
            return;
        }

        StopAllCoroutines();
        sentence = sentences[i++];
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence) {
        typing = true;
        text.text = "";
        int x = 0;
        foreach (char c in sentence.ToCharArray()) {
            text.text += c;
            if (x++ % 2 == 0) {
                yield return null;
            }
        }
        typing = false;
    }
}
