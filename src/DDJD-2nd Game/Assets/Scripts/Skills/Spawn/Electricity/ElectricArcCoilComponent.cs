using System.Collections;
using System.Collections.Generic;
using UnityEngine.VFX;
using UnityEngine;

public class ElectricArcCoilComponent : SpawnSkillComponent
{
    [SerializeField]
    private GameObject _arcPrefab;

    [SerializeField]
    private GameObject _vfxPrefab;

    [SerializeField]
    private float _yOffset = 0.5f;

    public override void Spawn()
    {
        PlayVfx();
        DamageNearbyEnemies();
        CreateElectricArc();
    }

    private void DamageNearbyEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(
            transform.position,
            _skill.SpawnStats.DamageRadius
        );
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == _caster)
                continue;

            if (_skill.SpawnStats.Damage > 0)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                float divider = Mathf.Min(1, distance);

                Damage(
                    collider.gameObject,
                    (int)(_skill.SpawnStats.Damage / divider),
                    collider.ClosestPoint(transform.position),
                    collider.transform.position - transform.position
                );
            }
        }
    }

    private void CreateElectricArc()
    {
        ObjectSpawner spawner = _caster.GetComponent<ObjectSpawner>();
        List<GameObject> spawnedObjects = spawner.SpawnedObjects;
        List<GameObject> coils = spawnedObjects.FindAll(
            obj => obj.GetComponent<ElectricArcCoilComponent>() != null
        );
        if (coils.Count <= 1) // First oness
            return;

        GameObject lastCoil = coils[coils.Count - 2];

        // Calculate Bezier arc with Y offset
        Vector3 pos1 = lastCoil.transform.position;
        Vector3 pos4 = transform.position;
        Vector3 pos2 = 2 * pos1 / 3 + pos4 / 3;
        Vector3 pos3 = pos1 / 3 + 2 * pos4 / 3;
        pos1.y += _yOffset;
        pos2.y += _yOffset;
        pos3.y += _yOffset;
        pos4.y += _yOffset;

        // Spawn object
        Vector3 arcPosition = (pos1 + pos4) / 2;
        GameObject arc = Instantiate(_arcPrefab, arcPosition, Quaternion.identity);
        arc.transform.right = pos1 - pos4;

        // Get bezier points
        Transform arcPos1 = arc.transform.Find("Pos1");
        Transform arcPos2 = arc.transform.Find("Pos2");
        Transform arcPos3 = arc.transform.Find("Pos3");
        Transform arcPos4 = arc.transform.Find("Pos4");

        // Scale object for correct box collider
        float realDistance = Vector3.Distance(pos1, pos4);
        float bezierDistance = Vector3.Distance(arcPos1.position, arcPos4.position);
        float scale = realDistance / bezierDistance;
        arc.transform.localScale = new Vector3(
            scale,
            arc.transform.localScale.y,
            arc.transform.localScale.z
        );

        // Set VFX positions
        arcPos1.position = pos1;
        arcPos2.position = pos2;
        arcPos3.position = pos3;
        arcPos4.position = pos4;

        SkillComponent arcComponent = arc.GetComponent<SkillComponent>();
        arcComponent.SetCaster(_caster);
        arcComponent.SetSkill(_skill);

        float arcLife = Mathf.Min(spawner.GetObjectLifeTime(lastCoil), _skill.SpawnStats.Duration);
        Destroy(arc, arcLife);
    }

    private void PlayVfx()
    {
        Vector3 position = transform.position;
        position.y = _vfxPrefab.transform.position.y;
        GameObject vfx = Instantiate(_vfxPrefab, position, Quaternion.identity);
        Destroy(vfx, _skill.SpawnStats.Duration);
    }
}