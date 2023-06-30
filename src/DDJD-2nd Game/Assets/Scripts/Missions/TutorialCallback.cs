using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCallback : MonoBehaviour
{
    private Player _player;
    private Tutorial _tutorial = null;

    public void HandleTutorial(Tutorial tutorial)
    {
        if ((_tutorial == null && tutorial != null) || !_tutorial.Started)
        {
            _tutorial = tutorial;
            _tutorial.Started = true;
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            _player.UIController.HandleTutorial(true, tutorial);
        }
        else
        {
            SwitchPageInTutorial();
        }
    }

    private void SwitchPageInTutorial()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        if (_tutorial)
        {
            _tutorial.SwitchPage();
            UIController uiController = _player.UIController;
            uiController.ChangeTutorialPage(_tutorial);
        }
    }

    public void ExitTutorial()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        if (_tutorial)
        {
            _tutorial = null;
            _player.UIController.OpenTutorial(false);
        }
    }
}
