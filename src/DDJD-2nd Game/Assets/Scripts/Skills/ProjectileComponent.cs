using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour , SkillComponent
{

    private Rigidbody _rb;
    private ProjectileStats _stats;
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStats(SkillStats stats){
        _stats = (ProjectileStats) stats;
    }

    public void Shoot(Vector3 direction){
        //Add stats here
        _rb.AddForce(direction.normalized * Time.deltaTime * 1000 * 10.0f, ForceMode.Impulse);
    }
}
