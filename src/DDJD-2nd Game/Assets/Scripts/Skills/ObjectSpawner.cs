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
    private Color _previewObjectColor;
    private Collider _previewObjectCollider;

    private AimComponent _aimComponent;

    private void Awake()
    {
        _aimComponent = GetComponent<AimComponent>();
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

        if (_previewObject != null)
            _previewObject.transform.position = (Vector3)GetPreviewObjectPosition(_previewObject);
    }

    public GameObject CreatePreviewObject(SpawnSkill skill, Quaternion rotation)
    {
        if (_previewObject != null)
        {
            return null;
        }

        Vector3? position = GetPreviewObjectPosition(skill.SpellPrefab);
        if (position == null)
        {
            return null;
        }

        _previewObject = Instantiate(skill.SpellPrefab, (Vector3)position, rotation);
        SpawnSkillComponent spawnComponent = _previewObject.GetComponent<SpawnSkillComponent>();
        spawnComponent.SetCaster(gameObject);
        spawnComponent.SetSkill(skill);

        // Disable collisions
        _previewObjectCollider = _previewObject.GetComponent<Collider>();
        _previewObjectCollider.enabled = false;

        // Add transparency
        Renderer renderer = _previewObject.GetComponent<Renderer>();
        Color oldColor = renderer.material.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 0.5f);
        renderer.material.color = newColor;
        _previewObjectColor = oldColor;

        return _previewObject;
    }

    public void SpawnObject(SpawnSkill skill)
    {
        // Reactivate collisions
        _previewObjectCollider.enabled = true;

        // Reset transparency
        Renderer renderer = _previewObject.GetComponent<Renderer>();
        renderer.material.color = _previewObjectColor;

        _spawnedObjects.Add(_previewObject);
        _spawnedObjectsLifeTime.Add(skill.SpawnStats.Duration);

        SpawnSkillComponent spawnComponent = _previewObject.GetComponent<SpawnSkillComponent>();
        spawnComponent.Spawn();

        _previewObject = null;
        _previewObjectCollider = null;
    }

    public float GetObjectLifeTime(GameObject obj)
    {
        int index = _spawnedObjects.IndexOf(obj);
        if (index == -1)
            return -1;
        return _spawnedObjectsLifeTime[index];
    }

    private Vector3? GetPreviewObjectPosition(GameObject gameObject = null)
    {
        if (gameObject == null && _previewObject != null)
            gameObject = _previewObject;

        RaycastHit hit;
        if (_aimComponent.GetAimRaycastHit(out hit))
        {
            Vector3 pos = hit.point;
            if (gameObject != null)
            {
                Collider collider = gameObject.GetComponent<Collider>();
                collider.enabled = true;
                pos.y += collider.bounds.extents.y;
                collider.enabled = false;
            }
            return pos;
        }
        if (_previewObject == null)
            return _previewObject.transform.position;
        return null;
    }
}
