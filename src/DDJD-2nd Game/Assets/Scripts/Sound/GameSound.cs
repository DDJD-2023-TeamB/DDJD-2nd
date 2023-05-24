using UnityEngine;

[System.Serializable]
public class GameSound
{
    [SerializeField]
    private bool _playOnStart = false;

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
}
