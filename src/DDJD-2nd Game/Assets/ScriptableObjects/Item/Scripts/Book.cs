using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Book", menuName = "Scriptable Objects/Items/Book")]
public class Book : CollectibleObject
{
    // associado uma skill - ItemSkill
    public override void Use(){
        // TODO: verifica se existe a skill na lista do learnedSkills e adiciona se não tiver
        Debug.Log("Using book");
    }
}