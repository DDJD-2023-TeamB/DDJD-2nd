using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDeath : MonoBehaviour
{
    private RagdollController _ragdollController;

    private GameObject _spawnLocation;

    private bool _canRespawn = false;

    private int _goldLostOnDeath = 50;

    public Action OnRespawn;

    public Action OnRespawnAvailable;

    private void Awake()
    {
        _ragdollController = GetComponent<RagdollController>();
    }

    public void Die()
    {
        _ragdollController.ActivateRagdoll();
        _canRespawn = false;
        StartCoroutine(EnableRespawn(5.0f));
    }

    private GameObject GetSpawnLocation()
    {
        _spawnLocation = GameObject.FindGameObjectWithTag("SpawnLocation");
        return _spawnLocation;
    }

    public void Respawn()
    {
        _ragdollController.DeactivateRagdoll();
        transform.position = GetSpawnLocation().transform.position;
        OnRespawn?.Invoke();
    }

    public IEnumerator EnableRespawn(float time)
    {
        yield return new WaitForSeconds(time);
        _canRespawn = true;
        OnRespawnAvailable?.Invoke();
    }

    public bool CanRespawn
    {
        get { return _canRespawn; }
    }

    public int GoldLostOnDeath
    {
        get { return _goldLostOnDeath; }
    }
}
