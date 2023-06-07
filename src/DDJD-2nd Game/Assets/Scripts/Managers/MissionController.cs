using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    //TODO AMANHA: conjunto de unblocked missões - Vindo do game state
    [SerializeField]
    private Mission2 _currentMission;

    void Start()
    {
        //TODO: iteraterar sobre as missions e defina-las como availables
        _currentMission.Status = MissionState.Available;
    }

    void Update()
    {
        //se os há objectivos a serem cumpridos
    }

    public void CheckIfNpcIsMyGoal(NpcObject npc)
    {
        if (_currentMission.Status == MissionState.Ongoing)
        {
            foreach (var goal in _currentMission.Goals)
            {
                if (!goal._completed && goal is InteractGoal interactGoal)
                {
                    if (interactGoal.NpcToInteract == npc)
                    {
                        goal._completed = true;
                        Debug.Log("Interact Goal Completed");
                    }
                }
            }
            CheckIfAllGoalsAreCompleted();
        }
       
    }

    public void CheckIfItemCollectedIsMyGoal(CollectibleObject collectible)
    {
        if (_currentMission.Status == MissionState.Ongoing)
        {
            foreach (var goal in _currentMission.Goals)
            {
                if (!goal._completed && goal is CollectGoal collectGoal)
                {
                    if (collectGoal.CollectibleToCollect == collectible)
                    {
                        collectGoal.Quantity -= 1;
                        if(collectGoal.Quantity == 0) goal._completed = true;
                        Debug.Log("Collect Goal Completed");
                    }
                }
            }
            CheckIfAllGoalsAreCompleted();
        }
       
    }

    public void CheckIfAllGoalsAreCompleted()
    {
        bool allGoalsCompleted = false;
        foreach (var goal in _currentMission.Goals)
        {
            if (!goal._completed)
            {
                break;
            }
            else if(!allGoalsCompleted){
                allGoalsCompleted = true;
            }
        }

        if (allGoalsCompleted)
            _currentMission.Status = MissionState.Completed;
    }

    /*public void CompleteGoal(Goal goal)
    {
        if (currentMission != null && currentMission.Status == MissionState.Ongoing)
        {
            currentMission.CompleteGoal(goal);

            if (currentMission.AreGoalsComplete())
            {
                currentMission.Complete();
                currentMission = null;
            }
        }
    }*/
}
