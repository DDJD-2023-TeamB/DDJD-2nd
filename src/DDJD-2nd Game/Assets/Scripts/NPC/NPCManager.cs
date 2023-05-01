using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : Interactable
{
    public Dialogue dialogue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact(){
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void ContinueInteraction(){
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        if(!dialogueManager.CheckIfDialogueEnded())
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        else{
            base.EndInteract();
        }
  
    }
}
