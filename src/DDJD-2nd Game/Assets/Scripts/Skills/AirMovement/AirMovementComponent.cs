using UnityEngine;

public abstract class AirMovementComponent : MonoBehaviour
{
    protected Player _player;
    protected Rigidbody _rb;

    protected virtual void Awake()
    {
        enabled = false;
        _player = GetComponent<Player>();
        _rb = GetComponent<Rigidbody>();
    }

    public virtual void Reset() { }

    public virtual void SetSkill(AirMovementSkill skill)
    {
        enabled = true;
    }

    protected Vector3 GetMovementDirection()
    {
        Vector2 direction = _player.Input.MoveInput;
        Vector3 returnDirection = transform.forward * direction.y + transform.right * direction.x;
        return returnDirection;
    }

    public virtual void Update() { }

    public virtual void OnKeyDown() { }

    public virtual void OnKeyUp() { }
}
