using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private DialogueInfo _dialogueInfo;

    private Queue<string> _sentences;

    [SerializeField]
    private TextMeshProUGUI _nameText;

    [SerializeField]
    private TextMeshProUGUI _dialogueText;
    private Animator _animator;
    private bool _isTyping = false;

    public void Awake()
    {
        _animator = GetComponent<Animator>();
        _sentences = new Queue<string>();
    }

    public void Start() { }

    public void StartDialogue(DialogueInfo dialogueInfo)
    {
        _animator.SetBool("isOpen", true);
        _nameText.text = dialogueInfo.Noun;
        _sentences.Clear();

        foreach (string sentence in dialogueInfo.Sentences)
        {
            _sentences.Enqueue(sentence);
        }
        Debug.Log("StartDialogue " + _sentences.Count);
        _dialogueInfo = dialogueInfo;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_isTyping)
        {
            return;
        }
        
        if( _sentences.Count > 0 )
        {
            string sentence = _sentences.Dequeue();
            _isTyping = true;
            StartCoroutine(TypeSentence(sentence));
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        _dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        _isTyping = false;
    }

    public void EndDialogue()
    {
        _animator.SetBool("isOpen", false);
    }

    public bool CheckIfDialogueEnded()
    {
        if (_isTyping)
        {
            return false;
        }
        if (_sentences.Count == 0)
        {
            return true;
        }
        return false;
    }

    public DialogueInfo DialogueInfo
    {
        get { return _dialogueInfo; }
        set { _dialogueInfo = value; }
    }
}
