using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuUI : MonoBehaviour
{
    private SoundEmitter _soundEmitter;

    private FMOD.Studio.PARAMETER_ID _menuParameterId;

    private void Awake()
    {
        _soundEmitter = GetComponent<SoundEmitter>();
    }

    private void Start()
    {
        _menuParameterId = _soundEmitter.GetParameterId("menu", "Main Menu Type");
    }

    public void ClickButton()
    {
        _soundEmitter.SetParameterWithLabel("menu", _menuParameterId, "Positive", true);
    }

    // Start is called before the first frame update

    public void resumeGame()
    {
        //TODO: UNPAUSE THE GAME
        _soundEmitter.SetParameterWithLabel("menu", _menuParameterId, "Positive", true);
        gameObject.SetActive(false);
    }

    public void quitGame()
    {
        //TODO: SAVE GAME STATE
        _soundEmitter.SetParameterWithLabel("menu", _menuParameterId, "Positive", true);
        Application.Quit();
    }

    public void mainMenu()
    {
        _soundEmitter.SetParameterWithLabel("menu", _menuParameterId, "Positive", true);
        SceneManager.LoadSceneAsync("Menu");
    }
}
