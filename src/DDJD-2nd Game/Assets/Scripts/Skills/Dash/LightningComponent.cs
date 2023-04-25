using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningComponent : DashComponent
{
    private int _casterLayer;

    [SerializeField]
    private string[] _layersToDashThrough;
    private int[] _layersToIgnore;

    private List<GameObject> _objectsHit = new List<GameObject>();

    protected override void Start()
    {
        base.Start();
        _casterLayer = _caster.layer;
        _layersToIgnore = new int[_layersToDashThrough.Length];
        for (int i = 0; i < _layersToDashThrough.Length; i++)
        {
            _layersToIgnore[i] = LayerMask.NameToLayer(_layersToDashThrough[i]);
            Physics.IgnoreLayerCollision(_casterLayer, _layersToIgnore[i], true);
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnImpact(Collider other, float multiplier = 1)
    {
        base.OnImpact(other, multiplier);
        Damage(
            other.gameObject,
            (int)(_skillStats.Damage * multiplier),
            other.ClosestPoint(_caster.transform.position),
            _caster.transform.forward
        );
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _layersToDashThrough.Length; i++)
        {
            Physics.IgnoreLayerCollision(_casterLayer, _layersToIgnore[i], false);
        }
    }
}
