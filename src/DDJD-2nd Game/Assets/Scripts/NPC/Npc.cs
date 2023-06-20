using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(0)]
[System.Serializable]
public class Npc : Interactable
{
    [SerializeField]
    private NpcObject _npc;
    private Animator _animator;
    private Dialogue _dialogue;
    private DialogueInfo _currentDialogueInfo;

    protected override void Start()
    {
        base.Start();
        _currentDialogueInfo = _npc.DefaultDialogueInfo;
        _animator = GetComponent<Animator>();
    }

    void Update() { }

    public void ChangeDialogue(DialogueInfo newDialogueInfo)
    {
        _currentDialogueInfo = newDialogueInfo;
    }

    public override void Interact()
    {
        if (_dialogue == null)
        {
            _dialogue = _player.UIController.PlayerUI.Dialogue;
        }

        _currentDialogueInfo = _npc.DefaultDialogueInfo;
        foreach (var mission in _missionController.GetNpcMissions(_npc))
        {
            if (mission.Status == MissionState.Available)
            {
                _currentDialogueInfo = mission.InteractionBegin.DialogueInfo;
                mission.Status = MissionState.Ongoing;
                mission.CurrentGoal.OnGoalStarted?.Invoke(); // TODO check if interaction begin is mandatory
                _missionController.MissionsUIController.UpdateMissionsUI();
                _missionController.CheckIfAllGoalsAreCompleted(mission);
            }
            else if (
                mission.Status == MissionState.Ongoing
                && mission.CurrentGoal is InteractGoal interactGoal
            )
            {
                _currentDialogueInfo = interactGoal.Interaction.DialogueInfo;
            }
        }

        _missionController.CheckIfNpcIsMyGoal(_npc);

        _dialogue.StartDialogue(_currentDialogueInfo);
        _animator.SetInteger("Talking Index", Random.Range(0, 4));
        _animator.SetTrigger("Talking");
    }

    public void ContinueInteraction()
    {
        if (!_dialogue.CheckIfDialogueEnded())
            _dialogue.DisplayNextSentence();
        else
        {
            _dialogue.EndDialogue();
            _animator.SetInteger("Idle Index", Random.Range(0, 5));
            _animator.SetTrigger("Idle");
            base.EndInteract();
        }
    }

    public override void EndInteract()
    {
        base.EndInteract();
    }
}
