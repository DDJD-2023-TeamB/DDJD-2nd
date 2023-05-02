using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricArcCoilComponent : SpawnSkillComponent
{
    [SerializeField]
    private GameObject _arcPrefab;

    public override void Spawn()
    {
        ObjectSpawner spawner = _caster.GetComponent<ObjectSpawner>();
        List<GameObject> spawnedObjects = spawner.SpawnedObjects;
        List<GameObject> coils = spawnedObjects.FindAll(
            obj => obj.GetComponent<ElectricArcCoilComponent>() != null
        );
        if (coils.Count <= 1) // First one
            return;

        GameObject lastCoil = coils[coils.Count - 2];

        Vector3 arcPosition = (lastCoil.transform.position + transform.position) / 2;
        GameObject arc = Instantiate(_arcPrefab, arcPosition, Quaternion.identity);

        Transform arcPos1 = arc.transform.Find("Pos1");
        Transform arcPos2 = arc.transform.Find("Pos2");
        Transform arcPos3 = arc.transform.Find("Pos3");
        Transform arcPos4 = arc.transform.Find("Pos4");
        arcPos1.position = lastCoil.transform.position;
        arcPos4.position = transform.position;
        arcPos2.position = 2 * arcPos1.position / 3 + arcPos4.position / 3;
        arcPos3.position = arcPos1.position / 3 + 2 * arcPos4.position / 3;

        float arcLife = Mathf.Min(spawner.GetObjectLifeTime(lastCoil), _skill.SpawnStats.Duration);
        Destroy(arc, arcLife);
    }
}
