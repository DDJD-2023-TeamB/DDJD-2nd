using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningComponent : DashComponent
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == _caster)
        {
            return;
        }
        // TODO: Deal damage to enemies
        Debug.Log("Lightning hitting " + other.gameObject.name);
    }
}
