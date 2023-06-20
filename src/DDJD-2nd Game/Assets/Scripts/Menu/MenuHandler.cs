using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{

    [SerializeField]
    private Slider slider;
    public void StartGame()
    {
        StartCoroutine(LoadScene("World"));
    }

    public void Playground()
    {
        StartCoroutine(LoadScene("Playground"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        Debug.Log("Loading scene: " + asyncLoad.progress);

        slider.gameObject.SetActive(true);
        GameObject.Find("StartButton").SetActive(false);
        GameObject.Find("PlaygroundButton").SetActive(false);
        GameObject.Find("QuitButton").SetActive(false);

        while (!asyncLoad.isDone)
        {
            slider.value = asyncLoad.progress * 100;
            yield return null;
        }
    }
}