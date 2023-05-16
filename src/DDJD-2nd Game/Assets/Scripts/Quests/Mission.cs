using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{
    private Quest[] allQuests;
    private enum Status { Inactive, Active, Completed, Blocked };
    private Status status = Status.Inactive;
    private int currentQuest;

    public void StartMission()
    {
        this.status = Status.Active;
        this.currentQuest = 0;
    } 
    public void EndMission()
    {
        this.status = Status.Completed;
    } 
    public void NextQuest()
    {
        this.currentQuest++;
        this.allQuests[this.currentQuest].Start();
    }
}
