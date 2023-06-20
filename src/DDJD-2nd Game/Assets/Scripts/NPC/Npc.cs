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

    private Mission _currentMission;

    private static string floatingIconPrefabPath = "Assets/Prefabs/UI/FloatingIconCanvas.prefab";
    private Animator _floatingIconAnimator;
    private GameObject _floatingIconCanvas;

    protected override void Start()
    {
        base.Start();
        _currentDialogueInfo = _npc.DefaultDialogueInfo;
        _animator = GetComponent<Animator>();
        List<Mission> missions = _missionController.GetNpcMissions(_npc, false);
        if (missions.Count != 0)
        {
            _currentMission = missions[0];
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
            HelpManager.Instance.ResetText();
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
            }
        }

        _missionController.CheckIfNpcIsMyGoal(_npc);

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
        Floating floatingScript = _floatingIconCanvas.transform
            .GetChild(0)
            .GetComponent<Floating>();
        _floatingIconAnimator.SetTrigger("Stop");
    }

    public void ContinueInteraction()
    {
        if (!_dialogue.CheckIfDialogueEnded())
        {
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
        base.EndInteract();
        _dialogue.EndDialogue();
        _animator.SetInteger("Idle Index", Random.Range(0, 5));
        _animator.SetTrigger("Idle");
        //Interaction e chamar a função do interation que é um event
    }

    protected override void Approach()
    {
        HelpManager.Instance.SetHelpText("Press F to interact");
    }

    public override bool IsInstant()
    {
        return false;
    }
}
