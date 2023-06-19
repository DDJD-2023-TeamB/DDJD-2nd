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
    private Queue<Mission> _missions = new Queue<Mission>();
    public Queue<Mission> Missions
    {
        get { return _missions; }
    }
    private Mission _currentMission;

    private Tutorial _currentTutorial;
    private bool _tutorial = false;

    protected override void Start()
    {
        base.Start();
        _currentDialogueInfo = _npc.DefaultDialogueInfo;
        _missions = _missionController.GetNpcMissions(_npc);
        _animator = GetComponent<Animator>();
        if (_missions.Count != 0) {
            _currentMission = _missions.Dequeue();
            if (_currentMission.Tutorial != null) _currentTutorial = (Tutorial)AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/Mission/Tutorial/" + _currentMission.Tutorial + ".asset", typeof(Tutorial));
        }
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
                if (_missions.Count > 0)
                {
                    _currentMission = _missions.Dequeue();
                    _currentTutorial = (Tutorial)AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/Mission/Tutorial/" + _currentMission.Tutorial + ".asset", typeof(Tutorial));
                }
                else
                {
                    _currentMission = null;
                    _currentTutorial = null;
                }
            }
        }
        _dialogue.StartDialogue(_currentDialogueInfo);
        _animator.SetInteger("Talking Index", Random.Range(0, 4));
        _animator.SetTrigger("Talking");
    }

    public void ContinueInteraction()
    {
        if (!_dialogue.CheckIfDialogueEnded())
            _dialogue.DisplayNextSentence();
        else if (_dialogue.CheckIfDialogueEnded() && !_tutorial)
        {
            if (_currentMission && _currentMission.Status == MissionState.Ongoing) {
                EndFullInteraction(false);
                CheckTutorial();
            }
            else EndFullInteraction(true);
        } 
        else if (_dialogue.CheckIfDialogueEnded() && _tutorial)
        {
            _currentTutorial.SwitchPage();
            _player.UIController.ChangeTutorialPage(_currentTutorial);
        }
    }

    private void CheckTutorial()
    {
        _tutorial = true;
        _player.UIController.OpenTutorial(true);
        _player.UIController.ChangeTutorialPage(_currentTutorial);
    }

    public void ExitInteraction()
    {
        if (_tutorial) {
            _tutorial = false;
            _player.UIController.OpenTutorial(false);
        }
        EndFullInteraction(true);
    }

    private void EndFullInteraction(bool endFullInteraction)
    {
        if (endFullInteraction) base.EndInteract();
        EndInteract();
    }

    public override void EndInteract()
    {
        _dialogue.EndDialogue();
        _animator.SetInteger("Idle Index", Random.Range(0, 5));
        _animator.SetTrigger("Idle");
    }
}
