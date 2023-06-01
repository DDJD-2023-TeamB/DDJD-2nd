using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
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
        _player.InteractedObject = this;
        Approach();
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }
        EndInteract();
    }

    protected virtual void Approach()
    {
        // TODO APPEAR A TEXT
        Debug.Log("Press F to open the upgrade book");
    }

    public abstract void Interact();

    public virtual void EndInteract()
    {
        _player.InteractedObject = null;
    }
}
