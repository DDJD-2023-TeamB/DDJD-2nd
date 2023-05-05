using UnityEngine;

public class BoneTransform
{
    private Vector3 _position;
    private Quaternion _rotation;
    public Vector3 Position
    {
        get { return _position; }
    }
    public Quaternion Rotation
    {
        get { return _rotation; }
    }

    public BoneTransform(Vector3 position, Quaternion rotation)
    {
        _position = position;
        _rotation = rotation;
    }
}
