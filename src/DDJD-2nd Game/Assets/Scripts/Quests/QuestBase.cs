/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName="Quests/Create a new quest")]
public class QuestBase : ScritableObject
{
    [SerializeField] string name;

    [SerializeField] string description;

    [SerializeField] Dialog startDialog;
    [SerializeField] Dialog inProgressDialog;
    [SerializeField] Dialog completeDialogue;

    [SerializeField] ItemBase requireItem;
    [SerializeField] ItemBase rewardItem;

    public string Name => name;
    public string Description => startDialog;

    public Dialog StartDialogue => startDialog;
    public Dialog InProgressDialogue => inProgressDialog?.Lines?.Count > 0 ? inProgressDialog : startDialog
    public Dialog CompleteDialogue => completeDialogue;

    public ItemBase RequireItem => requireItem;
    public ItemBase RewardItem => rewardItem;
}*/
