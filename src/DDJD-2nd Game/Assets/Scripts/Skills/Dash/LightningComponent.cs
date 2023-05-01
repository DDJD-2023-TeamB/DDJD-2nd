using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningComponent : DashComponent
{
    private int _casterLayer;

    [SerializeField]
    private string[] _layersToDashThrough;
    private int[] _layersToIgnore;
    private float timer = 0f;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _caster)
        {
            return;
        }
        _objectsHit.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _caster)
        {
            return;
        }
        _objectsHit.Remove(other.gameObject);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < _skill.DashSkillStats.DamageRate)
        {
            return;
        }
        timer = 0f;
        foreach (GameObject obj in _objectsHit)
        {
            Damage(
                obj,
                (int)_skill.DashSkillStats.Damage,
                obj.transform.position,
                transform.forward
            );
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _layersToDashThrough.Length; i++)
        {
            Physics.IgnoreLayerCollision(_casterLayer, _layersToIgnore[i], false);
        }
    }
}
