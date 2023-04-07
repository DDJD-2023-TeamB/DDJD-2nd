using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashable : MonoBehaviour
{
    public void DashWithSkill(Dash dashSkill)
    {
        // TODO: Improve after dash logic is implemented
        Quaternion rotation = Quaternion.LookRotation(transform.forward);
        GameObject spell = Instantiate(dashSkill.SpellPrefab, transform.position, rotation);
    }
}
