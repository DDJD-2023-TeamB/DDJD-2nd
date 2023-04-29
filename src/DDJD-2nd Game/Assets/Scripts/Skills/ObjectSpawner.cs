using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    private List<GameObject> _spawnedObjects = new List<GameObject>();
    public List<GameObject> SpawnedObjects
    {
        get => _spawnedObjects;
    }

    private List<float> _spawnedObjectsLifeTime = new List<float>();
    public List<float> SpawnedObjectsLifeTime
    {
        get => _spawnedObjectsLifeTime;
    }

    private GameObject _previewObject;
    public GameObject PreviewObject
    {
        get => _previewObject;
    }

    private void Update()
    {
        for (int i = 0; i < _spawnedObjects.Count; i++)
        {
            _spawnedObjectsLifeTime[i] -= Time.deltaTime;
            if (_spawnedObjectsLifeTime[i] <= 0)
            {
                Destroy(_spawnedObjects[i]);
                _spawnedObjects.RemoveAt(i);
                _spawnedObjectsLifeTime.RemoveAt(i);
            }
        }

        // TODO calculate preview position based on the player aim
    }

    public GameObject CreatePreviewObject(SpawnSkill skill, Quaternion rotation)
    {
        if (_previewObject != null)
        {
            return null;
        }
        // TODO calculate position based on the player aim
        // TODO change object opacity
        _previewObject = Instantiate(skill.SpellPrefab, new Vector3(), rotation);
        return _previewObject;
    }

    public void SpawnObject(SpawnSkill skill)
    {
        // TODO set opacity back to normal
        _spawnedObjects.Add(_previewObject);
        _spawnedObjectsLifeTime.Add(skill.SpawnStats.Duration);
        _previewObject = null;
    }
}
