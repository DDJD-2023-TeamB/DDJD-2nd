using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Interactable : MonoBehaviour
{
    private TextMeshProUGUI _helpText;
    //private GameObject object;
    protected Player _player;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update() { }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }
   
        _player._interactedObject = this.gameObject.GetComponent<Interactable>();
        Approach();
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }

        _player._interactedObject = null;
        HelpManager.Instance.SetHelpText("");
        //TODO
        Debug.Log("Leaving");
    }

    private void Approach(){
        //_playerIsInteracting
        HelpManager.Instance.SetHelpText("Press F to interact");
        EndInteract();
    }

    public abstract void Interact();

    public virtual void EndInteract()
    {
        _player.InteractedObject = null;
    }

}
