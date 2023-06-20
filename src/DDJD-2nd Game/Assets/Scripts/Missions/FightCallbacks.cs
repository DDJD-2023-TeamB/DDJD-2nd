using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCallbacks : MonoBehaviour
{
    public void OnFightGoalStarted(string enemyCampName)
    {
        EnemyCamp enemyCamp = GameObject.Find(enemyCampName).GetComponent<EnemyCamp>();
        enemyCamp.OnFightGoalStarted();
    }

    public void OnFightGoalCompleted(string enemyCampName)
    {
        EnemyCamp enemyCamp = GameObject.Find(enemyCampName).GetComponent<EnemyCamp>();
        enemyCamp.OnFightGoalCompleted();
    }
}
