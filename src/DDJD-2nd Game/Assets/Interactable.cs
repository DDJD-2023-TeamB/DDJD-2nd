using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //private GameObject object;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Interactable");
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
        //TODO
        Debug.Log("Leaving");
    }



    private void Approach(){
        //_playerIsInteracting
        Debug.Log("Press F to open the upgrade book");

    }

    public abstract void Interact();
}
