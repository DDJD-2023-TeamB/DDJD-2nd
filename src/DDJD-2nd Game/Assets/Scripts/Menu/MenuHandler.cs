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

        slider.gameObject.SetActive(true);
        GameObject.Find("StartButton").SetActive(false);
        GameObject.Find("PlaygroundButton").SetActive(false);
        GameObject.Find("QuitButton").SetActive(false);

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // Adjust the progress value
            slider.value = Mathf.RoundToInt(progress * 100);
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
    }
}