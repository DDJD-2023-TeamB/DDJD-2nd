using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    //conjunto de missões
    [SerializeField]
    public Mission2 currentMission;
    void Start()
    {
        currentMission.Status = MissionState.Available;
        currentMission.Npc.GetComponent<Npc>()._mission = currentMission;
    }

    public void initNpc()
    {
        
    }

    /*public void StartMission(Mission2 mission)
    {
        if (mission.Status == MissionState.Available)
        {
            currentMission = mission;
            currentMission.Start();
        }
    }

    public void CompleteGoal(Goal goal)
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