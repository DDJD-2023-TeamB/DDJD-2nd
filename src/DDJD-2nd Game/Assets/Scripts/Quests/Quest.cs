using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

// [CreateAssetMenu(fileName = "Quest", menuName = "DDJD-2nd Game/Quest", order = 0)]
public abstract class Quest: MonoBehaviour
{

    // public Image questItem;
    // public Color completedColor;
    // public Color activeColor;
    // public Color currentColor;
    public enum QuestType { Fight, Collect, Search };
    public QuestType questType;
    public enum QuestStatus { Inactive, Active, Completed };
    public QuestStatus questStatus = QuestStatus.Inactive;
    public Image questImage;

    public void Start() {
        this.questStatus = QuestStatus.Active;
    }
    public void Finish() {
        this.questStatus = QuestStatus.Completed;
    }

    public abstract bool CheckEnd();

    // private void OnTriggerEnter(Collider collider)
    // {
  
    //     if(collider.CompareTag("Player"))
    //     {
    //         FinishQuest();
    //         Destroy(gameObject);
    //     }
    // }

    // void FinishQuest(){
    //     questItem.GetComponent<Button>().interactable = false;
    //     currentColor = completedColor;
    //     questItem.color = completedColor;
    //    // arrow.gameObject.SetActive(false);
    // }
    // public void OnQuestClick()
    // {
    //     //arrow.gameObject.SetActive(true);
    //     //arrow.target = this.transform;
    //     foreach(Quest quest in allQuests)
    //     {
    //         quest.questItem.color = quest.currentColor;
    //     }
    //     questItem.color = activeColor;
    // }
}