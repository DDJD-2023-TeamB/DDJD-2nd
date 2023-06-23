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
        string parameterName = _element.SfxDamageLabel + "ChargeState";
        _sfxStateID = _soundEmitter.GetParameterId("absorb", parameterName);
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
    }

    public void PrepareTransfer(Player player)
    {
        _player = player;
        _soundEmitter.SetParameter("absorb", _sfxStateID, 0);
        _soundEmitter.Play("absorb");
    }

    public virtual void StopTransfer(Player player)
    {
        if (_player == player && _currentVFX != null)
        {
            _currentVFX.Stop();
        }

        _soundEmitter.SetParameter("absorb", _sfxStateID, 1);
    }

    public Element Element
    {
        get { return _element; }
    }
}
