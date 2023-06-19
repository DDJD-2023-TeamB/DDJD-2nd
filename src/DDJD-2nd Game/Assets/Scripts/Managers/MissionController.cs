using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class MissionController : MonoBehaviour
{
    public static MissionController Instance;
    private List<Mission> _unblockedMissions = new List<Mission>();

    public List<Mission> UnblockedMissions
    {
        get { return _unblockedMissions; }
    }

    [SerializeField]
    private GameState _gameState;

    private Player _player;
    private UIController _uiController;
    private MissionsUIController _missionsUIController;

    void Start()
    {
        _player = GetComponent<Player>();
        _uiController = GetComponent<UIController>();
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
                foreach (var goal in mission.Goals)
                {
                    if (!goal._completed && goal is InteractGoal interactGoal)
                    {
                        if (interactGoal.NpcToInteract == npc)
                        {
                            goal.Complete();
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
                            {
                                goal.Complete();
                            }
                                
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
        bool allGoalsCompleted = true;
        foreach (var goal in mission.Goals)
        {
            if (!goal._completed)
            {
                allGoalsCompleted = false;
                break;
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
        _uiController.showCompleteMissionText(mission.Title);
        GiveReward(mission);
        _unblockedMissions.Remove(mission);
        _gameState.FinishedMissions.Add(mission);
        UnblockFollowingMissions(mission);
        _missionsUIController.UpdateMissionsUI();
    }

    private void UnblockFollowingMissions(Mission mission)
    {
        foreach (var followingMissions in mission.FollowingMissions)
        {
            Debug.Log("UnblockFollowingMissions");
            Debug.Log(followingMissions);
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
        }

        return missions;
    }

    public void CompleteFightGoal(EnemySpawner _enemySpawner)
    {
        foreach (Mission mission in _unblockedMissions)
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
                        goal.Complete();
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

    public void SetMissionsUIController(MissionsUIController missionsUIController)
    {
        _missionsUIController = missionsUIController;
    }
}
