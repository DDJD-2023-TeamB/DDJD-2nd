using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashable : MonoBehaviour
{
    public void DashWithSkill(Dash dashSkill)
    {
        GameObject spell = Instantiate(
            dashSkill.SpellPrefab,
            transform.position,
            Quaternion.identity
        );
    }
}
