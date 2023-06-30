using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCallback : MonoBehaviour
{
    public void MakeNpcDamageable(string npcName)
    {
        GameObject npc = GameObject.Find(npcName);
        GameObject.Destroy(npc.GetComponent<Npc>());
    }
}
