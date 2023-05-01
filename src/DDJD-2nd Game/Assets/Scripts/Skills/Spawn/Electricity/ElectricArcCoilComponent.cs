using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricArcCoilComponent : SpawnSkillComponent
{
    [SerializeField]
    private GameObject _arcPrefab;

    public override void Spawn()
    {
        Transform arcPos1 = _arcPrefab.transform.Find("Pos1");
        Transform arcPos2 = _arcPrefab.transform.Find("Pos2");
        Transform arcPos3 = _arcPrefab.transform.Find("Pos3");
        Transform arcPos4 = _arcPrefab.transform.Find("Pos4");

        ObjectSpawner spawner = _caster.GetComponent<ObjectSpawner>();
        List<GameObject> spawnedObjects = spawner.SpawnedObjects;
        List<GameObject> coils = spawnedObjects.FindAll(
            obj => obj.GetComponent<ElectricArcCoilComponent>() != null
        );
        if (coils.Count <= 1) // First one
            return;

        GameObject lastCoil = coils[coils.Count - 2];
        arcPos1.position = lastCoil.transform.position;
        arcPos4.position = transform.position;
        arcPos2.position = 2 * arcPos1.position / 3 + arcPos4.position / 3;
        arcPos3.position = arcPos1.position / 3 + 2 * arcPos4.position / 3;

        Vector3 arcPosition = (arcPos1.position + arcPos4.position) / 2;
        Debug.Log(
            "pos1"
                + arcPos1.position
                + " pos2"
                + arcPos2.position
                + " pos3"
                + arcPos3.position
                + " pos4"
                + arcPos4.position
                + " arcPos"
                + arcPosition
        );
        GameObject arc = Instantiate(_arcPrefab, arcPosition, Quaternion.identity);
        float arcLife = Mathf.Min(spawner.GetObjectLifeTime(lastCoil), _skill.SpawnStats.Duration);
        Destroy(arc, arcLife);
    }
}
