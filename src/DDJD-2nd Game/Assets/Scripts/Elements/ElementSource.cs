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

    protected void Awake() { }

    // Start is called before the first frame update
    void Start() { }

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

    public virtual void StopTransfer(Player player)
    {
        if (_player == player && _currentVFX != null)
        {
            _currentVFX.Stop();
        }
    }

    public Element Element
    {
        get { return _element; }
    }
}
