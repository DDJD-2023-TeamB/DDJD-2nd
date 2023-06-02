using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    // Start is called before the first frame update

    public void resumeGame()
    {
        //TODO: UNPAUSE THE GAME

        gameObject.SetActive(false);
    }

    public void openOptions()
    {
        //TODO: OPEN OPTIONS
        Debug.Log("Openning options");
    }


    public void quitGame()
    {
        //TODO: SAVE GAME STATE

        Application.Quit();
    }
}
