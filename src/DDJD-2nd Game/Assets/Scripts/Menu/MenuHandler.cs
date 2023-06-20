using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{

    [SerializeField]
    private Slider loadingSlider;
    private AsyncOperation sceneLoadingOperation;
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


    private IEnumerator LoadScene(string sceneName)
    {
        sceneLoadingOperation = SceneManager.LoadSceneAsync(sceneName);

        loadingSlider.gameObject.SetActive(true);
        loadingSlider.value = 0f;
        GameObject.Find("StartButton").SetActive(false);
        GameObject.Find("PlaygroundButton").SetActive(false);
        GameObject.Find("QuitButton").SetActive(false);

        while (!sceneLoadingOperation.isDone)
        {
            loadingSlider.value = sceneLoadingOperation.progress * 100;
            yield return null;
        }
    }
}