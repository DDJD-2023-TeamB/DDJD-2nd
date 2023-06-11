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
    private Queue<Mission2> _missions = new Queue<Mission2>();
    public Queue<Mission2> Missions
    {
        get { return _missions; }
    }

    private Mission2 _currentMission;

    protected override void Start()
    {
        base.Start();
        _dialogue = _player.UIController.PlayerUI.Dialogue;
        _currentDialogueInfo = _npc.DefaultDialogueInfo;
        _missions = _missionController.GetNpcMissions(_npc);
        if (_missions.Count != 0)
            _currentMission = _missions.Dequeue();
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
        _missionController.CheckIfNpcIsMyGoal(_npc);

        if (_currentMission != null)
        {
            if (_currentMission.Status == MissionState.Blocked)
            {
                _currentDialogueInfo = _npc.DefaultDialogueInfo;
            }
            if (_currentMission.Status == MissionState.Available)
            {
                if (_npc == _currentMission.InteractionBegin.Npc)
                {
                    _currentDialogueInfo = _currentMission.InteractionBegin.DialogueInfo;
                    _currentMission.Status = MissionState.Ongoing;
                }
            }
            else if (_currentMission.Status == MissionState.Completed)
            {
                if (_npc == _currentMission.InteractionBegin.Npc)
                    _currentDialogueInfo = _npc.DefaultDialogueInfo;
                //_currentDialogueInfo = _currentMission.InteractionEnd.DialogueInfo;
                if (_missions.Count > 0)
                {
                    _currentMission = _missions.Dequeue();
                }
                else
                {
                    _currentMission = null;
                }
            }
        }
        _player.UIController.PlayerUI.Dialogue.StartDialogue(_currentDialogueInfo);
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
