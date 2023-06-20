using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[DefaultExecutionOrder(0)]
[System.Serializable]
public class Npc : Interactable
{
    [SerializeField]
    private NpcObject _npc;
    private Animator _animator;
    private Dialogue _dialogue;
    private DialogueInfo _currentDialogueInfo;

    private Mission _currentMission;

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
        foreach (var mission in _missionController.GetNpcMissions(_npc, false))
        {
            switch (mission.Status)
            {
                case MissionState.Blocked:
                {
                    _currentDialogueInfo = _npc.DefaultDialogueInfo;
                    break;
                }
                case MissionState.Available:
                {
                    _currentDialogueInfo = mission.InteractionBegin.DialogueInfo;
                    mission.Status = MissionState.Ongoing;
                    mission.CurrentGoal.OnGoalStarted?.Invoke();
                    _missionController.MissionsUIController?.UpdateMissionsUI();
                    _missionController.CheckIfAllGoalsAreCompleted(mission);
                    break;
                }
                case MissionState.Ongoing:
                {
                    if (mission.CurrentGoal is InteractGoal interactGoal)
                        _currentDialogueInfo = mission.InteractionBegin.DialogueInfo;
                    break;
                }
                // case MissionState.Completed:
                // {
                //     if (_npc == _currentMission.InteractionBegin.Npc)
                //         _currentDialogueInfo = _npc.DefaultDialogueInfo;

                //     if (_missions.Count > 0)
                //     {
                //         _currentMission = _missions.Dequeue();
                //     }
                //     else
                //     {
                //         _currentMission = null;
                //     }
                //     break;
                // }
            }
        }

        _missionController.CheckIfNpcIsMyGoal(_npc);

        _dialogue.StartDialogue(_currentDialogueInfo);
        _animator.SetInteger("Talking Index", Random.Range(0, 4));
        _animator.SetTrigger("Talking");
    }

    public void ContinueInteraction()
    {
        if (!_dialogue.CheckIfDialogueEnded()) {
            Debug.Log("Continue");
            _dialogue.DisplayNextSentence();
        }
        else
        {
            foreach (var mission in _missionController.GetNpcMissions(_npc, true))
            {
                Debug.Log(mission);
                if (
                    mission
                    && _npc == mission.InteractionBegin.Npc
                    && mission.Status == MissionState.Ongoing
                )
                {
                    _currentMission = mission;
                    mission.InteractionBegin.InteractionEnded();
                    return;
                }
            }
            EndFullInteraction(true);
        }
    }

    public void ExitInteraction()
    {
        _currentMission.InteractionBegin.Exit();
        EndFullInteraction(true);
    }

    private void EndFullInteraction(bool endFullInteraction)
    {
        if (endFullInteraction)
            base.EndInteract();
        EndInteract();
    }

    public override void EndInteract()
    {
        _dialogue.EndDialogue();
        _animator.SetInteger("Idle Index", Random.Range(0, 5));
        _animator.SetTrigger("Idle");
    }
}
