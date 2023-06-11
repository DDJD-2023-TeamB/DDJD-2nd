using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private TutorialPageInfo[] _tutorialInfo;

    // public void Awake()
    // {
    //     _animator = GetComponent<Animator>();
    //     _sentences = new Queue<string>();
    // }

    public void Start() { }

    public void DisplayNextSentence()
    {
        // if (_isTyping)
        // {
        //     return;
        // }
        
        // if( _sentences.Count > 0 )
        // {
        //     string sentence = _sentences.Dequeue();
        //     _isTyping = true;
        //     StartCoroutine(TypeSentence(sentence));
        // }
    }

    // IEnumerator TypeSentence(string sentence)
    // {
    //     // _dialogueText.text = "";
    //     // foreach (char letter in sentence.ToCharArray())
    //     // {
    //     //     _dialogueText.text += letter;
    //     //     yield return null;
    //     // }
    //     // yield return new WaitForSeconds(1.0f);
    //     // _isTyping = false;
    // }

    public void EndDialogue()
    {
        // _animator.SetBool("isOpen", false);
    }

    public void CheckIfDialogueEnded()
    {
        // if (_isTyping)
        // {
        //     return false;
        // }
        // if (_sentences.Count == 0)
        // {
        //     EndDialogue();
        //     return true;
        // }
        // return false;
    }

    public TutorialPageInfo[] TutorialPageInfo
    {
        get { return _tutorialInfo; }
    }
}
