using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Scriptable Objects/Items/Potion")]
public class Potion : CollectibleObject
{
    public int restoreHealthValue;

    [SerializeField]
    private FMODUnity.EventReference soundEvent;

    public override void Use(Player player)
    {
        FMODUnity.RuntimeManager.PlayOneShot(soundEvent, player.transform.position);
        player.Status.RestoreHealth(restoreHealthValue);

        Debug.Log("Using potion");
    }
}
