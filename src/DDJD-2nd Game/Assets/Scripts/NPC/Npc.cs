using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Npc : Interactable
{
    [SerializeField]
    private DialogueInfo _defaultDialogueInfo;

    private Dialogue _dialogue;
    private DialogueInfo _currentDialogueInfo;

    public Mission2 _mission;

    protected override void Start()
    {
        base.Start();
        _dialogue = _player.Dialogue;
        _currentDialogueInfo = _defaultDialogueInfo;
    }

    void Update() { }

    public void ChangeDialogue(DialogueInfo newDialogueInfo)
    {
        _currentDialogueInfo = newDialogueInfo;
    }

    public override void Interact()
    {
        if(_mission != null)
        {
            if( _mission.Status == MissionState.Blocked)
            {
                _currentDialogueInfo = _defaultDialogueInfo;
            }
            if( _mission.Status == MissionState.Available)
            {
                _currentDialogueInfo = _mission.DialogueBegin;
                _mission.Status = MissionState.Ongoing;
            }
            else if( _mission.Status == MissionState.Completed)
            {
                _currentDialogueInfo = _mission.DialogueEnd;
            }
            _dialogue.StartDialogue(_currentDialogueInfo);
        }
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
