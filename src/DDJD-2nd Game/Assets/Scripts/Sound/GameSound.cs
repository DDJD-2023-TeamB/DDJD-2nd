using UnityEngine;

[System.Serializable]
public class GameSound
{
    [SerializeField]
    private bool _playOnStart = false;

    [SerializeField]
    private bool _stopOnDestroy = true;

    [SerializeField]
    private bool _updatePosition = false;

    [SerializeField]
    private FMODUnity.EventReference _eventReference;

    public bool PlayOnStart
    {
        get { return _playOnStart; }
    }

    public FMODUnity.EventReference EventReference
    {
        get { return _eventReference; }
    }

    public bool StopOnDestroy
    {
        get { return _stopOnDestroy; }
    }

    public bool UpdatePosition
    {
        get { return _updatePosition; }
    }
}
