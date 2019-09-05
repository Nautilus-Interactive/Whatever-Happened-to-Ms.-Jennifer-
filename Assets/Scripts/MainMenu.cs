using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public GameObject credits;
    public GameObject options;
    public GameObject controls;

    public void StartGame() {
        Debug.Log("Loading Game Scene...");
        SceneManager.LoadScene("Starting");
    }

    // Options Screen Methods
    public void ShowOptions() {
        Debug.Log("Loading Options Panel...");
        HideCredits();
        HideControls();
        options.SetActive(true);
    }

    public void HideOptions() {
        Debug.Log("Loading Options Panel...");
        options.SetActive(false);
    }

    // Controls Screen Methods
    public void ShowControls() {
        Debug.Log("Loading Controls Panel...");
        HideCredits();
        HideOptions();
        controls.SetActive(true);
    }

    public void HideControls() {
        Debug.Log("Loading Controls Panel...");
        controls.SetActive(false);
    }

    // Credits Screen Methods
    public void ShowCredits() {
        Debug.Log("Loading Credit Panel...");
        HideOptions();
        HideControls();
        credits.SetActive(true);
    }

    public void HideCredits() {
        Debug.Log("Loading Credit Panel...");
        credits.SetActive(false);
    }

    public void QuitGame() {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
