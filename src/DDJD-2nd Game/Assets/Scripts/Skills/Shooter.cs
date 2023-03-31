using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private Skill _leftSpell;
    private Skill _rightSpell;

    public Skill LeftSpell{get{return _leftSpell;} set{_leftSpell = value;}}
    public Skill RightSpell{get{return _rightSpell;} set{_rightSpell = value;}}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeftShoot(Vector3 position, Vector3 direction){
        Shoot(_leftSpell, position, direction);
    }

    public void RightShoot(Vector3 position, Vector3 direction){
        Shoot(_leftSpell, position, direction);
    }

    private void Shoot(Skill skill, Vector3 position, Vector3 direction){
        GameObject spell = Instantiate(skill.SpellPrefab, position, Quaternion.identity);
        SkillComponent skillComponent = spell.GetComponent<SkillComponent>();
        skillComponent.SetStats(skill.Stats);
        skillComponent.Shoot(direction);
    }
}
