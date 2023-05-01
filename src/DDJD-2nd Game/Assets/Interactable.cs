using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
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
        player._interactedObject = true;
        Approach();
    }

    public void OnTriggerExit(Collider other){
        if(other.tag != "Player"){
            return;
        }
        Player player = other.gameObject.GetComponent<Player>();
        player._interactedObject = false;
        //TODO
        Debug.Log("Leaving");
    }



    private void Approach(){
        //_playerIsInteracting
        Debug.Log("Press F to open the upgrade book");

    }
}
