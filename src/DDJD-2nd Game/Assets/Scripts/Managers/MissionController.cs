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

    private Mission _selectedMission;
    public Mission SelectedMission
    {
        get { return _selectedMission; }
        set { _selectedMission = value; }
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

    public void CheckIfNpcIsMyGoal(NpcObject npc, Mission mission)
    {
        if (
            mission.Status == MissionState.Ongoing
            && mission.CurrentGoal is InteractGoal interactGoal
            && !mission.CurrentGoal.Completed
            && interactGoal.Interaction.Npc == npc
        )
        {
            GoalCompleted(mission);
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
        Debug.Log("All goals completed: " + allGoalsCompleted);
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
        foreach (var followingMission in mission.FollowingMissions)
        {
            followingMission.Unblock();
            _unblockedMissions.Add(followingMission);
        }
    }

    private void GiveReward(Mission mission)
    {
        foreach (var item in mission.Reward.Items)
        {
            _player.Inventory.AddItem(item);
        }

        _player.Inventory.AddGold(mission.Reward.Gold);
    }

    public List<Mission> GetNpcMissions(NpcObject npc, bool repeatInteractionBegin = true)
    {
        List<Mission> missions = new List<Mission>();

        foreach (var mission in _unblockedMissions)
        {
            {
                switch (mission.Status)
                {
                    case MissionState.Available:
                        if (mission.InteractionBegin.Npc == npc)
                        {
                            missions.Add(mission);
                        }
                        break;
                    case MissionState.Ongoing:
                        if (
                            mission.CurrentGoal is InteractGoal interactGoal
                            && interactGoal.Interaction.Npc == npc
                        )
                        {
                            missions.Add(mission);
                        }
                        else if (mission.InteractionBegin.Npc == npc && repeatInteractionBegin)
                            missions.Add(mission);
                        break;
                }
            }
        }

        // Put selected mission in first place to give it priority
        if (_selectedMission != null && missions.Remove(_selectedMission))
        {
            missions.Insert(0, _selectedMission);
        }

        return missions;
    }

    public void CompleteFightGoal(EnemySpawner _enemySpawner)
    {
        foreach (Mission mission in _unblockedMissions)
        {
            if (
                mission.Status != MissionState.Ongoing
                || mission.CurrentGoal is not FightGoal fightGoal
            )
            {
                continue;
            }
            if (fightGoal.EnemySpawner == _enemySpawner)
            {
                GoalCompleted(mission);
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
