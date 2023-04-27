using UnityEngine;

public abstract class AirMovementSkill : Skill
{
    /*
        Initialize the AirMovementComponent with the skill in the given object
    */
    public abstract AirMovementComponent Initialize(GameObject obj);
}
