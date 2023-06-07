using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LightningComponent : DashComponent, NonCollidable
{
    private int _casterLayer;

    [SerializeField]
    private string[] _layersToDashThrough;
    private int[] _layersToIgnore;

    private SkinnedMeshRenderer[] _skinnedMeshRenderers;

    private VisualEffect _vfx;

    [SerializeField]
    private Material _lightningMaterial;

    private List<List<Material>> _casterMaterials = new List<List<Material>>();

    private CapsuleCollider _collider;

    protected override void Awake()
    {
        base.Awake();
        _vfx = GetComponent<VisualEffect>();
        _lightningMaterial = Instantiate(_lightningMaterial);
        _collider = GetComponent<CapsuleCollider>();
    }

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

        _skinnedMeshRenderers = _caster.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer smr in _skinnedMeshRenderers)
        {
            List<Material> materials = new List<Material>();
            foreach (Material material in smr.materials)
            {
                materials.Add(material);
            }
            _casterMaterials.Add(materials);
            smr.materials = new Material[] { _lightningMaterial };
        }
        _vfx.SetVector3("Forward", transform.forward);
        _vfx.SetVector3("Position", _caster.transform.position);
        _vfx.SendEvent("Flash");
        StartCoroutine(UpdateVFXTrail());
        _soundEmitter.SetEventPositionToFollow("dash", _caster);
    }

    public override void SetSkill(Skill skill)
    {
        base.SetSkill(skill);
        StartCoroutine(DestroyAfter(_dashSkill.DashSkillStats.EffectDuration));
    }

    protected override void OnImpact(Collider other, float multiplier = 1)
    {
        base.OnImpact(other, multiplier);
        Damage(
            other.gameObject,
            (int)(_skillStats.Damage * multiplier),
            (int)_skillStats.ForceWithDamage(),
            other.ClosestPoint(_caster.transform.position),
            _caster.transform.forward
        );
    }

    private void OnDestroy() { }

    public override void OnDashEnd()
    {
        base.OnDashEnd();
        _vfx.SendEvent("Flash");
        _vfx.SetFloat("CharacterParticleRate", 0);
        UpdateCollider();
        for (int i = 0; i < _layersToDashThrough.Length; i++)
        {
            Physics.IgnoreLayerCollision(_casterLayer, _layersToIgnore[i], false);
        }

        for (int i = 0; i < _skinnedMeshRenderers.Length; i++)
        {
            _skinnedMeshRenderers[i].materials = _casterMaterials[i].ToArray();
        }
    }

    private void UpdateCollider()
    {
        float dashLength = (_caster.transform.position - transform.position).magnitude;
        _collider.center = new Vector3(0, 0, dashLength / 2);
        _collider.height = dashLength;
    }

    public override void DestroySpell()
    {
        _vfx.Stop();
        _collider.enabled = false;
        Destroy(gameObject, 1.0f);
    }

    private IEnumerator UpdateVFXTrail()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            _vfx.SetVector3("Position", _caster.transform.position);
            _vfx.SetSkinnedMeshRenderer("Mesh", _skinnedMeshRenderers[0]);
        }
    }

    private IEnumerator DestroyAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        DestroySpell();
    }
}
