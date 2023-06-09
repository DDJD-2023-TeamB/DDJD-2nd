using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    private List<Mission2> _unblockedMissions = new List<Mission2>();

    [SerializeField]
    private GameState _gameState;

    private Player _player;

    void Start()
    {
        ActivateMissions();
        _player = GetComponent<Player>();
        _unblockedMissions = _gameState.UnblockedMissions;
    }

    private void ActivateMissions()
    {
        foreach (var mission in _unblockedMissions)
        {
            if(mission.Status == MissionState.Blocked)
            {
                mission.Status = MissionState.Available;
            }
        }
    }
    public void CheckIfNpcIsMyGoal(NpcObject npc)
    {
        List<Mission2> missions = new List<Mission2>(_unblockedMissions);

        foreach (var mission in missions)
        {
            if (mission.Status == MissionState.Ongoing)
            {
                Debug.Log(mission);
                foreach (var goal in mission.Goals)
                {
                    Debug.Log(goal);
                    if (!goal._completed && goal is InteractGoal interactGoal)
                    {
                        if (interactGoal.NpcToInteract == npc)
                        {
                            Debug.Log("SAME");
                            goal._completed = true;
                            Debug.Log("Interact Goal Completed");
                        }
                    }
                }
                CheckIfAllGoalsAreCompleted(mission);
            }
        }
       
    }

    public void CheckIfItemCollectedIsMyGoal(CollectibleObject collectible)
    {
        List<Mission2> missions = new List<Mission2>(_unblockedMissions);

        foreach (var mission in missions)
        {
            if (mission.Status == MissionState.Ongoing)
            {
                foreach (var goal in mission.Goals)
                {
                    if (!goal._completed && goal is CollectGoal collectGoal)
                    {
                        if (collectGoal.CollectibleToCollect == collectible)
                        {
                            if(collectGoal.Quantity > 0) collectGoal.Quantity -= 1;
                            if (collectGoal.Quantity == 0) goal._completed = true;
                            Debug.Log("Collect Goal Completed");
                        }
                    }
                }
                CheckIfAllGoalsAreCompleted(mission);
            }
        }
       
    }

    public void CheckIfAllGoalsAreCompleted(Mission2 mission)
    {
        bool allGoalsCompleted = false;
        foreach (var goal in mission.Goals)
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
        {
            HandleMissionComplete(mission);
        }
            
    }

    private void HandleMissionComplete(Mission2 mission)
    {
        mission.Status = MissionState.Completed;
        GiveReward(mission);
        _unblockedMissions.Remove(mission);
        UnblockFollowingMissions(mission);
    }

    private void UnblockFollowingMissions(Mission2 mission)
    {
        foreach (var followingMissions in mission.FollowingMissions)
        {
            followingMissions.Status = MissionState.Available;
            _unblockedMissions.Add(followingMissions);
        }
    }

    private void GiveReward(Mission2 mission)
    {
        foreach (var item in mission.Reward.Items)
        {
            _player.Inventory.AddItem(item);
        }

        //_player.Inventory.AddGold(mission.Reward.Gold);
    }

    public Queue<Mission2> GetNpcMissions(NpcObject npc)
    {
        Queue<Mission2> missions = new Queue<Mission2>();

        foreach (var mission in _unblockedMissions)
        {
            if(mission.Status != MissionState.Completed)
            {
                if(mission.InteractionBegin.Npc == npc || mission.InteractionEnd.Npc == npc)
                {
                    missions.Enqueue(mission);
                }
            }
            foreach(var followingMission in mission.FollowingMissions)
            {
                if(followingMission.InteractionBegin.Npc == npc || followingMission.InteractionEnd.Npc == npc)
                {
                    missions.Enqueue(followingMission);
                }
            }
        }

        return missions;
    }
}
