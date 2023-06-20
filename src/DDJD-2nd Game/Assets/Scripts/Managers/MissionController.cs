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
    public MissionsUIController MissionsUIController
    {
        get { return _missionsUIController; }
    }

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
            mission.Unblock();
        }
    }

    public void CheckIfNpcIsMyGoal(NpcObject npc)
    {
        List<Mission> missions = new List<Mission>(_unblockedMissions);

        foreach (var mission in missions)
        {
            if (
                mission.Status != MissionState.Ongoing
                || mission.CurrentGoal is not InteractGoal interactGoal
                || mission.CurrentGoal.Completed
            )
                continue;

            if (interactGoal.Interaction.Npc == npc)
            {
                GoalCompleted(mission);
            }
        }
    }

    public void CheckIfItemCollectedIsMyGoal(CollectibleObject collectible)
    {
        List<Mission> missions = new List<Mission>(_unblockedMissions);

        foreach (var mission in missions)
        {
            if (
                mission.Status != MissionState.Ongoing
                || mission.CurrentGoal is not CollectGoal collectGoal
                || mission.CurrentGoal.Completed
            )
                continue;

            if (collectGoal.CollectibleToCollect == collectible)
            {
                if (collectGoal.Quantity > 0)
                {
                    collectGoal.Quantity -= 1;
                }
                else
                {
                    GoalCompleted(mission);
                }
            }
        }
    }

    public void CheckIfAllGoalsAreCompleted(Mission mission)
    {
        bool allGoalsCompleted = mission.IsCompleted();
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
        if (_missionsUIController != null)
            _missionsUIController.UpdateMissionsUI();
    }

    private void UnblockFollowingMissions(Mission mission)
    {
        foreach (var followingMissions in mission.FollowingMissions)
        {
            followingMissions.Unblock();
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

    public List<Mission> GetNpcMissions(NpcObject npc)
    {
        List<Mission> missions = new List<Mission>();

        foreach (var mission in _unblockedMissions)
        {
            {
                if (mission.Status == MissionState.Available && mission.InteractionBegin.Npc == npc)
                {
                    missions.Add(mission);
                }
                if (mission.Status == MissionState.Ongoing) {
                    if (mission.CurrentGoal is InteractGoal interactGoal) {
                        if (interactGoal.Interaction.Npc == npc) missions.Add(mission);
                        else if (mission.InteractionBegin.Npc == npc && interactGoal.Interaction.Npc != npc) missions.Add(mission);
                    }
                    else if (mission.InteractionBegin.Npc == npc) missions.Add(mission);
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
                        GoalCompleted(mission);
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

    public void GoalCompleted(Mission mission)
    {
        mission.CompleteCurrentGoal();
        CheckIfAllGoalsAreCompleted(mission);
        _missionsUIController?.UpdateMissionsUI();
    }
}
