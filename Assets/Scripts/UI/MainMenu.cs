using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string lvlName;
    public GameObject picture;
    public GameObject credits;

    public void NewGame() {
        SceneManager.LoadScene(lvlName);
    }

    public void Credits() {
        picture.SetActive(!picture.activeSelf);
        credits.SetActive(!credits.activeSelf);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
