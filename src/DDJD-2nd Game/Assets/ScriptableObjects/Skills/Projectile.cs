using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Scriptable Objects/OnlyOnce/Spell/Projectile", order = 1)]
public class Projectile : Skill
{
    
    [SerializeField] private ProjectileStats _stats;
    
}
