using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Npc : Interactable
{
    [SerializeField]
    private NpcObject _npc;

    private Dialogue _dialogue;
    private DialogueInfo _currentDialogueInfo;

    private MissionController _missionController;

    protected override void Start()
    {
        base.Start();
        _dialogue = _player.Dialogue;
        _currentDialogueInfo = _npc.DefaultDialogueInfo;
        _missionController = _player.GetComponent<MissionController>();
    }

    void Update() { }

    public void ChangeDialogue(DialogueInfo newDialogueInfo)
    {
        _currentDialogueInfo = newDialogueInfo;
    }

    public override void Interact()
    {
        _missionController.CheckIfNpcIsMyGoal(_npc);

        if (_npc.Mission != null)
        {
            if (_npc.Mission.Status == MissionState.Blocked)
            {
                _currentDialogueInfo = _npc.DefaultDialogueInfo;
            }
            if (_npc.Mission.Status == MissionState.Available)
            {
                if(_npc == _npc.Mission.InteractionBegin.Npc)
                {
                    _currentDialogueInfo = _npc.Mission.InteractionBegin.DialogueInfo;
                    _npc.Mission.Status = MissionState.Ongoing;
                }
            }
            else if (_npc.Mission.Status == MissionState.Completed)
            {
                if(_npc == _npc.Mission.InteractionEnd.Npc) _currentDialogueInfo = _npc.Mission.InteractionEnd.DialogueInfo;
            }
        }
        _dialogue.StartDialogue(_currentDialogueInfo);
    }

    public void ContinueInteraction()
    {
        if (!_dialogue.CheckIfDialogueEnded())
            _dialogue.DisplayNextSentence();
        else
        {
            _dialogue.EndDialogue();
            base.EndInteract();
        }
    }

    public override void EndInteract()
    {
        base.EndInteract();
        //TODO:: Add animation to the npc
    }
}
