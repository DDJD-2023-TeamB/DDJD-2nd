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
        ChangeStatus(Status.Active);
        this.currentQuest = 0;
    } 
    public void EndMission()
    {
        ChangeStatus(Status.Completed);

    } 
    public void ChangeStatus(Status status)
    {
        this.status = status;

    } 
    public void NextQuest()
    {
        this.currentQuest++;
        this.allQuests[this.currentQuest].Start();
    }
}
