using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Interactable : MonoBehaviour
{
    private TextMeshProUGUI _helpText;
    //private GameObject object;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other){
        
        if(other.tag != "Player"){
            return;
        }
        Player player = other.gameObject.GetComponent<Player>();
        player._interactedObject = this.gameObject.GetComponent<Interactable>();
        Approach();
    }

    public void OnTriggerExit(Collider other){
        if(other.tag != "Player"){
            return;
        }
        Player player = other.gameObject.GetComponent<Player>();
        player._interactedObject = null;
        HelpManager.Instance.SetHelpText("");
        //TODO
        Debug.Log("Leaving");
    }

    private void Approach(){
        //_playerIsInteracting
        HelpManager.Instance.SetHelpText("Press F to interact");
    }

    public abstract void Interact();
}
