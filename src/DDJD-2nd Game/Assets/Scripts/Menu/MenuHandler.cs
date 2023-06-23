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
    private SoundEmitter _soundEmitter;

    private FMOD.Studio.PARAMETER_ID _paremeterId;

    private void Awake()
    {
        _soundEmitter = GetComponent<SoundEmitter>();
    }

    public void Start()
    {
        _paremeterId = _soundEmitter.GetParameterId("button", "Main Menu Type");
    }

    public void StartGame()
    {
        _soundEmitter.SetParameterWithLabel("button", _paremeterId, "Positive", true);
        StartCoroutine(LoadScene("World"));
    }

    public void Playground()
    {
        _soundEmitter.SetParameterWithLabel("button", _paremeterId, "Positive", true);
        StartCoroutine(LoadScene("Playground"));
    }

    public void QuitGame()
    {
        _soundEmitter.SetParameterWithLabel("button", _paremeterId, "Positive", true);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
