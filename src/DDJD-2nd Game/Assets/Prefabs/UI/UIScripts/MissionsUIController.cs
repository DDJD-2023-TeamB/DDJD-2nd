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

    Mission selectedMission = null;
    Color highlightColor;

    Mission[] generalMissions;

    // Start is called before the first frame update
    void Start()
    {

        ColorUtility.TryParseHtmlString("#FED600", out highlightColor);
        loadActiveMissions();
        setActiveMission(-1);
    }

    void loadActiveMissions()
    {
        //Temporary code for testing, should later be loaded from a game controller
        Mission m1 = new Mission("Mission 1 idoajiaskl", "Mission 1 descriptionjdwankjndajkwdnkjasnkjnakwjnkasdnawnkdna");
        m1.addQuest(new Quest(0, "Quest 1"));
        m1.addQuest(new Quest(0, "Quest 2"));
        m1.addQuest(new Quest(1, "Quest 2"));
        Mission m2 = new Mission("Mission 2 idoajiaskl", "Mission 2 descriptionjdwankjndajkwdnkjasnkjnakwjnkasdnawnkdna");
        m2.addQuest(new Quest(0, "Quest 1"));
        m2.addQuest(new Quest(1, "Quest 2"));
        Mission m3 = new Mission("Mission 3 idoajiaskl", "Mission 3 descriptionjdwankjndajkwdnkjasnkjnakwjnkasdnawnkdna");
        m3.addQuest(new Quest(1, "Quest 1"));
        Mission m4 = new Mission("Mission 4 idoajiaskl", "Mission 4 descriptionjdwankjndajkwdnkjasnkjnakwjnkasdnawnkdna");
        m4.addQuest(new Quest(0, "Quest 1"));
        m4.addQuest(new Quest(0, "Quest 2"));
        m4.addQuest(new Quest(0, "Quest 3"));
        m4.addQuest(new Quest(1, "Quest 4"));
        m4.addQuest(new Quest(0, "Quest 5"));
        m4.addQuest(new Quest(2, "Quest 6"));
        m4.addQuest(new Quest(1, "Quest 7"));
        Mission[] missions = { m1, m2, m3, m4 };
        generalMissions = missions;
    }

    void updateMissionsUI()
    {
        foreach (Transform child in missionsContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        int accumulatedQuests = 0;

        for (int i = 0; i < generalMissions.Length; i++)
        {
            GameObject missionInstance = Instantiate(missionPrefab, missionsContent.transform);
            missionInstance.GetComponent<MissionSelectionScript>().missionID = generalMissions[i].missionID;
            missionInstance.transform.Find("MissionTitle").GetComponent<TextMeshProUGUI>().text = generalMissions[i].missionTitle;
            missionInstance.transform.Find("MissionDescription").GetComponent<TextMeshProUGUI>().text = generalMissions[i].missionDescription;
            if (generalMissions[i] == selectedMission)
            {
                missionInstance.transform.Find("MissionTitle").GetComponent<TextMeshProUGUI>().color = highlightColor;
                missionInstance.transform.Find("MissionDescription").GetComponent<TextMeshProUGUI>().color = highlightColor;

            }
            missionInstance.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
            missionInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -170 * i - 40 * accumulatedQuests);
            missionInstance.GetComponent<RectTransform>().sizeDelta = new Vector2(1600,160 + 40 * generalMissions[i].missionQuests.Length);
            for (int j = 0; j< generalMissions[i].missionQuests.Length; j++)
            {
                accumulatedQuests++;
                GameObject questInstance = Instantiate(questPrefab,missionInstance.transform);
                if (generalMissions[i] == selectedMission)
                {
                    questInstance.GetComponent<TextMeshProUGUI>().color = highlightColor;
                }
                questInstance.GetComponent<TextMeshProUGUI>().text = generalMissions[i].missionQuests[j].questDescription;
                questInstance.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
                questInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(50, -150 - 40 * j);
                if (generalMissions[i].missionQuests[j].questType == 0)
                {
                    questInstance.transform.Find("QuestTypeIcon").GetComponent<Image>().sprite = attackQuestIcon;
                }else if(generalMissions[i].missionQuests[j].questType == 1)
                {
                    questInstance.transform.Find("QuestTypeIcon").GetComponent<Image>().sprite = gatherQuestIcon;
                }
                else if (generalMissions[i].missionQuests[j].questType == 2)
                {
                    questInstance.transform.Find("QuestTypeIcon").GetComponent<Image>().sprite = searchQuestIcon;
                }
                else
                {
                    Debug.LogError("Invalid quest type!");
                }
            }
        }
        missionsContent.GetComponent<RectTransform>().sizeDelta = new Vector2(1600, 170 * generalMissions.Length + 40 * accumulatedQuests);
    }

    Mission getMissionFromID(Mission[] missions, int id)
    {
        for(int i = 0; i < missions.Length; i++)
        {
            if (missions[i].missionID == id)
            {
                return missions[i];
            }
        }
        return null;
    }

    public void setActiveMission(int id)
    {
        if (selectedMission == null && id == -1)
        {
            selectedMission = null;
            playingIndicatorTitle.GetComponent<TextMeshProUGUI>().text = "";
            playingIndicatorQuest.GetComponent<TextMeshProUGUI>().text = "";
        }
        else if (selectedMission != null && id == selectedMission.missionID || id == -1) //Deselect mission
        {
            selectedMission = null;
            playingIndicatorTitle.GetComponent<TextMeshProUGUI>().text = "";
            playingIndicatorQuest.GetComponent<TextMeshProUGUI>().text = "";
        }
        else //Select new mission
        {
            selectedMission = getMissionFromID(generalMissions, id);
            playingIndicatorTitle.GetComponent<TextMeshProUGUI>().text = selectedMission.missionTitle;
            for (int i = 0; i < selectedMission.missionQuests.Length; i++)
            {
                if (!selectedMission.missionQuests[i].completed)
                {
                    playingIndicatorQuest.GetComponent<TextMeshProUGUI>().text = selectedMission.missionQuests[i].questDescription;
                    break;
                }
            }
        }
        updateMissionsUI();
        //Set active mission in game controller
    }

}

public class Quest
{
    public int questType;
    public bool completed = false;
    public string questDescription;
    public Quest(int type, string description)
    {
        questType = type;
        questDescription = description;
    }
}

public class Mission
{
    public Quest[] missionQuests = null;
    public string missionTitle;
    public string missionDescription;
    public int missionID;
    public static int lastMissionID;

    public Mission(string title, string description)
    {
        missionTitle = title;
        missionDescription = description;
        missionQuests = new Quest[0];
        lastMissionID++;
        missionID = lastMissionID;
    }


    public void addQuest(Quest quest)
    {
        Array.Resize<Quest>(ref missionQuests, missionQuests.Length + 1);
        missionQuests[missionQuests.Length - 1] = quest;
    }
}
