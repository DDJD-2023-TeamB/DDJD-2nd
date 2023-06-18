using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

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

    private static string floatingIconPrefabPath = "Assets/Prefabs/UI/FloatingIconCanvas.prefab";
    private Animator _floatingIconAnimator;
    private GameObject _floatingIconCanvas;

    protected override void Start()
    {
        base.Start();
        _currentDialogueInfo = _npc.DefaultDialogueInfo;
        _missions = _missionController.GetNpcMissions(_npc);
        _animator = GetComponent<Animator>();
        if (_missions.Count != 0)
        {
            _currentMission = _missions.Dequeue();
            CreateCanvas();
        }
    }

    public void CreateCanvas()
    {
        GameObject floatingCanvasPrefab = (GameObject)
            AssetDatabase.LoadAssetAtPath(floatingIconPrefabPath, typeof(GameObject));
        _floatingIconCanvas = Instantiate(floatingCanvasPrefab);

        Image image = _floatingIconCanvas.transform.GetChild(0).GetComponent<Image>();

        _floatingIconAnimator = image.GetComponent<Animator>();
        _floatingIconCanvas.SetActive(false);
        _floatingIconCanvas.transform.SetParent(transform, false);
    }

    void Update()
    {
        if (_currentMission != null)
        {
            if (_currentMission.Status == MissionState.Available && !_floatingIconCanvas.activeSelf)
            {
                _floatingIconCanvas.SetActive(true);
            }
            else if (
                _currentMission.Status == MissionState.Ongoing && !_floatingIconCanvas.activeSelf
            )
            {
                _floatingIconCanvas.SetActive(true);
                PauseAnimation();
            }
        }
        else if (_floatingIconCanvas && _floatingIconCanvas.activeSelf)
        {
            _floatingIconCanvas.SetActive(false);
        }
    }

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
        _dialogue.StartDialogue(_currentDialogueInfo);
        _animator.SetInteger("Talking Index", Random.Range(0, 4));
        _animator.SetTrigger("Talking");
        if (_floatingIconAnimator)
        {
            PauseAnimation();
        }
    }

    public void PauseAnimation()
    {
        Floating floatingScript = _floatingIconCanvas.transform.GetChild(0).GetComponent<Floating>();
        _floatingIconAnimator.SetTrigger("Stop");
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
