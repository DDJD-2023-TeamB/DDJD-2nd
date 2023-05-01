using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Quest : MonoBehaviour
{

    public Image questItem;
    public Color completedColor;
    public Color activeColor;
    public Color currentColor;

    public Quest[] allQuests;

    private void Start()
    {
        allQuests = FindObjectsOfType<Quest>();
    }

    private void OnTriggerEnter(Collider collider)
    {
  
        if(collider.CompareTag("Player"))
        {
            FinishQuest();
            Destroy(gameObject);
        }
    }

    void FinishQuest(){
        questItem.GetComponent<Button>().interactable = false;
        currentColor = completedColor;
        questItem.color = completedColor;
       // arrow.gameObject.SetActive(false);
    }
    public void OnQuestClick()
    {
        //arrow.gameObject.SetActive(true);
        //arrow.target = this.transform;
        foreach(Quest quest in allQuests)
        {
            quest.questItem.color = quest.currentColor;
        }
        questItem.color = activeColor;
    }
}