using UnityEngine;

public class PlayerSightedMessage : EnemyMessage
{
    private Vector3 _playerPosition;

    public PlayerSightedMessage(Vector3 playerPosition)
    {
        _playerPosition = playerPosition;
    }

    public Vector3 GetPlayerPosition()
    {
        return _playerPosition;
    }
}
