using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DisappearEffect : MonoBehaviour
{

    [SerializeField]
    private Material _material;

    [SerializeField]
    private float _timeToDisappear = 1f;

    [SerializeField]
    private List<Renderer> _renderers;
    private float _timeElapsed = 0f;

    private bool disappearing = false;

    void Update()
    {   
        if(!disappearing){
            return;
        }
        Debug.Log("Disappearing " + _timeElapsed);
        _timeElapsed += Time.deltaTime;
        float dissolveAmount = _timeElapsed / _timeToDisappear;
        foreach (Renderer renderer in _renderers)
        {   
            Material material = renderer.materials[0];
            material.SetFloat("_Dissolve", dissolveAmount);
        }
        if (_timeElapsed >= _timeToDisappear)
        {
            Destroy(gameObject);
        }
    }

    public void StartDisappear(){
        _material.SetFloat("Dissolve", _timeElapsed / _timeToDisappear);
        disappearing = true;
        _material = new Material(_material);
        foreach (Renderer renderer in _renderers)
        {   
            renderer.materials = new Material[]{_material};
        }
    }
}
