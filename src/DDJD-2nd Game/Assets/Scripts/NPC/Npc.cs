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
    private static string floatingIconPrefabPath = "Assets/Prefabs/UI/FloatingIconCanvas.prefab";
    private Animator _floatingIconAnimator;
    private GameObject _floatingIconCanvas;

    protected override void Start()
    {
        base.Start();
        _currentDialogueInfo = _npc.DefaultDialogueInfo;
        _animator = GetComponent<Animator>();
        CreateFloatingIconCanvas();
        StartCoroutine(UpdateFloatingIcon());
    }

    public void CreateFloatingIconCanvas()
    {
        GameObject floatingCanvasPrefab = (GameObject)
            AssetDatabase.LoadAssetAtPath(floatingIconPrefabPath, typeof(GameObject));
        _floatingIconCanvas = Instantiate(floatingCanvasPrefab);

        Image image = _floatingIconCanvas.transform.GetChild(0).GetComponent<Image>();

        _floatingIconAnimator = image.GetComponent<Animator>();
        _floatingIconCanvas.SetActive(false);
        _floatingIconCanvas.transform.SetParent(transform, false);
    }

    private IEnumerator UpdateFloatingIcon()
    {
        while (true)
        {
            bool missionFound = false;
            foreach (var mission in _missionController.GetNpcMissions(_npc, false))
            {
                if (mission.Status == MissionState.Available)
                {
                    if (!_floatingIconCanvas.activeSelf)
                    {
                        _floatingIconCanvas.SetActive(true);
                    }
                    missionFound = true;
                    break;
                }
                else if (mission.Status == MissionState.Ongoing)
                {
                    if (!_floatingIconCanvas.activeSelf)
                    {
                        _floatingIconCanvas.SetActive(true);
                        PauseAnimation();
                    }
                    missionFound = true;
                    break;
                }
            }

            if (_floatingIconCanvas.activeSelf && !missionFound)
            {
                _floatingIconCanvas.SetActive(false);
            }

            yield return new WaitForSeconds(0.5f);
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
        Mission targetMission = null;
        foreach (var mission in _missionController.GetNpcMissions(_npc))
        {
            switch (mission.Status)
            {
                case MissionState.Available:
                {
                    _currentDialogueInfo = mission.InteractionBegin.DialogueInfo;
                    mission.Status = MissionState.Ongoing;
                    mission.CurrentGoal.OnGoalStarted?.Invoke();
                    _missionController.MissionsUIController?.UpdateMissionsUI();
                    _missionController.CheckIfAllGoalsAreCompleted(mission);
                    targetMission = mission;
                    break;
                }
                case MissionState.Ongoing:
                {
                    if (
                        mission.CurrentGoal is InteractGoal interactGoal
                        && interactGoal.Interaction.Npc == _npc
                    )
                    {
                        _currentDialogueInfo = interactGoal.Interaction.DialogueInfo;
                    }
                    else if (mission.InteractionBegin.Npc == _npc)
                        _currentDialogueInfo = mission.InteractionBegin.DialogueInfo;
                    targetMission = mission;
                    break;
                }
            }

            if (targetMission != null)
                break;
        }

        if (targetMission != null)
        {
            _missionController.CheckIfNpcIsMyGoal(_npc, targetMission);
            _dialogue.Mission = targetMission;
        }

        _dialogue.StartDialogue(_currentDialogueInfo);

        if (_animator != null)
        {
            _animator.SetInteger("Talking Index", Random.Range(0, 4));
            _animator.SetTrigger("Talking");
        }
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
            if (_dialogue.Mission != null)
            {
                if (_dialogue.Mission.IsInInteractionBegin(_npc))
                {
                    if (_dialogue.Mission.InteractionBegin.InteractionEnded())
                    {
                        return;
                    }
                }
                _dialogue.Mission = null;
            }
            EndFullInteraction(true);
        }
    }

    public void ExitInteraction()
    {
        if (_dialogue.Mission != null)
        {
            if (_dialogue.Mission.IsInInteractionBegin(_npc))
            {
                _dialogue.Mission.InteractionBegin.Exit();
            }
            _dialogue.Mission = null;
        }
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
        _dialogue?.EndDialogue();

        if (_animator != null)
        {
            _animator.SetInteger("Idle Index", Random.Range(0, 5));
            _animator.SetTrigger("Idle");
        }
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
