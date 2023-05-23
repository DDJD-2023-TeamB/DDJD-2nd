using UnityEngine;

public class MoveToMessage : EnemyMessage
{
    public Vector3 Position { get; set; }

    public MoveToMessage(Vector3 position)
    {
        Position = position;
    }
}
