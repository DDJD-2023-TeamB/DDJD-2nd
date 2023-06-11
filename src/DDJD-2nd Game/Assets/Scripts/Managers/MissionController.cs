using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class MissionController : MonoBehaviour
{
    public static MissionController Instance;
    private List<Mission2> _unblockedMissions = new List<Mission2>();
    private List<Mission> _unblockedMissions = new List<Mission>();

    public List<Mission> UnblockedMissions
    {
        get { return _unblockedMissions; }
    }

    [SerializeField]
    private GameState _gameState;

    private Player _player;

    void Start()
    {
        _player = GetComponent<Player>();
        _unblockedMissions = _gameState.UnblockedMissions;
        Instance = this;
        ActivateMissions();
    }

    private void ActivateMissions()
    {
        foreach (var mission in _unblockedMissions)
        {
            if (mission.Status == MissionState.Blocked)
            {
                mission.Status = MissionState.Available;
            }
        }
    }

    public void CheckIfNpcIsMyGoal(NpcObject npc)
    {
        List<Mission> missions = new List<Mission>(_unblockedMissions);

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
        List<Mission> missions = new List<Mission>(_unblockedMissions);

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
                            if (collectGoal.Quantity > 0)
                                collectGoal.Quantity -= 1;
                            if (collectGoal.Quantity == 0)
                                goal._completed = true;
                            Debug.Log("Collect Goal Completed");
                        }
                    }
                }
                CheckIfAllGoalsAreCompleted(mission);
            }
        }
    }

    public void CheckIfAllGoalsAreCompleted(Mission mission)
    {
        bool allGoalsCompleted = false;
        foreach (var goal in mission.Goals)
        {
            if (!goal._completed)
            {
                break;
            }
            else if (!allGoalsCompleted)
            {
                allGoalsCompleted = true;
            }
        }

        if (allGoalsCompleted)
        {
            HandleMissionComplete(mission);
        }
    }

    private void HandleMissionComplete(Mission mission)
    {
        mission.Status = MissionState.Completed;
        GiveReward(mission);
        _unblockedMissions.Remove(mission);
        UnblockFollowingMissions(mission);
    }

    private void UnblockFollowingMissions(Mission mission)
    {
        foreach (var followingMissions in mission.FollowingMissions)
        {
            followingMissions.Status = MissionState.Available;
            _unblockedMissions.Add(followingMissions);
        }
    }

    private void GiveReward(Mission mission)
    {
        foreach (var item in mission.Reward.Items)
        {
            _player.Inventory.AddItem(item);
        }

        //_player.Inventory.AddGold(mission.Reward.Gold);
    }

    public Queue<Mission> GetNpcMissions(NpcObject npc)
    {
        Queue<Mission> missions = new Queue<Mission>();

        foreach (var mission in _unblockedMissions)
        {
            if (mission.Status != MissionState.Completed)
            {
                if (mission.InteractionBegin.Npc == npc || mission.InteractionEnd.Npc == npc)
                {
                    missions.Enqueue(mission);
                }
            }
            /*
            foreach (var followingMission in mission.FollowingMissions)
            {
                if (
                    followingMission.InteractionBegin.Npc == npc
                    || followingMission.InteractionEnd.Npc == npc
                )
                {
                    missions.Enqueue(followingMission);
                }
            }
            */
        }

        return missions;
    }

    public void CompleteFightGoal(EnemySpawner _enemySpawner)
    {
        foreach (Mission2 mission in _unblockedMissions)
        {
            if (mission.Status != MissionState.Ongoing)
            {
                continue;
            }
            foreach (GoalObject goal in mission.Goals)
            {
                if (goal is FightGoal fightGoal)
                {
                    if (fightGoal.EnemySpawner == _enemySpawner)
                    {
                        goal._completed = true;
                        CheckIfAllGoalsAreCompleted(mission);
                    }
                }
            }
        }
    }

    public List<Mission> GetAvailableAndOngoingMissions()
    {
        List<Mission> availableMissions = new List<Mission>();

        foreach (var mission in _unblockedMissions)
        {
            if (mission.Status == MissionState.Available || mission.Status == MissionState.Ongoing)
                availableMissions.Add(mission);
        }

        return availableMissions;
    }
}
