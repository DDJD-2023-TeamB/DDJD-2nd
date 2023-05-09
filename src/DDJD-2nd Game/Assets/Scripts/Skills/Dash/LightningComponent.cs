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

    public override void SetSkill(Skill skill)
    {
        base.SetSkill(skill);
        Destroy(gameObject, _skill.DashSkillStats.EffectDuration);
    }

    protected override void OnImpact(Collider other, float multiplier = 1)
    {
        base.OnImpact(other, multiplier);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _layersToDashThrough.Length; i++)
        {
            Physics.IgnoreLayerCollision(_casterLayer, _layersToIgnore[i], false);
        }
    }
}
