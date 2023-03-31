using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour , SkillComponent
{

    private Rigidbody _rb;
    private ProjectileStats _stats;

    private GameObject _impactPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSkill(Skill skill){
        Projectile projectile = (Projectile) skill;
        _stats = projectile.Stats;
        _impactPrefab = projectile.ImpactPrefab;
    }

    public void Shoot(Vector3 direction){
        //Add stats here
        _rb.AddForce(direction.normalized * 10.0f, ForceMode.Impulse);
    }

    public void OnTriggerEnter(Collider other){
        Debug.Log("Projectile hit " + other.gameObject.name);
        if(other.gameObject.tag == "Player"){
            return;
        }
        GameObject impact = Instantiate(_impactPrefab, transform.position, Quaternion.identity);
        Destroy(impact, 3.0f);
        if(other.gameObject.tag == "Enemy"){
            //other.gameObject.GetComponent<Enemy>().TakeDamage(_stats.Damage);
            
        }
        Destroy(gameObject);
    }
}
