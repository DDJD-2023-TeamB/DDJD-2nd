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

    private List<Mission> generalMissions;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
        _missionController = _player.GetComponent<MissionController>();
        ColorUtility.TryParseHtmlString("#FED600", out highlightColor);
        loadActiveMissions();
        setActiveMission(null);
    }

    void loadActiveMissions()
    {
        generalMissions = _missionController.GetAvailableAndOngoingMissions();
        //Temporary code for testing, should later be loaded from a game controller
        /*Mission m1 = new Mission("Mission 1 idoajiaskl", "Mission 1 descriptionjdwankjndajkwdnkjasnkjnakwjnkasdnawnkdna");
        m1.addQuest(new Quest(0, "Quest 1"));
        m1.addQuest(new Quest(0, "Quest 2"));
        m1.addQuest(new Quest(1, "Quest 2"));
        Mission m2 = new Mission(
            "Mission 2 idoajiaskl",
            "Mission 2 descriptionjdwankjndajkwdnkjasnkjnakwjnkasdnawnkdna"
        );
        m2.addQuest(new Quest(0, "Quest 1"));
        m2.addQuest(new Quest(1, "Quest 2"));
        Mission m3 = new Mission(
            "Mission 3 idoajiaskl",
            "Mission 3 descriptionjdwankjndajkwdnkjasnkjnakwjnkasdnawnkdna"
        );
        m3.addQuest(new Quest(1, "Quest 1"));
        Mission m4 = new Mission(
            "Mission 4 idoajiaskl",
            "Mission 4 descriptionjdwankjndajkwdnkjasnkjnakwjnkasdnawnkdna"
        );
        m4.addQuest(new Quest(0, "Quest 1"));
        m4.addQuest(new Quest(0, "Quest 2"));
        m4.addQuest(new Quest(0, "Quest 3"));
        m4.addQuest(new Quest(1, "Quest 4"));
        m4.addQuest(new Quest(0, "Quest 5"));
        m4.addQuest(new Quest(2, "Quest 6"));
        m4.addQuest(new Quest(1, "Quest 7"));
        Mission[] missions = { m1, m2, m3, m4 };
        generalMissions = missions;*/
    }

    void updateMissionsUI()
    {
        foreach (Transform child in missionsContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        int accumulatedQuests = 0;

        for (int i = 0; i < generalMissions.Count; i++)
        {
            Mission mission = generalMissions[i];
            GameObject missionInstance = Instantiate(missionPrefab, missionsContent.transform);
            missionInstance.GetComponent<MissionSelectionScript>().mission = mission;
            missionInstance.transform.Find("MissionTitle").GetComponent<TextMeshProUGUI>().text =
                mission.Title;
            missionInstance.transform
                .Find("MissionDescription")
                .GetComponent<TextMeshProUGUI>()
                .text = mission.Description;
            if (mission == selectedMission)
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
            for (int j = 0; j < mission.Goals.Count; j++)
            {
                var goal = mission.Goals[j];
                accumulatedQuests++;
                GameObject questInstance = Instantiate(questPrefab, missionInstance.transform);
                if (mission == selectedMission)
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
            }
        }
        missionsContent.GetComponent<RectTransform>().sizeDelta = new Vector2(
            1600,
            170 * generalMissions.Count + 40 * accumulatedQuests
        );
    }

    public void setActiveMission(Mission mission)
    {
        if (selectedMission == null && mission == null)
        {
            playingIndicatorTitle.GetComponent<TextMeshProUGUI>().text = "";
            playingIndicatorQuest.GetComponent<TextMeshProUGUI>().text = "";
        }
        else if (selectedMission != null && mission == selectedMission || mission == null) //Deselect mission
        {
            selectedMission = null;
            playingIndicatorTitle.GetComponent<TextMeshProUGUI>().text = "";
            playingIndicatorQuest.GetComponent<TextMeshProUGUI>().text = "";
        }
        else //Select new mission
        {
            selectedMission = mission;
            playingIndicatorTitle.GetComponent<TextMeshProUGUI>().text = selectedMission.Title;
            foreach (var goal in selectedMission.Goals)
            {
                if (!goal.Completed)
                {
                    playingIndicatorQuest.GetComponent<TextMeshProUGUI>().text = goal.Description;
                    break;
                }
            }
        }
        updateMissionsUI();
        //Set active mission in game controller
    }
}
