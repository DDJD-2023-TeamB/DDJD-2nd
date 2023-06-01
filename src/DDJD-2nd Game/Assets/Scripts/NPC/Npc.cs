using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : Interactable
{
    [SerializeField]
    private DialogueInfo _dialogueInfo;
    private Dialogue _dialogue;

    protected override void Start()
    {
        base.Start();
        _dialogue = _player.Dialogue;
    }

    void Update() { }

    public override void Interact()
    {
        _dialogue.StartDialogue(_dialogueInfo);
    }

    public void ContinueInteraction()
    {
        Debug.Log("ContinueInteraction");
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
