using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ElementSource : MonoBehaviour
{
    [SerializeField]
    private Element _element;

    [SerializeField]
    private GameObject _tranferVfxPrefab;

    private Player _player;

    private ElementAbsorbVFX _currentVFX;

    private SoundEmitter _soundEmitter;
    private FMOD.Studio.PARAMETER_ID _sfxStateID;

    protected void Awake()
    {
        _soundEmitter = GetComponent<SoundEmitter>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _sfxStateID = _soundEmitter.GetParameterId("absorb", "State");
    }

    // Update is called once per frame
    void Update() { }

    public virtual void Transfer(Player player, Transform handTransform)
    {
        _player = player;
        _currentVFX = Instantiate(_tranferVfxPrefab, transform.position, Quaternion.identity)
            .GetComponent<ElementAbsorbVFX>();

        //VFx face hand
        _currentVFX.transform.forward = (handTransform.position - transform.position).normalized;
        _currentVFX.SetTarget(handTransform);

        _soundEmitter.SetParameterWithLabel("absorb", _sfxStateID, "Start", true);
        _soundEmitter.CallWithDelay(
            () => _soundEmitter.SetParameterWithLabel("absorb", _sfxStateID, "Idle", false),
            0.5f
        );
    }

    public virtual void StopTransfer(Player player)
    {
        if (_player == player && _currentVFX != null)
        {
            _currentVFX.Stop();
        }

        _soundEmitter.SetParameterWithLabel("absorb", _sfxStateID, "Complete", true);
    }

    public Element Element
    {
        get { return _element; }
    }
}
