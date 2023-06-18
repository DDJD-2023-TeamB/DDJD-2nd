using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "Potion", menuName = "Scriptable Objects/Items/Potion")]
public class Potion : CollectibleObject
{
    public int restoreHealthValue;

    [SerializeField]
    private FMODUnity.EventReference soundEvent;

    [SerializeField]
    private GameObject _effectPrefab;

    [SerializeField]
    private float _effectDuration = 2f;

    private VisualEffect _vfx;
    private SkinnedMeshRenderer _renderer;

    public override void Use(Player player)
    {
        FMODUnity.RuntimeManager.PlayOneShot(soundEvent, player.transform.position);
        player.Status.RestoreHealth(restoreHealthValue);

        GameObject effect = Instantiate(_effectPrefab);
        _vfx = effect.GetComponent<VisualEffect>();
        _renderer = GetPlayerMeshRenderer(player);
        if (_renderer == null)
        {
            Debug.LogError("Player mesh renderer not found");
            return;
        }

        _vfx.SetSkinnedMeshRenderer("SkinnedMeshRenderer", _renderer);
        VfxUtils.SetVFXTransformProperty(_vfx, "Transform", _renderer.transform);
        player.StartCoroutine(UpdateVFXTrail());

        Destroy(effect, _effectDuration);
    }

    private SkinnedMeshRenderer GetPlayerMeshRenderer(Player player)
    {
        SkinnedMeshRenderer[] renderers = player.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer renderer in renderers)
        {
            if (renderer.gameObject.tag == "PlayerSkinMesh")
            {
                return renderer;
            }
        }
        return null;
    }

    private IEnumerator UpdateVFXTrail()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (_vfx == null)
                yield break;

            _vfx.SetSkinnedMeshRenderer("SkinnedMeshRenderer", _renderer);
            VfxUtils.SetVFXTransformProperty(_vfx, "Transform", _renderer.transform);
        }
    }
}
