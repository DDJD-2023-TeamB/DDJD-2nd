using JetBrains.Annotations;
using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class MissionsUIController : MonoBehaviour
{
    public GameObject missionPrefab;
    public GameObject questPrefab;
    public GameObject missionsContent;

    public GameObject playingIndicatorTitle;
    public GameObject playingIndicatorQuest;

    public Sprite attackQuestIcon;
    public Sprite gatherQuestIcon;
    public Sprite searchQuestIcon;

    private Player _player;

    private MissionController _missionController;

    Mission selectedMission = null;
    Color highlightColor;
    Color completedColor;

    private List<Mission> generalMissions = new List<Mission>();

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
        _missionController = _player.GetComponent<MissionController>();
        ColorUtility.TryParseHtmlString("#FED600", out highlightColor);
        ColorUtility.TryParseHtmlString("#737373", out completedColor);
        LoadActiveMissions();
        SetActiveMission(null);
        _missionController.SetMissionsUIController(this);
    }

    void LoadActiveMissions()
    {
        List<Mission> activeMissions = _missionController.GetAvailableAndOngoingMissions();
        foreach (Mission mission in activeMissions)
        {
            if (!generalMissions.Contains(mission))
            {
                generalMissions.Add(mission);
            }
        }
    }

    public void UpdateMissionsUI()
    {
        LoadActiveMissions();
        foreach (Transform child in missionsContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        int accumulatedQuests = 0;

        List<Mission> orderedGeneralMissions = new List<Mission>();

        for (int i = 0; i < generalMissions.Count; i++)
        {
            if (generalMissions[i].Status == MissionState.Available)
            {
                orderedGeneralMissions.Add(generalMissions[i]);
            }
        }
        for (int i = 0; i < generalMissions.Count; i++)
        {
            if (generalMissions[i].Status == MissionState.Ongoing)
            {
                orderedGeneralMissions.Add(generalMissions[i]);
            }
        }
        for (int i = 0; i < generalMissions.Count; i++)
        {
            if (generalMissions[i].Status == MissionState.Completed)
            {
                orderedGeneralMissions.Add(generalMissions[i]);
            }
        }

        for (int i = 0; i < orderedGeneralMissions.Count; i++)
        {
            Mission mission = orderedGeneralMissions[i];
            if (mission.Status == MissionState.Blocked)
            {
                continue;
            }
            GameObject missionInstance = Instantiate(missionPrefab, missionsContent.transform);
            missionInstance.GetComponent<MissionSelectionScript>().mission = mission;
            missionInstance.transform.Find("MissionTitle").GetComponent<TextMeshProUGUI>().text =
                mission.Title;
            missionInstance.transform
                .Find("MissionDescription")
                .GetComponent<TextMeshProUGUI>()
                .text = mission.Description;
            if (mission.Status == MissionState.Available)
            {
                missionInstance.GetComponent<MissionSelectionScript>().setAsNew(true);
            }

            if (mission.Status == MissionState.Completed)
            {
                missionInstance.transform
                    .Find("MissionTitle")
                    .GetComponent<TextMeshProUGUI>()
                    .color = completedColor;
                missionInstance.transform
                    .Find("MissionDescription")
                    .GetComponent<TextMeshProUGUI>()
                    .color = completedColor;
            }
            else if (mission == selectedMission)
            {
                missionInstance.transform
                    .Find("MissionTitle")
                    .GetComponent<TextMeshProUGUI>()
                    .color = highlightColor;
                missionInstance.transform
                    .Find("MissionDescription")
                    .GetComponent<TextMeshProUGUI>()
                    .color = highlightColor;
            }
            missionInstance.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
            missionInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                0,
                -170 * i - 40 * accumulatedQuests
            );
            missionInstance.GetComponent<RectTransform>().sizeDelta = new Vector2(
                1600,
                160 + 40 * mission.Goals.Count
            );

            if (mission.Status == MissionState.Available) // Don't display goals if before starting
                continue;

            for (int j = 0; j < mission.Goals.Count; j++)
            {
                var goal = mission.Goals[j];
                accumulatedQuests++;
                GameObject questInstance = Instantiate(questPrefab, missionInstance.transform);
                if (mission.Goals[j].Completed)
                {
                    questInstance.GetComponent<TextMeshProUGUI>().color = completedColor;
                }
                else if (mission == selectedMission)
                {
                    questInstance.GetComponent<TextMeshProUGUI>().color = highlightColor;
                }
                questInstance.GetComponent<TextMeshProUGUI>().text = goal.Description;
                questInstance.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
                questInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    50,
                    -150 - 40 * j
                );
                if (goal is FightGoal)
                {
                    questInstance.transform.Find("QuestTypeIcon").GetComponent<Image>().sprite =
                        attackQuestIcon;
                }
                else if (goal is CollectGoal)
                {
                    questInstance.transform.Find("QuestTypeIcon").GetComponent<Image>().sprite =
                        gatherQuestIcon;
                }
                else if (goal is InteractGoal)
                {
                    questInstance.transform.Find("QuestTypeIcon").GetComponent<Image>().sprite =
                        searchQuestIcon;
                }
                else
                {
                    Debug.LogError("Invalid quest type!");
                }

                if (goal == mission.CurrentGoal)
                    break;
            }
        }
        missionsContent.GetComponent<RectTransform>().sizeDelta = new Vector2(
            1600,
            170 * orderedGeneralMissions.Count + 40 * accumulatedQuests
        );

        if (selectedMission != null)
            UpdatedSelectedMission(selectedMission);
    }

    public void SetActiveMission(Mission mission)
    {
        if (selectedMission == null && mission == null)
        {
            playingIndicatorTitle.GetComponent<TextMeshProUGUI>().text = "";
            playingIndicatorQuest.GetComponent<TextMeshProUGUI>().text = "";
        }
        // Deselect mission
        else if (selectedMission != null && mission == selectedMission || mission == null)
        {
            selectedMission = null;
            playingIndicatorTitle.GetComponent<TextMeshProUGUI>().text = "";
            playingIndicatorQuest.GetComponent<TextMeshProUGUI>().text = "";
        }
        else if (mission.Status != MissionState.Completed)
        {
            UpdatedSelectedMission(mission);
        }
        UpdateMissionsUI();
    }

    private void UpdatedSelectedMission(Mission mission)
    {
        if (mission.Status == MissionState.Completed)
        {
            playingIndicatorTitle.GetComponent<TextMeshProUGUI>().text = "";
            playingIndicatorQuest.GetComponent<TextMeshProUGUI>().text = "";
            return;
        }

        selectedMission = mission;
        playingIndicatorTitle.GetComponent<TextMeshProUGUI>().text = selectedMission.Title;
        if (selectedMission.CurrentGoal != null && selectedMission.Status == MissionState.Ongoing)
        {
            playingIndicatorQuest.GetComponent<TextMeshProUGUI>().text = selectedMission
                .CurrentGoal
                .Description;
        }
    }
}
