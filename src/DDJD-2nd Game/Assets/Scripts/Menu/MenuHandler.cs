using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("World");
    }

    public void Playground()
    {
        SceneManager.LoadScene("Playground");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}