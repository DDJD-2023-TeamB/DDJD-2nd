using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningComponent : DashComponent
{
    private int _casterLayer;

    [SerializeField]
    private string[] _layersToDashThrough;
    private int[] _layersToIgnore;

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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == _caster)
        {
            return;
        }
        // TODO: Deal damage to enemies
        Debug.Log("Lightning hitting " + other.gameObject.name);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _layersToDashThrough.Length; i++)
        {
            Physics.IgnoreLayerCollision(_casterLayer, _layersToIgnore[i], false);
        }
    }
}
