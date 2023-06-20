using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCallback : MonoBehaviour
{
    private Player _player;
    private Tutorial _tutorial;

    public void HandleTutorial(Tutorial tutorial)
    {
        Debug.Log("HandleTutorial");
        Debug.Log(tutorial);
        Debug.Log(_tutorial);
        if (_tutorial == null && tutorial != null)
        {
            _tutorial = tutorial;
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
        if (_tutorial)
        {
            _tutorial.SwitchPage();
            _player.UIController.ChangeTutorialPage(_tutorial);
        }
    }

    public void ExitTutorial()
    {
        if (_tutorial)
        {
            _tutorial = null;
            _player.UIController.OpenTutorial(false);
        }
    }

      
}
